using System.Data.SqlClient;
using Hotel.BL.Interfaces;
using Hotel.BL.Model;
using Hotel.DL.Exceptions;

namespace Hotel.DL.Repositories;

public class ActivityRepository : IActivityRepository {
    private string connectionString;

    public ActivityRepository(string connectionstring) {
        this.connectionString = connectionstring;
    }

    public void AddActivity(Activity activity) {
        try {
            string query =
                "INSERT INTO Activity (ActivityName, Description, Location, Schedule, Duration, Capacity, AdultPrice, ChildPrice, Discount) " +
                "VALUES (@ActivityName, @Description, @Location, @Schedule, @Duration, @Capacity, @AdultPrice, @ChildPrice, @Discount);";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try {
                    cmd.CommandText = query;
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@ActivityName", activity.Name);
                    cmd.Parameters.AddWithValue("@Description", activity.Description);
                    cmd.Parameters.AddWithValue("@Location", activity.Location);
                    cmd.Parameters.AddWithValue("@Duration", activity.Date);
                    cmd.Parameters.AddWithValue("@Schedule", activity.Duration);
                    cmd.Parameters.AddWithValue("@Capacity", activity.Capacity);
                    cmd.Parameters.AddWithValue("@AdultPrice", activity.PriceAdult);
                    cmd.Parameters.AddWithValue("@ChildPrice", activity.PriceChild);

                    cmd.Parameters.AddWithValue("@Discount", activity.Discount);


                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception) {
                    transaction.Rollback();
                }
            }
        }
        catch (Exception e) {
            throw new ActivityRepositoryException(e.Message);
        }
    }


    public List<Activity> GetActivities(string filter) {
        try {
            string query = $"SELECT * FROM Activity WHERE ActivityName LIKE '%{filter}%'";
            List<Activity> activities = new List<Activity>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand()) {
                conn.Open();
                cmd.CommandText = query;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Activity activity = new Activity(
                        Convert.ToInt32(reader["ActivityID"]),
                        reader["ActivityName"].ToString(),
                        reader["Description"].ToString(),
                        Convert.ToDateTime(reader["Schedule"]),
                        (int)((TimeSpan)reader["Duration"]).TotalMinutes, // Cast the TotalMinutes directly to int
                        Convert.ToInt32(reader["Capacity"]),
                        Convert.ToDecimal(reader["AdultPrice"]),
                        Convert.ToDecimal(reader["ChildPrice"]),
                        0,
                        reader["Location"].ToString()
                    );
                    if (reader.IsDBNull(9)) activity.Discount = 0;

                    else activity.Discount = reader.GetDecimal(9);
                    activities.Add(activity);
                }
            }

            return activities;
        }
        catch (Exception e) {
            throw new ActivityRepositoryException(e.Message);
        }
    }
}