using System.Data.OleDb;

namespace Event_App.Admin
{
    public static class TicketRepository
    {
        private static readonly string connectionString = App.Configuration["ConnectionStrings:DefaultConnection"];

        public static bool AddOrUpdateTicket(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var checkcmd = new OleDbCommand(
                "Select ticket_number " +
                "FROM tickets " +
                "WHERE [event_type]=? AND [event_title]=? AND [location]=? AND [ticket_type]=? and [event_datetime]=?", conn);
            checkcmd.Parameters.AddWithValue("?", ticket.Type);
            checkcmd.Parameters.AddWithValue("?", ticket.Event);
            checkcmd.Parameters.AddWithValue("?", ticket.Location);
            checkcmd.Parameters.AddWithValue("?", ticket.TicketType);
            checkcmd.Parameters.AddWithValue("?", ticket.EventDateTime);

            var existing = checkcmd.ExecuteScalar();

            if (existing != null)
            {
                int current = Convert.ToInt32(existing);
                int updated = current + ticket.AvailableTickets;
                using var updateCmd = new OleDbCommand(
                    "UPDATE Tickets " +
                    "SET [ticket_number]=? " +
                    "WHERE [event_type]=? AND [event_title]=? AND [location]=? AND [ticket_type]=? AND [event_datetime]=?", conn);
                updateCmd.Parameters.AddWithValue("?", updated);
                updateCmd.Parameters.AddWithValue("?", ticket.Type);
                updateCmd.Parameters.AddWithValue("?", ticket.Event);
                updateCmd.Parameters.AddWithValue("?", ticket.Location);
                updateCmd.Parameters.AddWithValue("?", ticket.TicketType);
                updateCmd.Parameters.AddWithValue("?", ticket.EventDateTime);
                return updateCmd.ExecuteNonQuery() > 0;
            }
            else
            {
                using var insertCmd = new OleDbCommand(
                    "INSERT INTO tickets ([event_type], [event_title], [location], [event_datetime] ,[ticket_type], [price],[ticket_number]) " +
                    "VALUES (?,?,?,?,?,?,?)", conn);
                insertCmd.Parameters.AddWithValue("?", ticket.Type);
                insertCmd.Parameters.AddWithValue("?", ticket.Event);
                insertCmd.Parameters.AddWithValue("?", ticket.Location);
                insertCmd.Parameters.AddWithValue("?", ticket.EventDateTime);
                insertCmd.Parameters.AddWithValue("?", ticket.TicketType);
                insertCmd.Parameters.AddWithValue("?", ticket.Price);
                insertCmd.Parameters.AddWithValue("?", ticket.AvailableTickets);
                return insertCmd.ExecuteNonQuery() > 0;
            }
        }
        public static List<Ticket> GetAllTickets()
        {
            var tickets = new List<Ticket>();
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand("SELECT [event_type], [event_title], [location], [event_datetime], [ticket_type],[price], [ticket_number] FROM tickets", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               
                var ticket = new Ticket(
                    reader["event_type"].ToString(),
                    reader["event_title"].ToString(),
                    reader["location"].ToString(),
                    Convert.ToDateTime(reader["event_datetime"]),
                    reader["ticket_type"].ToString(),
                    Convert.ToInt32(reader["price"]),
                    Convert.ToInt32(reader["ticket_number"])
                );
                tickets.Add(ticket);
            }
            return tickets;
        }

        public static bool DeleteTicket(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "DELETE FROM tickets " +
                "WHERE [event_type]=? AND [event_title]=? AND [location]=? AND [ticket_type]=? AND [event_datetime]=?", conn);
            cmd.Parameters.AddWithValue("?", ticket.Type);
            cmd.Parameters.AddWithValue("?", ticket.Event);
            cmd.Parameters.AddWithValue("?", ticket.Location);
            cmd.Parameters.AddWithValue("?", ticket.TicketType);
            cmd.Parameters.AddWithValue("?", ticket.EventDateTime);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
