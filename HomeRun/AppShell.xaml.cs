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
            Routing.RegisterRoute("todo/todoItem", typeof(HomePage));
            Routing.RegisterRoute("todo/todoList", typeof(Rooms));

            /// Wenn Login page geklickt wird, muss die MainPage wieder zurück geändert werden;
            /// 
            // 1.Application.Current.MainPage = new NavigationPage(new LoginPage());

            // 2. App Neustarten, sodass alles neu geladen wird! (so hab ich kein Problem mit der Login page und Appshell()

            Routing.RegisterRoute("Logout", typeof(App));
        }

    }
}
