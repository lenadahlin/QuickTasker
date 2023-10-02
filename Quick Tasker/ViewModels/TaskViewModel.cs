using Quick_Tasker.Models;
using Quick_Tasker.Services;
using System.ComponentModel;
using SQLite;
using Android.Graphics;
using System.Diagnostics;
using static Android.App.DownloadManager;
using System.Globalization;
using Microsoft.VisualBasic;

namespace Quick_Tasker.ViewModels
{
    internal class TaskViewModel
    {
        public static TaskViewModel Current { get; set; }

        SQLiteConnection connection;

        public TaskViewModel()
        {
            Current = this;
            connection = DatabaseService.Connection;
        }

        public List<Tasks> Tasks
        {
            get
            {
                return connection.Table<Tasks>().ToList();
            }
        }

        //for TaskBank list
        public List<Tasks> GetUncompletedTasks
        {
            get
            {
                return connection.Table<Tasks>().Where(task => !task.CompletedStatus).OrderBy(x => x.DueDate).ToList();

            }
        }
        //for Completed list
        public List<Tasks> GetCompletedTasks
        {
            get
            {
                return connection.Table<Tasks>().Where(task => task.CompletedStatus).OrderByDescending(x => x.CompletedDate).ToList();

            }
        }
        //for DailyView list
        public List<Tasks> GetAssignedTasks(DateTime newDate)
        {

            return connection.Table<Tasks>().Where(task => task.AssignedDate == newDate).OrderBy(x => x.Name).ToList();

        }

        //for TaskGenerator
        public Tasks GetRandomTask(TimeSpan timeAvailable, DateTime assignedDate)
        {

            //format time to be the same format as db
            string formattedTimeAvailable = timeAvailable.ToString(@"hh\:mm\:ss");
            Debug.WriteLine("Formatted time available: " + formattedTimeAvailable);
            string query = @"
            SELECT * FROM Tasks
            WHERE EstimatedTime <= ? 
            AND AssignedDate IS NULL 
            AND CompletedDate IS NULL 
            AND (DueDate IS NULL OR DueDate >= ?)
            ORDER BY RANDOM() LIMIT 1";


            return connection.Query<Tasks>(query, formattedTimeAvailable, assignedDate).FirstOrDefault();
        }

        public void SaveTask(Tasks model)
        {
            //If it has an Id, then it already exists and we can update it
            if (model.Id > 0)
            {
                connection.Update(model);
            }
            //If not, it's new and we need to add it
            else
            {
                connection.Insert(model);
            }
        }

        public void DeleteTask(Tasks model)
        {
            //If it has an Id, then we can delete it
            if (model.Id > 0)
            {
                connection.Delete(model);
            }
        }

        internal void SaveTask(Task newTask)
        {
            throw new NotImplementedException();
        }
    }
}

