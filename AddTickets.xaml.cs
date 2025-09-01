using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Event_App
{
    public partial class AddTickets : Window
    {
        public AddTickets()
        {
            InitializeComponent();
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            MainPanel mainPanelWindow = new();
            mainPanelWindow.Show();
            this.Close();
        }

        private void Add_Ticket(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TypeTextBox.Text) ||
                string.IsNullOrWhiteSpace(EventTextBox.Text) ||
                string.IsNullOrWhiteSpace(DateTextBox.Text) ||
                string.IsNullOrWhiteSpace(TimeTextBox.Text) ||
                string.IsNullOrWhiteSpace(Ticket.Text))
            {
                MessageBox.Show("Please fill in all fields", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!DateTime.TryParse(DateTextBox.Text, out DateTime date))
            {
                MessageBox.Show("Invalid date format.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!TimeSpan.TryParse(TimeTextBox.Text, out TimeSpan time))
            {
                MessageBox.Show("Invalid time format.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(Ticket.Text, out int availableTickets))
            {
                MessageBox.Show("Invalid ticket number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var ticket = new Ticket(
                TypeTextBox.Text,
                EventTextBox.Text,
                date,
                time,
                availableTickets
            );

            string file = "tickets.txt";
            string line = $"{ticket.Type}:{ticket.Event}:{ticket.Date:dd-mm-yyyy}:{ticket.Time:c}:{ticket.AvailableTickets}";
            File.AppendAllLines(file, new List<string> { line });
            MessageBox.Show("Ticket added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class Ticket
    {
        public string Type { get; set; }
        public string Event { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int AvailableTickets { get; set; }

        public Ticket(string type, string eventName, DateTime date, TimeSpan time, int availableTickets)
        {
            Type = type;
            Event = eventName;
            Date = date;
            Time = time;
            AvailableTickets = availableTickets;
        }
    }
}
