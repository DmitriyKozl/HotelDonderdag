﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotel.BL.Managers;
using Hotel.BL.Model;
using Hotel.Domain.Managers;
using Hotel.Util;


namespace Hotel.Presentation.Register {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private RegistrationManager registrationManager;
        private CustomerManager customerManager;
        private ActivityManager activityManager;
        private MemberManager memberManager;
        private Registration registration;
        private Customer customer;
        private Activity activity;
        private List<Member> members;

        public MainWindow() {
            InitializeComponent();
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);
            CustomerComboBox.ItemsSource = customerManager.GetCustomers(null);
            ActivitiesComboBox.ItemsSource = activityManager.GetActivities(null);
            customer = new Customer(
            );
            customer.Members = new List<Member>();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e) {
            if (CustomerComboBox.SelectedItem == null || ActivitiesComboBox.SelectedItem == null) {
                MessageBox.Show("Please fill all fields");
                return;
            }

            registrationManager.AddRegistration(registration);
            MessageBox.Show("Registration completed successfully");
            Close();
        }

        private void UpdateRegistrationDetails() {
            registration = new Registration(customer, activity);

            if (activity.Discount == null || activity.Discount == 0) {
                SubtotalAdultsTextBlock.Text =
                    registration.costAdult.ToString() + $" {registration.NumberOfAdults} adults";
                SubtotalChildrenTextBlock.Text =
                    registration.costChild.ToString() + $" {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = " ";
            }
            else {
                SubtotalAdultsTextBlock.Text =
                    $"{registration.costAdult.ToString()} ({activity.PriceAdult * registration.NumberOfAdults} per adult) {registration.NumberOfAdults} adults";
                SubtotalChildrenTextBlock.Text =
                    $"{registration.costChild.ToString()} ({activity.PriceChild * registration.NumberOfChildren}) {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = $"Discount: {activity.Discount}%";
            }

            TotalCostTextBlock.Text = registration.Price.ToString();
        }

        private void MemberCheckBox_Checked(object sender, RoutedEventArgs e) {
            var checkBox = sender as CheckBox;
            var member = checkBox.Tag as Member;
            if (member != null) {
                customer.Members.Add(member);
            }

            UpdateRegistrationDetails();
        }

        private void MemberCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            var checkBox = sender as CheckBox;
            var member = checkBox.Tag as Member;
            if (member != null) {
                customer.Members.Remove(member);
            }

            UpdateRegistrationDetails();
        }

        private void CustomerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            members = new List<Member>();
            customer = CustomerComboBox.SelectedItem as Customer;
            if (customer != null) {
                members = memberManager.GetMembers(customer.Id);
                MembersCheckboxes.ItemsSource = members; // Bind the checkboxes to the members list
            }
        }

        private void ActivitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            MembersCheckboxes.IsEnabled = true;
            // MembersListBox.SelectedItems.Clear();
            activity = ActivitiesComboBox.SelectedItem as Activity;
            DateTextBlock.Text = activity.Date.ToString();
            LocationTextBlock.Text = activity.Location;
            AvailableSeatsTextBlock.Text = activity.Capacity.ToString();
            customer.Members = new List<Member>();
            registration = new Registration(customer, activity);

            if (activity.Discount == null || activity.Discount == 0) {
                SubtotalAdultsTextBlock.Text = registration.costAdult.ToString() +
                                               $"       {registration.NumberOfAdults} adult(s)";
                SubtotalChildrenTextBlock.Text = registration.costChild.ToString() +
                                                 $"       {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = " ";
                TotalCostTextBlock.Text = registration.Price.ToString();
            }
            else {
                SubtotalAdultsTextBlock.Text = registration.costAdult +
                                               $" ({activity.PriceAdult * registration.NumberOfAdults})       {registration.NumberOfAdults} adult(s)";
                SubtotalChildrenTextBlock.Text = registration.costChild +
                                                 $" ({activity.PriceChild * registration.NumberOfChildren})      {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = $"Discount: {activity.Discount}%";
                TotalCostTextBlock.Text = registration.Price.ToString();
            }
        }
    }
}