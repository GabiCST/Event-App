using System.Windows; 
namespace Event_App
{ 
    public partial class UpdateTicketInfo : Window
    {
        private Ticket _ticket; 
        public UpdateTicketInfo(Ticket ticket)
        {
            InitializeComponent();
            _ticket = ticket;
            EventTypeBox.Text = ticket.Type;
            EventBox.Text = ticket.Event;
            LocationBox.Text = ticket.Location;
            DateBox.Text = ticket.EventDateTime.ToString("dd-MM-yyyy HH:mm");
            TicketTypeBox.Text = ticket.TicketType.ToString();
            PriceBox.Text = ticket.Price.ToString();
            TicketNumberBox.Text = ticket.AvailableTickets.ToString();
        }
        public void Save_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                _ticket.Type = EventTypeBox.Text.Trim();
                _ticket.Event = EventBox.Text.Trim();
                _ticket.Location = LocationBox.Text.Trim();
                if (!DateTime.TryParseExact(DateBox.Text.Trim(), "dd-MM-yyyy HH:mm",
                        null, System.Globalization.DateTimeStyles.None, out DateTime datetime))
                {
                    MessageBox.Show("Invalid date format. Use dd-MM-yyyy HH:mm","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                }
                _ticket.EventDateTime = datetime;
                if(!int.TryParse(PriceBox.Text.Trim(),out int price))
                {
                    MessageBox.Show("Price must be an number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _ticket.Price = price;
                if (!int.TryParse(TicketNumberBox.Text.Trim(), out int ticketnumber))
                {
                    MessageBox.Show("Price must be an number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _ticket.AvailableTickets = ticketnumber;
                bool success = Admin.TicketRepository.UpdateTicket(_ticket);
                if(success)
                {
                    MessageBox.Show("Ticket updated successfully.","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                    if(Application.Current.Windows.OfType<TicketManager>().FirstOrDefault() is TicketManager ticketManager)
                    {
                        ticketManager.RefreshTickets();
                    }
                    this.Close();
                   
                }
                else MessageBox.Show("Failde to update ticket", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public void Cancel_button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

         
    }
} 