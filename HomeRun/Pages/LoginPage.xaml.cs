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
            
            //Application.Current.MainPage = new NavigationPage(new LoginPage()); //müsste dann beim Logout wieder zurück geändert werden
           
            
            InitializeComponent();

        }


        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            
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

                await DisplayAlert("Login", "Wrong Email/Password", "Try again");
                Console.WriteLine(ex);
            }
        }
    }
}