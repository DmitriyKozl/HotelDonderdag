using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel.BL.Managers;
using Hotel.BL.Model;
using Hotel.Util;

namespace Hotel.Presentation.Organizer;

public partial class AddActivity : Window {
    private ActivityManager _activityManager;
    public List<string> Hours { get; }
    public List<string> Minutes { get; }

    public AddActivity() {
        Hours = Enumerable.Range(0, 24).Select(i => i.ToString("D2")).ToList();
        Minutes = new List<string>
        { "00", "15", "30", "45" };
        InitializeComponent();
        HoursComboBox.ItemsSource = Hours;
        MinutesComboBox.ItemsSource = Minutes;
        _activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
    }

    private void AddActivityButton_Click(object sender, RoutedEventArgs e) {
        if (NameTextBox.Text == "" || DescriptionTextBox.Text == "" || DatePicker.SelectedDate == null ||
            HoursComboBox.SelectedItem == null || MinutesComboBox.SelectedItem == null || DurationTextBox.Text == "" ||
            AvailablePlacesTextBox.Text == "" || PriceAdultTextBox.Text == "" || PriceChildTextBox.Text == "" ||
            LocationTextBox.Text == "") {
            MessageBox.Show("Please fill all fields!");
            return;
        }

        string selectedHour = HoursComboBox.SelectedItem as string;
        string selectedMinute = MinutesComboBox.SelectedItem as string;
        if (selectedHour == null || selectedMinute == null) {
            MessageBox.Show("Please select a valid time!");
            return;
        }

        // Get values from the input fields
        string name = NameTextBox.Text;
        string description = DescriptionTextBox.Text;
        DateTime  date = (DatePicker.SelectedDate ?? DateTime.Now)
            .AddHours(Convert.ToInt32(selectedHour))
            .AddMinutes(Convert.ToInt32(selectedMinute));
           
        int duration = Convert.ToInt32(DurationTextBox.Text);
        int availablePlaces = Convert.ToInt32(AvailablePlacesTextBox.Text);
        decimal priceAdult = Convert.ToDecimal(PriceAdultTextBox.Text);
        decimal priceChild = Convert.ToDecimal(PriceChildTextBox.Text);
        decimal discount = Convert.ToDecimal(DiscountTextBox.Text);
        string location = LocationTextBox.Text;

        // Create the new Activity object
        Activity newActivity = new Activity( name, description, date, duration, availablePlaces, priceAdult,
            priceChild, discount, location);

        _activityManager.AddActivity(newActivity);

        MessageBox.Show("Activity added successfully!");

        Close();
    }
}