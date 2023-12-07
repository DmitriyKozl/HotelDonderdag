using Hotel.BL.Model;

namespace Hotel.BL.Interfaces; 

public interface IActivityRepository {
    void AddActivity(Activity activity);
    List<Activity> GetActivities(string filter);
}