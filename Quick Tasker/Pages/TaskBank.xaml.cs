using AndroidX.Lifecycle;
using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;
using System.Threading.Tasks;

namespace Quick_Tasker.Pages;

public partial class TaskBank : ContentPage
{
    private TaskViewModel viewModel;
    public TaskBank()
    {
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
    }
    //TODO checking as complete is updating the date for EVERY task 
    private void CheckedComplete(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Tasks task)
        {
            if (e.Value)
            {
                // if checkbox is checked, change CompletedDate to today
                task.CompletedDate = DateTime.Now;
            }
            else
            {
                // if checkbox is un-checked, change CompletedDate to null
                task.CompletedDate = null;
            }
            viewModel.SaveTask(task);
        }
    }
}
