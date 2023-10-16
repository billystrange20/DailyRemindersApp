using Plugin.FirebasePushNotification;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyRemindersApp
{
    public partial class App : Application
    {
        private static ActivitiesDB activitiesDB;
        
        // sets up new activities db if one is not already made
        public static ActivitiesDB ActivitiesDB
        {
            get
            {
                if (activitiesDB == null)
                {
                    activitiesDB = new ActivitiesDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ActivitiesDB.db3"));
                    return activitiesDB;
                }

                return activitiesDB;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            
            // token event
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            // push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
            // push message received event
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };
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
