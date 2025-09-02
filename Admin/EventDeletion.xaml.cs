using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Event_App
{
    public partial class EventDeletion : Window
    {
        private readonly User? _user;
        public EventDeletion(User user)
        {
            _user = user;
            InitializeComponent();
            LoadTickets("tickets.txt");
        }
        private void LoadTickets(string file)
        {
            if (!File.Exists(file))
            {
                EventsPanel.Children.Add(new TextBlock { Text = "No events avaliable" });
                return;
            }
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 6) AddEvent(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
            }
        }
        private void AddEvent(string type, string title, string date, string time, string ticketType, string nrTickets)
        {
           Border card = new()
           {
               BorderBrush = System.Windows.Media.Brushes.Black,
               BorderThickness = new Thickness(1),
               Margin = new Thickness(5),
               Padding = new Thickness(10)
           };
           
           StackPanel panel = new();
           panel.Children.Add(new TextBlock { Text = $"Event type:{type}"});
           panel.Children.Add(new TextBlock { Text = $"Event: {title}", FontWeight = FontWeights.Bold });
           panel.Children.Add(new TextBlock { Text = $"Date: {date}" });
           panel.Children.Add(new TextBlock { Text = $"Time: {time}" });
            panel.Children.Add(new TextBlock { Text = $"Ticket type: {ticketType}" });
            panel.Children.Add(new TextBlock { Text = $"Available Tickets: {nrTickets}" });
            Button deleteButton = new Button
            {
                Content = "Delete",
                Tag = new { Type = type, Title = title, Date = date, Time = time, TicketType = ticketType, NrTickets = nrTickets }
            };
            
            var eventData = deleteButton.Tag;
            deleteButton.Click += (sender, e) =>
            {
                if (_user == null)
                {
                    MessageBox.Show("User information is missing. Cannot delete events.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                dynamic tag = eventData;
                var result = MessageBox.Show($"Are you sure you want to delete the event '{tag.Title}'?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes) return;
                if (deleteButton.Parent is StackPanel panel && panel.Parent is Border border)
                {
                    EventsPanel.Children.Remove(border);
                }
                string file = "tickets.txt";
                if (File.Exists(file))
                {
                    var lines = File.ReadAllLines(file).ToList();
                    string lineToRemove = $"{tag.Type},{tag.Title},{tag.Date},{tag.Time},{tag.TicketType},{tag.NrTickets}";
                    lines.RemoveAll(l => l.Trim() == lineToRemove);
                    File.WriteAllLines(file, lines);
                }
            };
            panel.Children.Add(deleteButton);
            card.Child = panel;
            EventsPanel.Children.Add(card);
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new(_user!);
            adminWindow.Show();
            this.Close();
        }
    }
}
