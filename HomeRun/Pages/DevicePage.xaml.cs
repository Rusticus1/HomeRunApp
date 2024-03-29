﻿using Firebase.Database.Query;
using HomeRun.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeRun.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicePage : ContentPage
    {
        public DevicePage()
        {
            InitializeComponent();

            bool initialLoad = true;
            var sub = FirebaseService.Instance.GetClient().Child("rooms").AsObservable<Room>().Subscribe(d =>
            {

                Room updatedRoom = d.Object;
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    StackLayout stack = (StackLayout)FindByName("device");
                    
                    Label zimmer = new Label()
                    {
                        Text = updatedRoom.Title,
                        FontSize = 20

                    };
                    // 25= Alle Zimmer plus alle Lichter und Buttons im Stack
                    if(initialLoad == true)
                    {
                        stack.Children.Add(zimmer);
                    }
                  

                    foreach (KeyValuePair<string, Models.Device> dev in updatedRoom.Devices)
                    {
                        if (dev.Value.Type == "licht")
                        {

                            Label label = new Label()
                            {
                                Text = dev.Value.Title
                            };
                            Switch sw = new Switch()
                            {

                            };
                           
                            if (dev.Value.Status == "on")
                            {
                                sw.IsToggled = true;
                            }
                            else if (dev.Value.Status == "off")
                            {
                                sw.IsToggled = false;
                            }
                            
                            if(initialLoad == true) // = true, wenn es das erste mal die Seite lädt
                            {
                                stack.Children.Add(label);
                                stack.Children.Add(sw);
                            }                            

                            sw.Toggled += async (sender, e) =>
                            {                                
                                initialLoad = false;    // nicht mehr der erste load, also false damit nichts mehr zum Stack hinzugefügt wird                      
                                bool isToggled = e.Value;                                

                                if (dev.Value.Status == "off")
                                {
                                    dev.Value.Status = "on";
                                }
                                else
                                {
                                    dev.Value.Status = "off";
                                }                                
                                await FirebaseService.Instance.GetClient().Child("rooms").Child(d.Key).Child("devices").Child(dev.Key).PutAsync(dev.Value);
                            };
                        }
                    }                    
                });
            });
        }
    }
}