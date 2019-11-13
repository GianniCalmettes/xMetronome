using Autofac;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xMetronome;
using xMetronome.SoundsManagement;
using xMetronomeClient.ViewModels;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace AndroidClient
{
    public partial class App : Application
    {

        public static IContainer Container; 

        public App(ContainerBuilder containerBuilder)
        {
            try
            {

                Container = SetupContainer(containerBuilder);
                InitializeComponent();

                MainPage = new NavigationPage(
                    new MainPage(Container.Resolve<MainPageViewModel>(), Container.Resolve<SettingsPageViewModel>())
                );

            }
            catch (Exception e)
            {
                throw;
            }            
        }

        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private IContainer SetupContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<BeatCounter>();

            containerBuilder.Register(c => new Metronome(c.Resolve<ISoundManager>(), c.Resolve<BeatCounter>()))
                .As<Metronome>();           

            containerBuilder.RegisterType<MainPageViewModel>();
            containerBuilder.RegisterType<SettingsPageViewModel>();

            return containerBuilder.Build();
        }
    }
}