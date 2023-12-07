using System.Data.SqlClient;
using Hotel.BL.Interfaces;
using Hotel.BL.Model;
using Hotel.DL.Exceptions;

namespace Hotel.DL.Repositories;

public class MemberRepository : IMemberRepository {
    string connectionString;

    public MemberRepository(string connectionString) {
        this.connectionString = connectionString;
    }


    public void AddMember(Member member, int customerid) {
        try {
            string query =
                "INSERT INTO Member (name, birthday, customerId, status) " +
                "VALUES (@name, @birthday, @customerId,@status)";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", member.Name);
                command.Parameters.AddWithValue("@birthday",
                    new DateTime(member.Birthday.Year, member.Birthday.Month, member.Birthday.Day));
                command.Parameters.AddWithValue("@customerId", customerid);
                command.Parameters.AddWithValue("@status", 1);
                command.ExecuteNonQuery();

            }
        }
        catch (Exception e) {
            throw new MemberRepositoryException("AddMember failed", e);
        }
    }

    public void UpdateMember(Member member, int customerid, string name) {
        try {
            string query =
                "UPDATE Member SET Name = @Name, BirthDay = @BirthDay WHERE CustomerId = @CustomerId AND Name=@oldname";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@BirthDay",
                    new DateTime(member.Birthday.Year, member.Birthday.Month, member.Birthday.Day));
                command.Parameters.AddWithValue("@CustomerId", customerid);
                command.Parameters.AddWithValue("@oldname", name);
                command.ExecuteNonQuery();

            }
        }
        catch (Exception e) {
            throw new MemberRepositoryException("AddMember failed", e);
        }
    }

    public void DeleteMember(Member member, int customerid) {
        try {
            string query = "DELETE FROM Member WHERE CustomerId = @CustomerId AND Name=@Name";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", member.Name);
                command.Parameters.AddWithValue("@CustomerId", customerid);
                command.ExecuteNonQuery();

            }
        }
        catch (Exception e) {
            throw new MemberRepositoryException("AddMember failed", e);
        }
    }

    public List<Member> GetMembers(int customerid) {
        try {
            string query = "SELECT * FROM Member WHERE CustomerId = @CustomerId";
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = connection.CreateCommand()) {
                connection.Open();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@CustomerId", customerid);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read()) {
                        DateTime birthdayDateTime = (DateTime)reader["BirthDay"];
                        DateOnly birthdayDateOnly = DateOnly.FromDateTime(birthdayDateTime);
                        members.Add(new Member(
                            (string)reader["Name"],
                            birthdayDateOnly
                            ));
                    }            
                connection.Close();
                return members;

            }
        }
        catch (Exception e) {
            throw new MemberRepositoryException("GetMembers failed", e);
        }
    }
}