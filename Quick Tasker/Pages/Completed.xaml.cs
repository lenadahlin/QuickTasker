using AndroidX.Lifecycle;
using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;

namespace Quick_Tasker.Pages;

public partial class Completed : ContentPage
{
    private TaskViewModel viewModel;
    public Completed()
    {
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
    }
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
