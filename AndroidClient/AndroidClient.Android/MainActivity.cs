using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Autofac;
using xMetronome.SoundsManagement;
using xMetronomeClient.Services;
using xMetronomeClient.Repositories;
using AndroidClient.Droid.SoundService;

namespace AndroidClient.Droid
{
    [Activity(Label = "AndroidClient", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    { 
        private ContainerBuilder InitContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AndroidSoundManager>().As<ISoundManager>();


            builder.RegisterType<CrossPlateformSettingsRepository>().As<ISettingsRepository>();
            builder.Register(c => new DefaultSettingsService(c.Resolve<ISettingsRepository>()))
                .As<ISettingsService>()
                .SingleInstance()
                .OnActivating(x => x.Instance.LoadSettings());

            builder.Register(c => new AudioGeneratedSoundService(c.Resolve<ISettingsService>())).As<IAudio>();

            return builder;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            InitContainer();

            LoadApplication(new App(InitContainer()));
        }
    }
}