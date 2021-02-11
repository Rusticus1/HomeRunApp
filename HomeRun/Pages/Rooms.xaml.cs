using System;
using Firebase.Database;
using System.Reactive.Linq;
using Firebase.Database.Query;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeRun.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HomeRun.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rooms : ContentPage
    {

        public Rooms(string zimmerId)
        {
            InitializeComponent();

            Label textbox = this.FindByName<Label>("Page");
            textbox.Text = zimmerId;

            try
            {
                var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
                {
                    Room updatedRoom = d.Object;
                    Console.WriteLine("Got update" + d.Object.Title);
                    if (updatedRoom.Title == zimmerId)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {

                            Grid grr = (Grid)FindByName("DevicesInRoom");
                            grr.Children.Clear();

                            int count = 1;
                            foreach (KeyValuePair<string, Models.Device> dev in updatedRoom.Devices) // weil Dictionary!!
                            {
                                Console.WriteLine(dev.Value.Title + " " + dev.Value.Status);
                                Button button = new Button()
                                {
                                    Text = dev.Value.Title,
                                };

                                if (dev.Value.Status == "on")
                                {
                                    button.BackgroundColor = Color.Yellow;
                                }

                                button.SetValue(Grid.RowProperty, count);

                                button.Clicked += async (sender, e) =>
                                {
                                    if (dev.Value.Status == "off")
                                    {
                                        dev.Value.Status = "on";
                                    }
                                    else
                                    {
                                        dev.Value.Status = "off";
                                    }
                                    await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                                };
                                grr.Children.Add(button);
                                count++;
                            }
                        });                    
                    }
                });
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
    
}