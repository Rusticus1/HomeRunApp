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
            Console.WriteLine(zimmer.Title);
        }

    }
}