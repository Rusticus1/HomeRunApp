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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
            {
                Room room = d.Object;
                Console.WriteLine(room);

                foreach (KeyValuePair<string, Models.Device> dev in room.Devices)
                {
                    Console.WriteLine(dev.Value.Title);
                }
                try
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Editor textbox = this.FindByName<Editor>("Debug");
                        textbox.Text = room.Title;
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }
    }
}