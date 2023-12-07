using System.Windows;
using System.Windows.Controls;
using Hotel.BL.Managers;
using Hotel.Util;
using Hotel.DL.Repositories;

namespace Hotel.Presentation.Organizer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private OrganizerManager _organizer;

        public MainWindow() {
            InitializeComponent();
            _organizer = new OrganizerManager(RepositoryFactory.OrganizerRepository);

            DataContext = new
            { Organizers = _organizer.GetOrganizers() };
        }

        private void OrganizerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedOrganizer = (BL.Model.Organizer)OrganizerComboBox.SelectedItem;
            SelectedOrganizerTextBlock.Text = "Selected Organizer: " + (selectedOrganizer?.Name ?? "");
            WelcomeTextBlock.Text = "Welcome " + (selectedOrganizer?.Name ?? "") + "!";
            if (selectedOrganizer != null) {
                button1.IsEnabled = true;
                button2.IsEnabled = true;
            }
        }
        
        private void ViewActivities_Click(object sender, RoutedEventArgs e) {
            ActivityWindow viewActivitiesWindow = new ActivityWindow();
            viewActivitiesWindow.Show();
        }
        
        private void AddNewActivity_Click(object sender, RoutedEventArgs e) {
            AddActivity addNewActivityWindow = new AddActivity();
            addNewActivityWindow.Show();
        }
    }
}