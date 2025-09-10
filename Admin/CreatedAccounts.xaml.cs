using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Event_App
{ 
    public partial class CreatedAccounts : Window
    {
        public CreatedAccounts()
        {
            InitializeComponent();
            LoadUsers();
        }
        public void LoadUsers()
        {
            var users = UserRepository.GetAllUsers();
            if (users.Count == 0)
            {
                Accounts.Children.Add(new TextBlock
                {
                    Text = "No users available.",
                    FontSize = 16,
                    Foreground = Brushes.Gray,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(10)
                });
                return;
            }
            foreach (var user in users)
            {
                AddUserCard(user);
            }
        }
        private void AddUserCard(User user)
        {
            Border border = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10),
                Padding = new Thickness(10),
                Background = Brushes.White,
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.Gray,
                    Direction = 320,
                    ShadowDepth = 2,
                    Opacity = 0.5
                }
            };
            StackPanel stackPanel = new StackPanel();
            TextBlock usernameText = new TextBlock
            {
                Text = $"Username: {user.Username}",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black
            };
            TextBlock emailText = new TextBlock
            {
                Text = $"Email: {user.Email}",
                FontSize = 14,
                Foreground = Brushes.DarkGray,
                Margin = new Thickness(0, 5, 0, 0)
            };
            TextBlock roleText = new TextBlock
            {
                Text = $"Role: {user.Role}",
                FontSize = 14,
                Foreground = Brushes.DarkGray,
                Margin = new Thickness(0, 5, 0, 0)
            };
            stackPanel.Children.Add(usernameText);
            stackPanel.Children.Add(emailText);
            stackPanel.Children.Add(roleText);
            border.Child = stackPanel;
            Accounts.Children.Add(border);
           
        }
        public void Back_Button(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new();
            adminWindow.Show();
            this.Close();
        }
    }
}
