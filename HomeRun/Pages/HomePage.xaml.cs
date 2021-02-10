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

            var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
            {
                Room room = d.Object;  //wird noch nirgends gespeichert
                Console.WriteLine(room);
                allRooms.Add(room);
                Dictionary<string, Models.Device> device = new Dictionary<string, Models.Device>();
                
                foreach (KeyValuePair<string, Models.Device> dev in room.Devices)
                {
                    Console.WriteLine(dev.Value.Title + " " + dev.Value.Status);
                }              
                //try
                //{
                //    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                //    {
                //        Editor textbox = this.FindByName<Editor>("Debug");
                //        textbox.Text = room.Title;
                //        //Button button = this.FindByName<Button>(room.Title);
                        
                //    });

                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //}                
            });
        }

        // Liste allRooms behinhaltet alle Räume die in der Methode OnAppearing() gespeichert wurden
        // button name in Xaml wird verglichen mit den cases
        // neuer Room wird erstellt und je nach Case der Room von der Liste mitgegeben
        private async void RoomButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (button.ClassId)
            {
                case "Kueche":
                    await Navigation.PushAsync(new Rooms(allRooms[0]));   //findbyname
                    break;
                case "WC":
                    await Navigation.PushAsync(new Rooms(allRooms[1]));
                    break; 
                case "Bad":
                    await Navigation.PushAsync(new Rooms(allRooms[2]));
                    break;
                case "Esszimmer":
                    await Navigation.PushAsync(new Rooms(allRooms[3])); 
                    break;
                case "Flur":
                    await Navigation.PushAsync(new Rooms(allRooms[4]));
                    break;
                case "Wohnzimmer":
                    await Navigation.PushAsync(new Rooms(allRooms[5]));
                    break; 
                case "Schlafzimmer":
                    await Navigation.PushAsync(new Rooms(allRooms[6]));
                    break;

            }
            //await Navigation.PushAsync(new Rooms()); //als parameter den roomname mitgeben?

        }

        /*void btn1_Clicked(System.Object sender, System.EventArgs e)
        {
            Button clickedBtn = (Button)sender;

            Console.WriteLine("Button clicked");
        }*/
    }
}