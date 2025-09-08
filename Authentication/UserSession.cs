
using System.Data.OleDb;
namespace Event_App
{
    public static class UserSession
    {
        public static User? CurrentUser { get; private set; }
        public static void setUser(User user)
        {
            CurrentUser = user;
        }
        public static void clearUser()
        {
            CurrentUser = null;
        }
    }
}
