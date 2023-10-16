using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DailyRemindersApp
{
    public class ActivitiesDB
    {
        private readonly SQLiteAsyncConnection _activitiesDB;

        public ActivitiesDB(string dbPath)
        {
            _activitiesDB = new SQLiteAsyncConnection(dbPath);
            _activitiesDB.CreateTableAsync<Activities>().Wait();
;       }

        // READ
        public Task<List<Activities>> GetActivitiesAsync()
        {
            return _activitiesDB.Table<Activities>().ToListAsync();
        }

        // CREATE
        public Task<int> SaveActivitiesAsync(Activities activities)
        {
            return _activitiesDB.InsertAsync(activities);
        }

        // UPDATE
        public Task<int> UpdateActivitiesAsync(Activities activities)
        {
            return _activitiesDB.UpdateAsync(activities);
        }

        // DELETE
        public Task<int> DeleteActivitiesAsync(Activities activities)
        {
            return _activitiesDB.DeleteAsync(activities);
        }
    }
}
