using HomeRun.Pages;
using System;

using Xamarin.Forms;


namespace HomeRun.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonMainToLogin( object sender, EventArgs e)
        {

            await Navigation.PushAsync(new LoginPage());
        }

    }
}
