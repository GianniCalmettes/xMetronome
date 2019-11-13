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

        public MainPage(MainPageViewModel mainPageViewModel, SettingsPageViewModel settingsPageViewModel)
        {
            _settingsPageViewModel = settingsPageViewModel;

            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

        public async void OnShowAboutPageAsync(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AboutPage());
        }
        
        public async void OnShowSettingsAsync(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SettingsPage(_settingsPageViewModel));
        }
    }
}