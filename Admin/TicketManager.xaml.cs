using Event_App.Admin;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Event_App
{
    public partial class TicketManager : Window
    {
        public TicketManager()
        {
            InitializeComponent();
            LoadTickets();
        }
        private void LoadTickets()
        {
            var tickets = TicketRepository.GetAllTickets();
            if (tickets.Count == 0)
            {
                EventsPanel.Children.Add(new TextBlock
                {
                    Text = "No events available.",
                    FontSize = 16,
                    Foreground = Brushes.Gray,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(10)
                });
                return;
            }
            foreach (var ticket in tickets)
            {
                AddEventCard(ticket);
            }
        }
        private void AddEventCard(Ticket ticket)
        {
            Border card = new()
            {
                BorderBrush = Brushes.DarkGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                Background = Brushes.White,
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.Black,
                    Direction = 320,
                    ShadowDepth = 5,
                    Opacity = 0.25
                }
            };

            StackPanel panel = new() { Orientation = Orientation.Vertical };
            panel.Children.Add(new TextBlock
            {
                Text = $"{ticket.Type} - {ticket.Event}",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            });

            panel.Children.Add(new TextBlock
            {
                Text = $"Location: {ticket.Location}",
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 2)
            });
            panel.Children.Add(new TextBlock
            {
                Text = $"Date & time: {ticket.EventDateTime:dd.MM.yyyy HH:mm}",
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 2)
            });

            panel.Children.Add(new TextBlock
            {
                Text = $"Ticket Type: {ticket.TicketType}",
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 2)
            });
            panel.Children.Add(new TextBlock
            {
                Text = $"Price: ${ticket.Price}",
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 10)
            });
            Button deleteButton = new()
            {
                Content = "Delete Event",
                Padding = new Thickness(5, 2, 5, 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Cursor = System.Windows.Input.Cursors.Hand
            };
            deleteButton.Click += (s, e) =>
            {
                var result = MessageBox.Show($"Are you sure you want to delete the event '{ticket.Event}'?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (TicketRepository.DeleteTicket(ticket))
                    {
                        EventsPanel.Children.Clear();
                        LoadTickets();
                        MessageBox.Show("Event deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the event.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            };
            Button ticketchanger = new()
            {
                Content = "Change Ticket Info",
                Padding = new Thickness(5, 2, 5, 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Cursor = System.Windows.Input.Cursors.Hand,
                Margin = new Thickness(0,0,0,10)
            };
            ticketchanger.Click += (s, e) =>
            {
                UpdateTicketInfo update = new(ticket);
                update.Show();
            };
            panel.Children.Add(ticketchanger);
            panel.Children.Add(deleteButton);
            card.Child = panel;
            EventsPanel.Children.Add(card);
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new();
            adminWindow.Show();
            this.Close();
        }
        public void RefreshTickets()
        {
            EventsPanel.Children.Clear();
            LoadTickets();
        }
    }
}
