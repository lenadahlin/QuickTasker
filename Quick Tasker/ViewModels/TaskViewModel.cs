using Quick_Tasker.Models;
using Quick_Tasker.Services;
using System.ComponentModel;
using SQLite;

namespace Quick_Tasker.ViewModels
{
    internal class TaskViewModel 
    {
        //public event PropertyChangedEventHandler PropertyChanged;
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

        //TODO have nulls at the end of the list
        public List<Tasks> GetUncompletedTasks
        {
            get
            {
                return connection.Table<Tasks>().Where(task => !task.CompletedStatus).OrderBy(x => x.DueDate).ToList();

            }
        }
        public List<Tasks> GetCompletedTasks
        {
            get
            {
                return connection.Table<Tasks>().Where(task => task.CompletedStatus).OrderByDescending(x => x.CompletedDate).ToList();

            }
        }
        public List<Tasks> GetAssignedTasks(DateTime newDate)
        {

                return connection.Table<Tasks>().Where(task => task.AssignedDate == newDate).OrderBy(x => x.Name).ToList();

        }

        public void SaveTask(Tasks model)
        {
            //If it has an Id, then it already exists and we can update it
            if (model.Id > 0)
            {
                connection.Update(model);
               // PropertyChanged(this, new PropertyChangedEventArgs("Tasks"));
            }
            //If not, it's new and we need to add it
            else
            {
                connection.Insert(model);
              //  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tasks"));
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

