using System.Windows;
using Hotel.BL.Managers;
using Hotel.Util;

namespace Hotel.Presentation.Organizer; 

public partial class ActivityWindow : Window {
    private ActivityManager activityManager;
    public ActivityWindow()
    {
        InitializeComponent();
        activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
        ;           RefreshActivities();
    }

    private void SearchButton_Click(object sender, object e)
    {
        //show activities that match the search
        ActivitiesDataGrid.ItemsSource = activityManager.GetActivities(SearchTextBox.Text);
    }

    private void RefreshActivities()
    {
        //show all activities
        ActivitiesDataGrid.ItemsSource = activityManager.GetActivities("");
    }

    private void ShowAllButton_Click(object sender, RoutedEventArgs e)
    {
        RefreshActivities();
    }
}
