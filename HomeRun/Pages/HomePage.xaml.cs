using System;
using Firebase.Database;
using System.Reactive.Linq;
using Firebase.Database.Query;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeRun.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HomeRun.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public List<Room> allRooms = new List<Room>();
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        // Liste allRooms behinhaltet alle Räume die in der Methode OnAppearing() gespeichert wurden
        // button name in Xaml wird verglichen mit den cases
        // neuer Room wird erstellt und je nach Case der Room von der Liste mitgegeben
        private async void RoomButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            await Navigation.PushAsync(new Rooms(button.ClassId));

        }

        /*void btn1_Clicked(System.Object sender, System.EventArgs e)
        {
            Button clickedBtn = (Button)sender;

            Console.WriteLine("Button clicked");
        }*/
    }
}