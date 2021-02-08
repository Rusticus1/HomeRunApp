using System;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeRun.Models;

namespace HomeRun.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                this.FindByName<Editor>("Debug").Text = "Loading";
                FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
                {
                    Console.WriteLine(d.Object.Title);
                    this.FindByName<Editor>("Debug").Text = d.Object.Title;
                });
            } catch(Exception ex)
            {
                this.FindByName<Editor>("Debug").Text = "Error";

                Console.WriteLine(ex);
            }

            Console.WriteLine("Test");
        }
    }
}