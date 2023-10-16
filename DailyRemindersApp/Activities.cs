using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DailyRemindersApp
{
    // a DB that will store the name of the activity, if the user has checked off the activity,
    // keeps a count of how many times the activity has been checked, and the last time the activity has been checked
    public class Activities
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Activity { get; set; }
        public bool IsChecked { get; set; }
        public int Count { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
