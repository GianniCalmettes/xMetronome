using AndroidClient;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xMetronomeClient.ViewModels;

namespace xMetronomeClient
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage (SettingsPageViewModel settingsPageViewModel)
		{
            BindingContext = settingsPageViewModel;
			InitializeComponent ();
		}
	}
}