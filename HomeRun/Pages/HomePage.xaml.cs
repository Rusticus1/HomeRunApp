using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeRun.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        //public List<Room> allRooms = new List<Room>();
        public HomePage()
        {
            InitializeComponent();
        }

        // Wenn die Seite erscheint, passiert das kurz bevor die Page dargestellt wird: 
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //}              

        // neuer Room wird erstellt und je nach Case der Room von der Liste mitgegeben
        private async void RoomButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            await Navigation.PushAsync(new Rooms(button.ClassId));

        }

        /*void btn1_Clicked(System.Object sender, System.EventArgs e)
        {
            Button clickedBtn = (Button)sender;

            Console.WriteLine("Button clicked");
        }*/
    }
}