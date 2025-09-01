using System.IO;
using System.Windows;

namespace Event_App
{
    public partial class AddTickets : Window
    {
        private readonly User? _user;
        public AddTickets(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new(_user!);
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
            TimeSpan time = new (hours, minutes,0);

            int nrTickets = TicketNumber.Value ?? 0;

            if (nrTickets == 0)
            {
                MessageBox.Show("Invalid ticket number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ticket = new Ticket(
                TypeTextBox.Text, 
                EventTextBox.Text,
                date,
                time,
                nrTickets
            );

            string file = "tickets.txt";
            List<string> lines = File.Exists(file) ? File.ReadAllLines(file).ToList() : new List<string>();
            bool ticketsExists = false;

            
            for(int i = 0; i < lines.Count; ++i)
            {
                var parts = lines[i].Split(',');
                if(parts.Length >=5 && parts[1] == ticket.Event
                    && DateTime.TryParseExact (parts[2], "dd-MM-yyyy",null,
                    System.Globalization.DateTimeStyles.None, out DateTime existingDate) &&
                    TimeSpan.TryParse(parts[3], out TimeSpan existingTime)
                    && existingDate == ticket.Date && existingTime == ticket.Time)
                {
                    int existingTickets = int.TryParse(parts[4], out int temp) ? temp : 0;
                    int updatedTickets = existingTickets + ticket.AvailableTickets;
                    lines[i] = $"{parts[0]},{parts[1]},{parts[2]},{parts[3]},{updatedTickets}";
                    ticketsExists = true;

                   
                    break;
                }
            }
            if (!ticketsExists)
            {
                string timeStr = ticket.Time.ToString(@"hh\:mm");
                string line = $"{ticket.Type},{ticket.Event},{ticket.Date:dd-MM-yyyy},{timeStr},{ticket.AvailableTickets}";
                lines.Add(line);
                MessageBox.Show("Tickets added successfully.");
            }
            else MessageBox.Show("Number of tickets updated successfully.");
            File.WriteAllLines(file, lines);
        }
    }

    
}
