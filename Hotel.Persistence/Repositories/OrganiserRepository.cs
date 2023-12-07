using System.Data.SqlClient;
using Hotel.BL.Interfaces;
using Hotel.BL.Model;
using HotelProject.BL.Exceptions;

namespace Hotel.Persistence.Repositories;

public class OrganiserRepository : IOrganiserRepository {
    public string connectionString { get; set; }

    public OrganiserRepository(string connectionString) {
        this.connectionString = connectionString;
    }

    public List<Organizer> GetOrganizers() {
        try {
            string query =
                $"SELECT o.OrganizerID, o.OrganizationName, o.Description, o.Email, o.Phone, a.City, a.StreetName, a.PostalCode, a.HouseNumber " +
                $"FROM Organizer o LEFT JOIN Address a ON o.AddressID = a.AddressID";
            List<Organizer> organisers = new List<Organizer>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    Organizer organizer = new Organizer
                    (
                        (int)reader["OrganizerId"],
                        (string)reader["OrganizationName"],
                        (string)reader["Description"],
                        new ContactInfo
                        (
                            (string)reader["Email"],
                            (string)reader["Phone"],
                            new Address(
                                reader["City"] as string, // Using 'as' to handle possible DBNull values
                                reader["StreetName"] as string,
                                reader["PostalCode"] as string,
                                reader["HouseNumber"] as string
                            )
                        )
                    );
                    organisers.Add(organizer);
                }

                connection.Close();

                return organisers;
            }
        }
        catch (Exception ex) {
            throw new OrganizerException("GetOrganizers failed", ex);
        }
    }
}