namespace Event_App
{ 
    public class Ticket(int id, string type, string eventName, string location ,DateTime date,string TicketType, int price, int nrTickets, bool isAvailable)
    {
        public int Id { get; set; } = id;
        public string Type { get; set; } = type;
        public string Event { get; set; } = eventName;
        public string Location { get; set; } = location;
        public DateTime EventDateTime { get; set; } = date; 
   
        public string TicketType { get; set; } = TicketType;
        public int  Price { get; set; } = price;
        public int AvailableTickets { get; set; } = nrTickets;
        public bool IsAvailable { get; set; } = isAvailable;
    }
}
