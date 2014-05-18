using System.Windows;
using Chicken4WP.Models;
using Microsoft.Phone.Controls;

namespace Chicken4WP
{
    public partial class App : Application
    {
        public static PhoneApplicationFrame RootFrame { get; private set; }

        public App()
        {
            InitializeComponent();
        }

        public static User CurrentUser { get; private set; }

        public static void UpdateCurrentUser(User user)
        {
            CurrentUser = user;
        }
    }
}