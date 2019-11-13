using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace xMetronomeClient.ViewModels
{
    public class AboutPageViewModel
    {
        public ICommand OpenBrowserCommand { get; private set; }

        public AboutPageViewModel()
        {

            OpenBrowserCommand = new Command<string>(async (url) =>
            {
                await Launcher.OpenAsync(url);
            });
        }

    }
}
