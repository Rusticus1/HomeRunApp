using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;

namespace HomeRun
{
    public class FirebaseService
    {

        private static FirebaseService instance = null;
        private FirebaseAuthClient auth = null;
        private FirebaseClient client = null;

        private FirebaseService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyBIGPP5wtt3bGl4aBMiuMsPiyJxVlbe9Js",
                AuthDomain = "smarthome-50f1c.firebaseapp.com",
                Providers = new FirebaseAuthProvider[] {
                    new EmailProvider()
                }
            };
            this.auth = new FirebaseAuthClient(config);

        }

        public static FirebaseService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FirebaseService();
                }
                return instance;
            }
        }

        public FirebaseClient GetClient()
        {
            // hier client hinzugefügt
            //this.client = new FirebaseClient("https://smarthome-50f1c-default-rtdb.europe-west1.firebasedatabase.app");

            return instance.client;
        }

        public async void Logout()
        {
            try
            {
               
                await this.auth.SignOutAsync();
                instance = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public async Task<Boolean> SignInWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await this.auth.SignInWithEmailAndPasswordAsync(email, password);
                this.client = new FirebaseClient("https://smarthome-50f1c-default-rtdb.europe-west1.firebasedatabase.app");
                
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}
