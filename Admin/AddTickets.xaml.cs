using Event_App.Admin;
using System;
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
            AdminWindow adminWindow = new();
            adminWindow.Show();
            this.Close();
        }

        private void Add_Ticket(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TypeTextBox.Text) || string.IsNullOrWhiteSpace(EventTextBox.Text) || !DateTextBox.SelectedDate.HasValue)
            {
                MessageBox.Show("Please fill in all fields", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime date = DateTextBox.SelectedDate.Value;
            int hours = HourTime.Value ?? 0;
            int minutes = MinutesTime.Value ?? 0;
            TimeSpan time = new(hours, minutes, 0);
            string TicketType = TicketType_Combo.Text;
            if (string.IsNullOrWhiteSpace(TicketType)) TicketType = "Standard";
            int nrTickets = TicketNumber.Value ?? 0;

            if (nrTickets == 0)
            {
                MessageBox.Show("Invalid ticket number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(PriceTextBox.Text, out int price) || price <= 0)
            {
                MessageBox.Show("Invalid price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime eventDateTime = date.Date + time;
            var ticket = new Ticket(
                TypeTextBox.Text,
                EventTextBox.Text,
                LocationTextBox.Text,
                eventDateTime,
                TicketType,
                price,
                nrTickets
            );
            try
            {
                bool success = TicketRepository.AddOrUpdateTicket(ticket);
                if (success)
                {
                    MessageBox.Show("Ticket added/updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    TypeTextBox.Clear();
                    EventTextBox.Clear();
                    LocationTextBox.Clear();
                    DateTextBox.SelectedDate = null;
                    HourTime.Value = null;
                    MinutesTime.Value = null;
                    TicketType_Combo.SelectedIndex = -1;
                    TicketNumber.Value = null;
                }
                else
                {
                    MessageBox.Show("An error occurred while adding/updating the ticket.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DataBase error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
