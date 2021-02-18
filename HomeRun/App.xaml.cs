﻿using System;
using HomeRun.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeRun
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            MainPage = new NavigationPage(new LoginPage());

            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
