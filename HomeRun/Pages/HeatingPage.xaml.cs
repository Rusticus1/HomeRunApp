using Firebase.Database.Query;
using HomeRun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeRun.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeatingPage : ContentPage
    {
        public HeatingPage()
        {
            InitializeComponent();

            bool initialLoad = true;
            var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
            {

                Room updatedRoom = d.Object;
                Xamarin.Forms.Device.BeginInvokeOnMainThread( () =>
                {
                    StackLayout stack = (StackLayout)FindByName("device");
                                        
                    foreach (KeyValuePair<string, Models.Device> dev in updatedRoom.Devices)
                    {
                        if (dev.Value.Type == "heizung")
                        {

                            Label label1 = new Label()
                            {
                                Text = updatedRoom.Title,
                                FontSize = 20,
                                VerticalTextAlignment = TextAlignment.Center
                            };

                            Stepper stepper = new Stepper()
                            {
                                Increment = 0.5,
                                Minimum = 0,
                                Maximum = 30,
                                Value = double.Parse(dev.Value.Temp),  
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.End
                            };
                            Label label2 = new Label()
                            {
                                FontSize = 15,
                                HorizontalOptions = LayoutOptions.Center,
                                Text = "   °C " + stepper.Value.ToString()
                            };

                            if (initialLoad == true) // = true, wenn es das erste mal die Seite lädt
                            {
                                stack.Children.Add(label1);
                                stack.Children.Add(label2);
                                stack.Children.Add(stepper);
                            }
                            stepper.ValueChanged += async (sender, e) =>
                            {
                                initialLoad = false;
                                dev.Value.Temp = stepper.Value.ToString();

                                label2.Text = "   °C " + stepper.Value.ToString();
                                await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                            };
                        }
                    }
                });
            });           
        }
    }
}