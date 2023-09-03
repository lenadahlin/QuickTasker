using Quick_Tasker.Models;
using Quick_Tasker.Services;
using SQLite;

namespace Quick_Tasker.ViewModels
{
    internal class TaskViewModel : ObservableObject
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

        //TODO have nulls at the end of the list
        public List<Tasks> GetTasksByDueDate
        {
            get
            {
                return connection.Table<Tasks>().OrderBy(x => x.DueDate).ToList();

            }
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

