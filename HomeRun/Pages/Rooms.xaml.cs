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
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {

                            Grid grr = (Grid)FindByName("DevicesInRoom");
                            grr.Children.Clear();

                            int count = 1;
                            foreach (KeyValuePair<string, Models.Device> dev in updatedRoom.Devices) // weil Dictionary!!
                            {
                                Console.WriteLine(dev.Value.Title + " " + dev.Value.Status);
                                if (dev.Value.Type == "heizung")
                                {
                                    Label label1 = new Label()
                                    {
                                        Text = dev.Value.Title,
                                        FontSize = 24
                                    };
                                    double temperatur = double.Parse(dev.Value.Temp);
                                    Stepper stepper = new Stepper()
                                    {
                                        Increment = 0.5,
                                        Minimum = 0,
                                        Maximum = 60,
                                        Value = temperatur
                                    };
                                    Label label2 = new Label()
                                    {
                                        //HorizontalOptions = LayoutOptions.Center,
                                        //VerticalOptions = LayoutOptions.Center,
                                        FontSize = 20,
                                        Text = "Temp: " + stepper.Value.ToString()
             
                                    };
                                    grr.Children.Add(label1);
                                    grr.Children.Add(stepper);
                                    grr.Children.Add(label2);
                                    label1.SetValue(Grid.RowProperty, count);
                                    label1.SetValue(Grid.ColumnProperty, 0);
                                    stepper.SetValue(Grid.RowProperty, count);
                                    stepper.SetValue(Grid.ColumnProperty, 2);
                                    label2.SetValue(Grid.RowProperty, count);
                                    label2.SetValue(Grid.ColumnProperty, 1);
                                    stepper.ValueChanged += async (sender, e) =>
                                    {
                                        label2.Text = "Temp: " + e.NewValue.ToString();
                                        await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                                    };
                                   count++;
                                } 
                                else
                                {
                                    Label label = new Label()
                                    {
                                        Text = dev.Value.Title,
                                        FontSize = 26,

                                    };

                                    Switch button = new Switch()
                                    {
                                        Margin = new Thickness(0, 0, 30, 0)
                                        //Text = dev.Value.Title,
                                    };

                                    if (dev.Value.Status == "on")
                                    {
                                        button.IsToggled = true;
                                    }

                                    label.SetValue(Grid.RowProperty, count);
                                    label.SetValue(Grid.ColumnProperty, 0);
                                    button.SetValue(Grid.RowProperty, count);
                                    button.SetValue(Grid.ColumnProperty, 2);

                                    button.Toggled += async (sender, e) =>
                                    {
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

                                    grr.Children.Add(label);
                                    grr.Children.Add(button);
                                    count++;
                                }
                            }
                        });
                    }
                });
            }
            catch (Exception ex)
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