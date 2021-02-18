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
            /// APP SHELL hier eingefügt 
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            try
            {
                await FirebaseService.Instance.SignInWithEmailPassword(UserLoginEmail.Text, UserLoginPassword.Text);
                //zurück zur APP und dann zur Home Page??

                Application.Current.MainPage = new AppShell(); //müsste dann beim Logout wieder zurück geändert werden
                await Navigation.PushAsync(new HomePage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login", "Wrong Email/Password", "So sad");
                Console.WriteLine(ex);
            }
        }
    }
}