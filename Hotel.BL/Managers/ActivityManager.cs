using Hotel.BL.Interfaces;
using Hotel.BL.Model;

namespace Hotel.BL.Managers; 

public class ActivityManager : IActivityRepository{
    
    IActivityRepository _activityRepository;
    
    public ActivityManager(IActivityRepository activityRepository){
        _activityRepository = activityRepository;
    }
    
    public void AddActivity(Activity activity){
        _activityRepository.AddActivity(activity);
    }
    
    public List<Activity> GetActivities(string filter){
        return _activityRepository.GetActivities(filter);
    }
    
}