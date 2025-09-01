using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_App
{ 
    public class Ticket(string type, string eventName, DateTime date, TimeSpan time, int nrTickets)
    {
        public string Type { get; set; } = type;
        public string Event { get; set; } = eventName;
        public DateTime Date { get; set; } = date;
        public TimeSpan Time { get; set; } = time;
        public int AvailableTickets { get; set; } = nrTickets;

    }
}
