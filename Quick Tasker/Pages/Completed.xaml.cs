using AndroidX.Lifecycle;
using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;
namespace Quick_Tasker.Pages;

public partial class Completed : ContentPage
{
    private readonly TaskViewModel viewModel;
    public Completed()
    {
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
    }
    //refreshes tasks whenever page opens
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CompletedView.ItemsSource = viewModel.GetCompletedTasks;
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
                CompletedView.ItemsSource = viewModel.GetCompletedTasks;
            }
            else if (!task.CompletedStatus && task.CompletedDate != null)
            {
                // if checkbox is un-checked, change CompletedDate to null
                task.CompletedDate = null;
                viewModel.SaveTask(task);
                CompletedView.ItemsSource = viewModel.GetCompletedTasks;
            }
        }
    }
}
