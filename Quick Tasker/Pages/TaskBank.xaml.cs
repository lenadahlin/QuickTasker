using AndroidX.Lifecycle;
using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;
using System.Threading.Tasks;

namespace Quick_Tasker.Pages;

public partial class TaskBank : ContentPage
{
    private readonly TaskViewModel viewModel;
    public TaskBank()
    {
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
    }
    //refreshes tasks whenever page opens
    protected override void OnAppearing()
    {
        base.OnAppearing();
        TaskBankView.ItemsSource = viewModel.GetUncompletedTasks;
    }

    private void CheckedComplete(object sender, CheckedChangedEventArgs e)
    {

        if (sender is CheckBox checkBox && checkBox.BindingContext is Tasks task)
        {
            if (task.CompletedStatus && task.CompletedDate == null)
            {
                // if checkbox is checked, change CompletedDate to today
                task.CompletedDate = DateTime.Now;
                viewModel.SaveTask(task);
            }
            else if (!task.CompletedStatus && task.CompletedDate != null)
            {
                // if checkbox is un-checked, change CompletedDate to null
                task.CompletedDate = null;
                viewModel.SaveTask(task);
            }
            TaskBankView.ItemsSource = viewModel.GetUncompletedTasks;
        }
    }
}
