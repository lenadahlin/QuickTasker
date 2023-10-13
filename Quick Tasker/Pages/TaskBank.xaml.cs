using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;
using static Android.Provider.ContactsContract.CommonDataKinds;

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

    private void EditButton_Clicked(System.Object sender, System.EventArgs e)
    {
        if (sender is ImageButton imgButton && imgButton.BindingContext is Tasks task)
        {
            Navigation.PushAsync(new EditTask((task)));
        }
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (Picker.SelectedIndex == 0)
        {
            TaskBankView.ItemsSource = viewModel.GetUncompletedTasks;
        } else if (Picker.SelectedIndex == 1)
        {
            TaskBankView.ItemsSource = viewModel.GetNoDueDateTasks;
        } else if (Picker.SelectedIndex == 2)
        {
            TaskBankView.ItemsSource = viewModel.GetUnassignedTasks;
        }
    }
}
