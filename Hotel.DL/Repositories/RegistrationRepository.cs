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
        }

        catch (Exception e) {
            throw new RegistrationRepositoryException("AddRegistration failed", e);
        }
    }
}