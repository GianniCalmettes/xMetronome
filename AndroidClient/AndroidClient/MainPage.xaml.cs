using Autofac;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using xMetronomeClient;
using xMetronomeClient.ViewModels;

namespace AndroidClient
{
    public partial class MainPage : ContentPage
    {
        private SettingsPageViewModel _settingsPageViewModel;
        private AboutPageViewModel _aboutPageViewModel;

        public MainPage(MainPageViewModel mainPageViewModel, SettingsPageViewModel settingsPageViewModel, AboutPageViewModel aboutPageViewModel)
        {
            _settingsPageViewModel = settingsPageViewModel;
            _aboutPageViewModel = aboutPageViewModel;

            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

        public async void OnShowAboutPageAsync(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AboutPage(_aboutPageViewModel));
        }
        
        public async void OnShowSettingsAsync(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SettingsPage(_settingsPageViewModel));
        }
    }
}