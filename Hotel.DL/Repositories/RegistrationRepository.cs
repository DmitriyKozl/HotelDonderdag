using System.Data.SqlClient;
using Hotel.BL.Interfaces;
using Hotel.BL.Model;
using Hotel.DL.Exceptions;

namespace Hotel.DL.Repositories;

public class RegistrationRepository : IRegistrationRepository {
    private string connectionString;

    public RegistrationRepository(string connectionString) {
        this.connectionString = connectionString;
    }

    public void AddRegistration(Registration registration) {
        try {
            if (!HasEnoughCapacity(registration.Activity.ActivityId,
                    registration.NumberOfAdults + registration.NumberOfChildren)) {
                throw new RegistrationRepositoryException("Not enough capacity for this activity.");
            }

            string query =
                "INSERT INTO Registrations (CustomerID, ActivityID, NumberOfAdults, NumberOfChildren,TotalCost) " +
                "VALUES (@CustomerID, @ActivityID, @NumberOfAdults,@NumberOfChildren, @TotalCost)";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", registration.Customer.Id);
                command.Parameters.AddWithValue("@ActivityID", registration.Activity.ActivityId);
                command.Parameters.AddWithValue("@NumberOfAdults", registration.NumberOfAdults);
                command.Parameters.AddWithValue("@NumberOfChildren", registration.NumberOfChildren);
                command.Parameters.AddWithValue("@TotalCost", registration.Price);
                connection.Open();
                command.ExecuteNonQuery();
            }

            UpdateActivityCapacity(registration.Activity.ActivityId,
                registration.NumberOfAdults + registration.NumberOfChildren);
        }

        catch (Exception e) {
            throw new RegistrationRepositoryException("AddRegistration failed", e);
        }
    }

    private bool HasEnoughCapacity(int activityId, int numberOfParticipants) {
        string query = "SELECT Capacity FROM Activity WHERE ActivityID = @ActivityID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn)) {
            cmd.Parameters.AddWithValue("@ActivityID", activityId);
            conn.Open();

            int currentCapacity = (int)cmd.ExecuteScalar();

            return currentCapacity >= numberOfParticipants;
        }
    }

    private void UpdateActivityCapacity(int activityId, int numberOfParticipants) {
        string query = "UPDATE Activity SET Capacity = Capacity - @NumberOfParticipants WHERE ActivityID = @ActivityID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn)) {
            cmd.Parameters.AddWithValue("@NumberOfParticipants", numberOfParticipants);
            cmd.Parameters.AddWithValue("@ActivityID", activityId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}