using Event_App.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Event_App
{
 
    public partial class ViewPurchasedTickets : Window
    {
        public ViewPurchasedTickets()
        {
            InitializeComponent();
            LoadTickets();
        }
        private void LoadTickets()
        {
            var tickets = TicketRepository.GetAllPurchasedTickets();
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
                Content = "Refund event",
                Padding = new Thickness(5, 2, 5, 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Cursor = System.Windows.Input.Cursors.Hand
            };
            deleteButton.Click += (s, e) =>
            {
                var result = MessageBox.Show($"Are you sure you want to refund the ticket for event '{ticket.Event}'?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (TicketRepository.RefundTicket(ticket))
                    {
                        EventsPanel.Children.Clear();
                        LoadTickets();
                        MessageBox.Show("Event refunded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("Failed to refund the event.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            };

            panel.Children.Add(deleteButton);
            card.Child = panel;
            EventsPanel.Children.Add(card);
        }
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            MainPanel mainPanel = new();
            mainPanel.Show();
            this.Close();
        }
    }
}