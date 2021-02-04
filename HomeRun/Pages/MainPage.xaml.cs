using HomeRun.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace HomeRun
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void ButtonMainToLogin( object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}
