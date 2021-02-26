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
    public partial class JalousiePage : ContentPage
    {
        public JalousiePage()
        {
            InitializeComponent();

            bool initialLoad = true;
            var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
            {

                Room updatedRoom = d.Object;
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    StackLayout stack = (StackLayout)FindByName("device");

                    Label zimmer = new Label()
                    {
                        Text = updatedRoom.Title,
                        FontSize = 20,
                        VerticalTextAlignment = TextAlignment.Center

                    };
          
                    if (initialLoad == true)
                    {
                        stack.Children.Add(zimmer);
                    }


                    foreach (KeyValuePair<string, Models.Device> dev in updatedRoom.Devices)
                    {
                        if (dev.Value.Type == "jalousie")
                        {

                            Label label = new Label()
                            {
                                Text = dev.Value.Title,
                                Margin = new Thickness(30, 0, 0, 0)
                            };
                            Switch sw = new Switch()
                            {
                                Margin = new Thickness(0, 0, 30, 0)
                            };

                            if (dev.Value.Status == "on")
                            {
                                sw.IsToggled = true;
                            }
                            else if (dev.Value.Status == "off")
                            {
                                sw.IsToggled = false;
                            }

                            if (initialLoad == true) // = true, wenn es das erste mal die Seite lädt
                            {
                                stack.Children.Add(label);
                                stack.Children.Add(sw);
                            }

                            sw.Toggled += async (sender, e) =>
                            {
                                initialLoad = false;    // nicht mehr der erste load, also false damit nichts mehr zum Stack hinzugefügt wird                      
                                bool isToggled = e.Value;

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
                        }
                    }
                });
            });
        }
    }
}