using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Chicken4WP.Services
{
    public class ProgressService
    {
        const string DefaultIndicatorText = "Loading...";
        private readonly ProgressIndicator progressIndicator;

        public ProgressService(PhoneApplicationFrame rootFrame)
        {
            progressIndicator = new ProgressIndicator { Text = DefaultIndicatorText };

            rootFrame.Navigated += RootFrameOnNavigated;
        }

        private void RootFrameOnNavigated(object sender, NavigationEventArgs args)
        {
            var content = args.Content;
            var page = content as PhoneApplicationPage;
            if (page == null)
                return;

            page.SetValue(SystemTray.ProgressIndicatorProperty, progressIndicator);
        }

        public void Show()
        {
            Show(DefaultIndicatorText);
        }

        public void Show(string text)
        {
            progressIndicator.Text = text;
            progressIndicator.IsIndeterminate = true;
            progressIndicator.IsVisible = true;
        }

        public void Hide()
        {
            progressIndicator.IsIndeterminate = false;
            progressIndicator.IsVisible = false;
        }
    }
}
