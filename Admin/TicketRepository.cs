using System.Data.OleDb;
using System.Windows;

namespace Event_App.Admin
{
    public static class TicketRepository
    {
        private static readonly string connectionString = App.Configuration["ConnectionStrings:DefaultConnection"];

        public static bool AddTicket(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var checkcmd = new OleDbCommand(
                "Select [ticket_id],[ticket_number] " +
                "FROM tickets " +
                "WHERE [event_type]=? AND [event_title]=? AND [location]=? AND [ticket_type]=? AND [event_datetime]=?", conn);
            checkcmd.Parameters.AddWithValue("?", ticket.Type);
            checkcmd.Parameters.AddWithValue("?", ticket.Event);
            checkcmd.Parameters.AddWithValue("?", ticket.Location);
            checkcmd.Parameters.AddWithValue("?", ticket.TicketType);
            checkcmd.Parameters.AddWithValue("?", ticket.EventDateTime);

            using var reader = checkcmd.ExecuteReader();
            if (reader.Read())
            { 
                MessageBox.Show("Ticket already exists in database.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else
            {
                using var insertCmd = new OleDbCommand(
                    "INSERT INTO tickets ([event_type], [event_title], [location], [event_datetime] ,[ticket_type], [price],[ticket_number],[availability]) " +
                    "VALUES (?,?,?,?,?,?,?,?)", conn);
                insertCmd.Parameters.AddWithValue("?", ticket.Type);
                insertCmd.Parameters.AddWithValue("?", ticket.Event);
                insertCmd.Parameters.AddWithValue("?", ticket.Location);
                insertCmd.Parameters.AddWithValue("?", ticket.EventDateTime);
                insertCmd.Parameters.AddWithValue("?", ticket.TicketType);
                insertCmd.Parameters.AddWithValue("?", ticket.Price);
                insertCmd.Parameters.AddWithValue("?", ticket.AvailableTickets);
                insertCmd.Parameters.AddWithValue("?", true);
                return insertCmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool UpdateTicket(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "UPDATE tickets " +
                "SET [event_type]=?, [event_title]=?, [location]=?, [event_datetime]=?, [ticket_type]=?, [price]=?, [ticket_number]=?, [availability]=? " +
                "WHERE [ticket_id]=?", conn);
            cmd.Parameters.AddWithValue("?", ticket.Type);
            cmd.Parameters.AddWithValue("?", ticket.Event);
            cmd.Parameters.AddWithValue("?", ticket.Location);
            cmd.Parameters.AddWithValue("?", ticket.EventDateTime);
            cmd.Parameters.AddWithValue("?", ticket.TicketType);
            cmd.Parameters.AddWithValue("?", ticket.Price);
            cmd.Parameters.AddWithValue("?", ticket.AvailableTickets);
            cmd.Parameters.AddWithValue("?", ticket.IsAvailable);
            cmd.Parameters.AddWithValue("?", ticket.Id);
            return cmd.ExecuteNonQuery() > 0;
        }
        public static bool AddTicketToFavorites(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using (var checkcmd = new OleDbCommand(
                "Select count(*) " +
                "FROM FavoriteEvents " +
                "WHERE [ticket_id]=? AND [user_id]=?", conn))
            {
                checkcmd.Parameters.AddWithValue("?", ticket.Id);
                checkcmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);

                int count = Convert.ToInt32(checkcmd.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Ticket already in favorites.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            using (var insertCmd = new OleDbCommand(
                     "INSERT INTO FavoriteEvents ([user_id], [ticket_id], [date_added]) " +
                     "VALUES (?,?,?)", conn))
            {
                insertCmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
                insertCmd.Parameters.AddWithValue("?", ticket.Id);
                insertCmd.Parameters.AddWithValue("?", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                return insertCmd.ExecuteNonQuery() > 0;

            }
        }
        public static bool PurchaseTicket(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using (var updatecmd = new OleDbCommand(
                    "Update Tickets " +
                    "Set [ticket_number] = [ticket_number] - 1 " +
                    "WHERE ticket_id = ? AND [ticket_number] > 0", conn, transaction))
                {
                    updatecmd.Parameters.AddWithValue("?", ticket.Id);
                    int rowsAffected = updatecmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Ticket is sold out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        transaction.Rollback();
                        return false;
                    }
                }
                using (var updateMoneyCmd = new OleDbCommand(
                    "Update users " +
                    "Set [money] = [money] - ? " +
                    "Where user_id = ? AND [money] >= ?", conn, transaction))
                {
                    updateMoneyCmd.Parameters.AddWithValue("?", ticket.Price);
                    updateMoneyCmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
                    updateMoneyCmd.Parameters.AddWithValue("?", ticket.Price);

                    int rowsAffected = updateMoneyCmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Insufficient funds to complete the purchase.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        transaction.Rollback();
                        return false;
                    }
                }
                using (var insertcmd = new OleDbCommand(
                    "Insert INTO PurchasedEvents ([ticket_id], [user_id], [date_added]) " +
                    "VALUES (?,?,?)", conn, transaction))
                {
                    insertcmd.Parameters.AddWithValue("?", ticket.Id);
                    insertcmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
                    insertcmd.Parameters.AddWithValue("?", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    insertcmd.ExecuteNonQuery();
                }
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error processing purchase.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                transaction.Rollback();
                return false;
            }
        }
        public static List<Ticket> GetAllTickets()
        {
            var tickets = new List<Ticket>();
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand("SELECT [t.ticket_id],[t.event_type], [t.event_title], [t.location], [t.event_datetime], [t.ticket_type],[t.price], [t.ticket_number],[t.availability] FROM tickets as t", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tickets.Add(MapReaderToTicket(reader));
            }
            return tickets;
        }
        public static List<Ticket> GetAllFavoriteTickets()
        {
            var tickets = new List<Ticket>();
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [t.ticket_id], [t.event_type], [t.event_title], [t.location], [t.event_datetime], [t.ticket_type],[t.price], [t.ticket_number],[t.availability],[fe.date_added] " +
                "FROM tickets AS t INNER JOIN FavoriteEvents AS fe ON t.ticket_id = fe.ticket_id "+
                "WHERE fe.user_id = ?", conn);
            cmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                tickets.Add(MapReaderToTicket(reader));
            }
            return tickets;
        }
        public static List<Ticket> GetAllPurchasedTickets()
        {
            var tickets = new List<Ticket>();
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "SELECT [t.ticket_id], [t.event_type], [t.event_title], [t.location], [t.event_datetime], [t.ticket_type],[t.price], [t.ticket_number],[t.availability],[pe.date_added] " +
                "FROM tickets AS t INNER JOIN PurchasedEvents AS pe ON t.ticket_id = pe.ticket_id " +
                "WHERE pe.user_id = ?", conn);
            cmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tickets.Add(MapReaderToTicket(reader));
            }
            return tickets;
        }
        private static Ticket MapReaderToTicket(OleDbDataReader reader)
        {
            return new Ticket(Convert.ToInt32(reader["t.ticket_id"]),
                    reader["t.event_type"].ToString(),
                    reader["t.event_title"].ToString(),
                    reader["t.location"].ToString(),
                    Convert.ToDateTime(reader["t.event_datetime"]),
                    reader["t.ticket_type"].ToString(),
                    Convert.ToInt32(reader["t.price"]),
                    Convert.ToInt32(reader["t.ticket_number"]),
                    Convert.ToBoolean(reader["t.availability"]));
        }
        public static bool DeleteTicket(Ticket ticket)
        {
            using var conn = new OleDbConnection(connectionString);
            conn.Open();
            using var cmd = new OleDbCommand(
                "DELETE FROM tickets " +
                "WHERE [ticket_id]=?", conn);
            cmd.Parameters.AddWithValue("?", ticket.Id);
            try {
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot delete ticket that is in use.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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
                using (var updateMoneyCmd = new OleDbCommand(
                   "Update users " +
                   "Set [money] = [money] + ? " +
                   "Where user_id = ?", conn, transaction))
                {
                    updateMoneyCmd.Parameters.AddWithValue("?", ticket.Price);
                    updateMoneyCmd.Parameters.AddWithValue("?", UserSession.CurrentUser.Id); 

                    int rowsAffected = updateMoneyCmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    { 
                        MessageBox.Show("Error processing refund.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        transaction.Rollback();
                        return false;
                    }
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
                MessageBox.Show("Error processing refund.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                transaction.Rollback();
                return false;
            }
        }
    }
}
