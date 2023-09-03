namespace Quick_Tasker.Pages;
using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;
public partial class NewTask : ContentPage
{
    public NewTask()
    {
        InitializeComponent();
    }

    //Task information toggles
    private void DueDateSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        DueDateEntry.IsEnabled = e.Value;
        DueDateEntry.IsVisible = e.Value;
    }
    private void AssignedDateSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        AssignedDateEntry.IsEnabled = e.Value;
        AssignedDateEntry.IsVisible = e.Value;
    }
    private void EstimatedTimeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        EstimatedTimeEntry.IsEnabled = e.Value;
        EstimatedTimeEntry.IsVisible = e.Value;
    }


    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        _ = new TaskViewModel();
        //date and times from entries need to be converted
        //all are null if not enabled
        DateTime? dueDateOnly = null;
        DateTime? assignedDateOnly = null;
        TimeSpan? estimatedTimeOnly = null;

        //Due Date conversion
        if (DueDateEntry.IsEnabled)
        {
            dueDateOnly = DueDateEntry.Date;
        }
        //Assigned Date conversion
        if (AssignedDateEntry.IsEnabled)
        {
            assignedDateOnly = AssignedDateEntry.Date;
        }

        //Estimated Time conversion
        if (EstimatedTimeEntry.IsEnabled)
        {
            estimatedTimeOnly = EstimatedTimeEntry.Time;
        }

        //check if empty
        if ((string.IsNullOrEmpty(NameEntry.Text) || NameEntry.Text.Length < 1))
        {
            await DisplayAlert("Warning", "Tasks must have a name", "OK");
            return;
        }
        else
        {
            //constructor
            Tasks newTask = new Tasks
            {
                Name = NameEntry.Text,
                DueDate = dueDateOnly,
                AssignedDate = assignedDateOnly,
                EstimatedTime = estimatedTimeOnly,
                CompletedDate = null // null until completed
            };
            // Save the task to the database
            TaskViewModel.Current.SaveTask(newTask);
            await Navigation.PopAsync();
        }
    }
}
