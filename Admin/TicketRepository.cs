using System.Data.OleDb;
using System.Windows;

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
                "Select [ticket_id],[ticket_number] " +
                "FROM tickets " +
                "WHERE [event_type]=? AND [event_title]=? AND [location]=? AND [ticket_type]=? and [event_datetime]=?", conn);
            checkcmd.Parameters.AddWithValue("?", ticket.Type);
            checkcmd.Parameters.AddWithValue("?", ticket.Event);
            checkcmd.Parameters.AddWithValue("?", ticket.Location);
            checkcmd.Parameters.AddWithValue("?", ticket.TicketType);
            checkcmd.Parameters.AddWithValue("?", ticket.EventDateTime);

            using var reader = checkcmd.ExecuteReader();
            if (reader.Read())
            { 
                int ticketid = Convert.ToInt32(reader["ticket_id"]); 
                int current = Convert.ToInt32(reader["ticket_number"]);
                int updated = current + ticket.AvailableTickets;
                using var updateCmd = new OleDbCommand(
                    "UPDATE Tickets " +
                    "SET [ticket_number]=? " +
                    "WHERE [ticket_id]=?", conn);
                updateCmd.Parameters.AddWithValue("?", updated);
                updateCmd.Parameters.AddWithValue("?", ticketid);
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
            using var cmd = new OleDbCommand("SELECT [ticket_id],[event_type], [event_title], [location], [event_datetime], [ticket_type],[price], [ticket_number] FROM tickets", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                var ticket = new Ticket(
                    Convert.ToInt32(reader["ticket_id"]),
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
        public static List<Ticket> GetAllFavoriteTickets()
        {
            var tickets = new List<Ticket>();
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [t.ticket_id], [t.event_type], [t.event_title], [t.location], [t.event_datetime], [t.ticket_type],[t.price], [t.ticket_number],[fe.date_added] " +
                "FROM tickets AS t INNER JOIN FavoriteEvents AS fe ON t.ticket_id = fe.ticket_id "+
                "WHERE fe.user_id = ?", conn);
            cmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                var ticket = new Ticket(
                    Convert.ToInt32(reader["t.ticket_id"]),
                    reader["t.event_type"].ToString(),
                    reader["t.event_title"].ToString(),
                    reader["t.location"].ToString(),
                    Convert.ToDateTime(reader["t.event_datetime"]),
                    reader["t.ticket_type"].ToString(),
                    Convert.ToInt32(reader["t.price"]),
                    Convert.ToInt32(reader["t.ticket_number"])
                );
                tickets.Add(ticket);
            }
            return tickets;
        }
        public static List<Ticket> GetAllPurchasedTickets()
        {
            var tickets = new List<Ticket>();
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [t.ticket_id], [t.event_type], [t.event_title], [t.location], [t.event_datetime], [t.ticket_type],[t.price], [t.ticket_number],[pe.date_added] " +
                "FROM tickets AS t INNER JOIN PurchasedEvents AS pe ON t.ticket_id = pe.ticket_id " +
                "WHERE pe.user_id = ?", conn);
            cmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                var ticket = new Ticket(
                    Convert.ToInt32(reader["t.ticket_id"]),
                    reader["t.event_type"].ToString(),
                    reader["t.event_title"].ToString(),
                    reader["t.location"].ToString(),
                    Convert.ToDateTime(reader["t.event_datetime"]),
                    reader["t.ticket_type"].ToString(),
                    Convert.ToInt32(reader["t.price"]),
                    Convert.ToInt32(reader["t.ticket_number"])
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
                "WHERE [ticket_id]=?", conn);
            cmd.Parameters.AddWithValue("?", ticket.Id);
            return cmd.ExecuteNonQuery() > 0;
        }
        public static bool DeleteTicketFromFavorite(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "DELETE " +
                "FROM FavoriteEvents " +
                "Where [ticket_id] = ? AND [user_id] = ?", conn);
            cmd.Parameters.AddWithValue("?", ticket.Id);
            cmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
            return cmd.ExecuteNonQuery() > 0;
        }
        public static bool RefundTicket(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using (var updatecmd = new OleDbCommand(
                    "Update Tickets " +
                    "Set [ticket_number] = [ticket_number] + 1 " +
                    "WHERE ticket_id = ?", conn, transaction)) 
                {
                    updatecmd.Parameters.AddWithValue("?", ticket.Id);
                    updatecmd.ExecuteNonQuery();
                }
                using (var deletecmd = new OleDbCommand(
                    "Delete " +
                    "From purchasedEvents "+
                    "Where [ticket_id] = ? AND [user_id] = ?", conn,transaction))
                {
                    deletecmd.Parameters.AddWithValue("?", ticket.Id);
                    deletecmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
                    deletecmd.ExecuteNonQuery();
                }
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
