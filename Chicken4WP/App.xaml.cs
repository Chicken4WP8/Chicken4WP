using System.Windows;
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
    }
}