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

            Label textbox = this.FindByName<Label>("Page"); // Label "Page" wird in ZimmerId (Name des Buttons auf HomePage) umbenannt
            textbox.Text = zimmerId;

            try
            {
                // subscribe: Wenn sich hier etwas ändert, werde ich benachrichtigt bzw. dann ladet es erneut.
                var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
                {
                    Room updatedRoom = d.Object;  //kann jeder Raum sein
                    Console.WriteLine("Got update" + d.Object.Title);                                   //im debugger mal probieren
                    if (updatedRoom.Title == zimmerId)  // Betrifft das update meinen Aktuellen Raum?
                    {                                   // Wenn ja, dann :  , ansonsten wird es ignoriert
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            Grid grr = (Grid)FindByName("DevicesInRoom");
                            grr.Children.Clear();

                            int count = 1;
                            foreach (KeyValuePair<string, Models.Device> dev in updatedRoom.Devices) // weil Dictionary!!
                            {
                                if (dev.Value.Type == "heizung")
                                {
                                    Console.WriteLine(dev.Value.Temp);
                                    Label label1 = new Label()
                                    {
                                        Text = dev.Value.Title,
                                        FontSize = 20
                                    };
                                    Stepper stepper = new Stepper()
                                    {
                                        Increment = 0.5,
                                        Minimum = 0,
                                        Maximum = 60,
                                        Value = double.Parse(dev.Value.Temp),  //bis hier funktionierts
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Center
                                    };
                                    Label label2 = new Label()
                                    {
                                        FontSize = 20,
                                        Text = "   °C " + stepper.Value.ToString()
                                    };
                                    Console.WriteLine(stepper.Value);

                                    grr.Children.Add(label1);
                                    grr.Children.Add(label2);
                                    grr.Children.Add(stepper);
                                    label1.SetValue(Grid.RowProperty, count);
                                    label1.SetValue(Grid.ColumnProperty, 0);
                                    label2.SetValue(Grid.RowProperty, count);
                                    label2.SetValue(Grid.ColumnProperty, 1);
                                    stepper.SetValue(Grid.RowProperty, count);
                                    stepper.SetValue(Grid.ColumnProperty, 2);
                                    stepper.SetValue(Grid.ColumnSpanProperty, 2);

                                    stepper.ValueChanged += async (sender, e) =>
                                    {
                                        dev.Value.Temp = stepper.Value.ToString();

                                        label2.Text = "   °C " + stepper.Value.ToString();
                                        await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                                    };
                                    count++;
                                }
                                else
                                {
                                    Label label = new Label()
                                    {
                                        Text = dev.Value.Title,
                                        FontSize = 20,

                                    };
                                    Label doorlock = new Label()
                                    {                     
                                        Text = dev.Value.Status,
                                        FontSize = 15,
                                        IsVisible = false,
                                        VerticalTextAlignment = TextAlignment.Center,                                        
                                        Margin = new Thickness(30, 0, 0, 0)
                                        
                                    };
                                    if(dev.Value.Type == "tuer")
                                    {
                                        doorlock.IsVisible = true;
                                    }
                                    Switch button = new Switch()
                                    {
                                        Margin = new Thickness(0, 0, 30, 0)
                                        //Text = dev.Value.Title,
                                    };

                                    if (dev.Value.Status == "on" || dev.Value.Status == "verschlossen")
                                    {
                                        button.IsToggled = true;
                                    }
                                    label.SetValue(Grid.RowProperty, count);
                                    label.SetValue(Grid.ColumnProperty, 0);
                                    doorlock.SetValue(Grid.RowProperty, count);
                                    doorlock.SetValue(Grid.ColumnProperty, 2);
                                    doorlock.SetValue(Grid.ColumnSpanProperty, 2);
                                    button.SetValue(Grid.RowProperty, count);
                                    button.SetValue(Grid.ColumnProperty, 3);

                                    button.Toggled += async (sender, e) =>
                                    {
                                        bool isToggled = e.Value;
                                        if (dev.Value.Status == "off")
                                        {
                                            dev.Value.Status = "on";
                                        }
                                        else if (dev.Value.Status == "offen")
                                        {
                                            dev.Value.Status = "verschlossen";
                                        }
                                        else
                                        {
                                            dev.Value.Status = "off";
                                            if (dev.Value.Type == "tuer")
                                            {
                                                dev.Value.Status = "offen";
                                                await DisplayAlert("Achtung!", "Die Türe wurde aufgesperrt", "OK");
                                            }
                                        }
                                        await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                                    };
                                    grr.Children.Add(label);
                                    grr.Children.Add(doorlock);
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
    }

}