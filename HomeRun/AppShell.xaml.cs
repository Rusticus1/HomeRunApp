using HomeRun.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        // hinzugefügt für SHELL
        void RegisterRoutes()
        {

            Routing.RegisterRoute("HomePage", typeof(HomePage));
            //Routing.RegisterRoute("DevicePage", typeof(DevicePage));

            Routing.RegisterRoute("LightsPage", typeof(LightsPage));
            Routing.RegisterRoute("HeatingPage", typeof(HeatingPage));
            Routing.RegisterRoute("JalousiePage", typeof(JalousiePage));
            Routing.RegisterRoute("Logout", typeof(MainPage));

        }


        protected override void OnNavigating(ShellNavigatingEventArgs e)
        {
            // Cancel back navigation if data is unsaved
            if(e.Target.Location.ToString() == "//LoginPage")
            {
                FirebaseService.Instance.Logout();
            }
        }


    }
}
