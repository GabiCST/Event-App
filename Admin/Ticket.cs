namespace Event_App
{ 
    public class Ticket(string type, string eventName, string location ,DateTime date,string TicketType, int price, int nrTickets)
    {
        public string Type { get; set; } = type;
        public string Event { get; set; } = eventName;
        public string Location { get; set; } = location;
        public DateTime EventDateTime { get; set; } = date; 
   
        public string TicketType { get; set; } = TicketType;
        public int  Price { get; set; } = price;
        public int AvailableTickets { get; set; } = nrTickets;

    }
}
