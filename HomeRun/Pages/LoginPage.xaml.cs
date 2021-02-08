using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeRun.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyBIGPP5wtt3bGl4aBMiuMsPiyJxVlbe9Js",
                AuthDomain = "smarthome-50f1c.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };

            try
            {
                var client = new FirebaseAuthClient(config);
                var user = await client.SignInWithEmailAndPasswordAsync(UserLoginEmail.Text, UserLoginPassword.Text);
                await Navigation.PushAsync(new HomePage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login", ex.ToString(), "So sad");
                Console.WriteLine(ex);
            }
        }
    }
}