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
    public partial class DevicePage : ContentPage
    {
        public DevicePage()
        {
            InitializeComponent();
            Grid dgrid = (Grid)FindByName("AllDevices");
            dgrid.Children.Clear();
            //Grid für alle Devices ist immer gleich:
            try
            {
                // subscribe: Wenn sich hier etwas ändert, werde ich benachrichtigt bzw. dann ladet es erneut.
                var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
                {
                    Room updatedDevice = d.Object;  //kann jeder Raum sein
                    Console.WriteLine("Got update" + d.Object.Title);                                   //im debugger mal probieren
                                                                                                        // Betrifft das update meinen Aktuellen Raum?
                                                                                                        // Wenn ja, dann :  , ansonsten wird es ignoriert
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Grid grr = (Grid)FindByName("AllDevices");
                        dgrid.Children.Clear();

                        #region Grid and Labels
                        Label label1 = new Label()
                        {
                            Text = "Lichter",
                            FontSize = 20,
                        };
                        Switch button1 = new Switch()
                        {
                            Margin = new Thickness(0, 0, 30, 0)
                            //Text = dev.Value.Title,
                        };
                        Label label2 = new Label()
                        {
                            Text = "Jalousien",
                            FontSize = 20,
                        };
                        Switch button2 = new Switch()
                        {
                            Margin = new Thickness(0, 0, 30, 0)
                            //Text = dev.Value.Title,
                        };
                        Label label3 = new Label()
                        {
                            Text = "Heizung",
                            FontSize = 20
                        };
                        Stepper stepper = new Stepper()
                        {
                            Increment = 0.5,
                            Minimum = 0,
                            Maximum = 30,
                            Value = 22,  //bis hier funktionierts
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center
                        };
                        Label label4 = new Label()
                        {
                            FontSize = 20,
                            Text = "   °C " + stepper.Value.ToString()
                        };

                        // zum Grid danach hinzufügen? 
                                                
                        #endregion

                        int count = 1;

                        // foreach raum.... if Light schauen ob an. Wenn ja: Button.value = true
                        foreach (KeyValuePair<string, Models.Device> dev in updatedDevice.Devices) // weil Dictionary!!
                        {
                            if (dev.Value.Type == "heizung")
                            {
                                #region heizung
                                //Console.WriteLine(dev.Value.Temp);
                                //Label label1 = new Label()
                                //{
                                //    Text = dev.Value.Title,
                                //    FontSize = 20
                                //};
                                //Stepper stepper = new Stepper()
                                //{
                                //    Increment = 0.5,
                                //    Minimum = 0,
                                //    Maximum = 60,
                                //    Value = double.Parse(dev.Value.Temp),  //bis hier funktionierts
                                //    VerticalOptions = LayoutOptions.Center,
                                //    HorizontalOptions = LayoutOptions.Center
                                //};
                                //Label label2 = new Label()
                                //{
                                //    FontSize = 20,
                                //    Text = "   °C " + stepper.Value.ToString()
                                //};
                                //Console.WriteLine(stepper.Value);

                                //grr.Children.Add(label1);
                                //grr.Children.Add(label2);
                                //grr.Children.Add(stepper);
                                //label1.SetValue(Grid.RowProperty, count);
                                //label1.SetValue(Grid.ColumnProperty, 0);
                                //label2.SetValue(Grid.RowProperty, count);
                                //label2.SetValue(Grid.ColumnProperty, 1);
                                //stepper.SetValue(Grid.RowProperty, count);
                                //stepper.SetValue(Grid.ColumnProperty, 2);
                                //stepper.SetValue(Grid.ColumnSpanProperty, 2);

                                //stepper.ValueChanged += async (sender, e) =>
                                //{
                                //    dev.Value.Temp = stepper.Value.ToString();

                                //    label2.Text = "   °C " + stepper.Value.ToString();
                                //    await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                                //};
                                //count++;
                                #endregion
                            }
                            else if(dev.Value.Type == "licht" && dev.Value.Status == "on")
                            {
                                button1.IsToggled = true;


                                //button1.Toggled += async (sender, e) =>
                                //{
                                //    bool isToggled = e.Value;
                                //    if (dev.Value.Status == "off")
                                //    {
                                //        dev.Value.Status = "on";
                                //    }
                                //    else if (dev.Value.Status == "offen")
                                //    {
                                //        dev.Value.Status = "verschlossen";
                                //    }
                                //    else
                                //    {
                                //        dev.Value.Status = "off";
                                //        if (dev.Value.Type == "tuer")
                                //        {
                                //            dev.Value.Status = "offen";
                                //            await DisplayAlert("Achtung!", "Die Türe wurde aufgesperrt", "OK");
                                //        }
                                //    }
                                //    // foreach Child in rooms

                                //    await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                                //};
                                count++;
                            }
                        }

                        // wenn die Einstellungen der Buttons geregelt sind werden sie zum grid hinzugefügt
                        dgrid.Children.Add(label1);
                        dgrid.Children.Add(button1);
                        dgrid.Children.Add(label2);
                        dgrid.Children.Add(button2);
                        dgrid.Children.Add(label3);
                        dgrid.Children.Add(stepper);
                        dgrid.Children.Add(label4);

                        label1.SetValue(Grid.ColumnProperty, 0);
                        button1.SetValue(Grid.ColumnProperty, 3);
                        label2.SetValue(Grid.RowProperty, 1);
                        label2.SetValue(Grid.ColumnProperty, 0);
                        button2.SetValue(Grid.RowProperty, 1);
                        button2.SetValue(Grid.ColumnProperty, 3);

                        label3.SetValue(Grid.RowProperty, 3);
                        label3.SetValue(Grid.ColumnProperty, 0);
                        label4.SetValue(Grid.RowProperty, 3);
                        label4.SetValue(Grid.ColumnProperty, 1);
                        stepper.SetValue(Grid.RowProperty, 3);
                        stepper.SetValue(Grid.ColumnProperty, 2);
                        stepper.SetValue(Grid.ColumnSpanProperty, 2);
                    });

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }                      
        }
    }
}