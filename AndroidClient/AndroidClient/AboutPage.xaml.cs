using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xMetronomeClient.ViewModels;

namespace xMetronomeClient
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage (AboutPageViewModel aboutPageViewModel)
		{
            InitializeComponent();
            BindingContext = aboutPageViewModel;
        }
	}
}