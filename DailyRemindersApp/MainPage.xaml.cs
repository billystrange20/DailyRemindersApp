using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DailyRemindersApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Activities> act = await App.ActivitiesDB.GetActivitiesAsync();

            // checks all activities lastChecked, if same as today, nothing,
            // if a day has passed, will uncheck,
            // if more than one day has passed, will uncheck and reset day streak
            foreach (Activities a in act)
            {
                if (a.LastChecked.Day == DateTime.Now.Day)
                {
                    // nothing
                } else if (a.LastChecked.Day+1 == DateTime.Now.Day)
                {
                    a.IsChecked = false;

                } else if (a.LastChecked.Day+1 < DateTime.Now.Day)
                {
                    a.IsChecked = false;
                    a.Count = 0;
                }

                await App.ActivitiesDB.UpdateActivitiesAsync(a);
            }

            collection.ItemsSource = await App.ActivitiesDB.GetActivitiesAsync();
        }

        // when the button is clicked, the user will add a new activity to the
        // activity DB and is displayed on the grid for them to see
        // if no text inputted, activity will not be added
        async void OnNewActivityButtonClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("New Activity", "Name of activity:");

            if (result != null)
            {
                // adds new activity to db here
                await App.ActivitiesDB.SaveActivitiesAsync(new Activities
                {
                    Activity = result,
                    IsChecked = false,
                    Count = 0,
                    LastChecked = DateTime.MinValue
                });

                await DisplayAlert("New activity added!", "", "OK");

                collection.ItemsSource = await App.ActivitiesDB.GetActivitiesAsync();

            } else
            {
                await DisplayAlert("No activity added", "", "OK");
            }
        }


        // gets activityDB coloumn from collectionView and stored in lastSelection
        Activities lastSelection;
        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as Activities;
        }

        // changes selected activity in DB and checks it (IsChecked = true)
        // if already checked displays popup message
        async void OnUpdatedButtonClicked(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                if (lastSelection.IsChecked)
                {
                    await DisplayAlert("Activity already checked", "", "OK");
                } else
                {
                    lastSelection.IsChecked = true;
                    lastSelection.Count+=1;
                    lastSelection.LastChecked = DateTime.Now;

                    await App.ActivitiesDB.UpdateActivitiesAsync(lastSelection);

                    collection.ItemsSource = await App.ActivitiesDB.GetActivitiesAsync();

                    await DisplayAlert("Activities Updated", "", "OK");
                }
            }
        }


        // deleted selected activity from the database
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                await App.ActivitiesDB.DeleteActivitiesAsync(lastSelection);

                collection.ItemsSource = await App.ActivitiesDB.GetActivitiesAsync();

                await DisplayAlert("Activity Deleted", "", "OK");
            }
        }
    }
}
