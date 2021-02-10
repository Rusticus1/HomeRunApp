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
        public Rooms(Room zimmer)
        {
            InitializeComponent();
            Label textbox = this.FindByName<Label>("Page");
            //Editor edit = this.FindByName<Editor>("Title");
            textbox.Text = zimmer.Title;
            Console.WriteLine(zimmer.Devices); //Devices Liste ist Dictionary

            Grid grr = (Grid)FindByName("DevicesInRoom");
            foreach (KeyValuePair<string, Models.Device> dev in zimmer.Devices) // weil Dictionary!!
            {
                Console.WriteLine(dev.Value.Title + " " + dev.Value.Status);
                Label label = new Label()
                {
                    Text = dev.Value.Title
                };
                Button button = new Button()
                {
                    Text = dev.Value.Title
                };
                button.Clicked += async (sender, e) =>
                {                   
                    //await FirebaseService.UpdateDevice();  ???
                    if(dev.Value.Status == "off")
                    {
                        dev.Value.Status = "on";
                    }
                    else
                    {
                        dev.Value.Status = "off";
                    }
                    
                };
                grr.Children.Add(label);
                grr.Children.Add(button);

            }
        }
    }
    
}