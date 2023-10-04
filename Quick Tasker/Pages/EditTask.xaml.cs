using Quick_Tasker.ViewModels;
namespace Quick_Tasker.Pages;

using Android.App;
using Quick_Tasker.Models;
public partial class EditTask : ContentPage
{
    private readonly Tasks taskForEdit;
    public EditTask()
    {
        InitializeComponent();
    }
    public EditTask(Tasks task)
    {
        InitializeComponent();
        taskForEdit = task;
        NameEntry.Text = taskForEdit.Name;
        if (taskForEdit.DueDate == null)
        {
            DueDateEntry.Date = DateTime.Now;
            DueDateEntry.IsEnabled = false;
            DueDateEntry.IsVisible = false;
            DueDateSwitch.IsToggled = false;
        }
        else if (taskForEdit.DueDate != null)
        {
            DueDateEntry.Date = (DateTime)task.DueDate;
            DueDateEntry.IsEnabled = true;
            DueDateEntry.IsVisible = true;
            DueDateSwitch.IsToggled = true;
        }
        if (taskForEdit.AssignedDate == null)
        {
            AssignedDateEntry.Date = DateTime.Now;
            AssignedDateEntry.IsEnabled = false;
            AssignedDateEntry.IsVisible = false;
            AssignedDateSwitch.IsToggled = false;
        }
        else if (taskForEdit.AssignedDate != null)
        {
            AssignedDateEntry.Date = (DateTime)task.AssignedDate;
            AssignedDateEntry.IsEnabled = true;
            AssignedDateEntry.IsVisible = true;
            AssignedDateSwitch.IsToggled = true;
        }
        if (taskForEdit.EstimatedTime == null)
        {
            EstimatedTimeEntry.IsEnabled = false;
            EstimatedTimeEntry.IsVisible = false;
            EstimatedTimeSwitch.IsToggled = false;
        }
        else if (taskForEdit.EstimatedTime != null)
        {
            EstimatedTimeEntry.Time = (TimeSpan)task.EstimatedTime;
            EstimatedTimeEntry.IsEnabled = true;
            EstimatedTimeEntry.IsVisible = true;
            EstimatedTimeSwitch.IsToggled = true;
        }
        if (taskForEdit.CompletedDate == null)
        {
            CompletedDateEntry.Date = DateTime.Now;
            CompletedDateEntry.IsEnabled = false;
            CompletedDateEntry.IsVisible = false;
            CompletedDateSwitch.IsToggled = false;
        }
        else if (taskForEdit.CompletedDate != null)
        {
            CompletedDateEntry.Date = (DateTime)task.CompletedDate;
            CompletedDateEntry.IsEnabled = true;
            CompletedDateEntry.IsVisible = true;
            CompletedDateSwitch.IsToggled = true;
        }
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
    private void CompletedDateSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        CompletedDateEntry.IsEnabled = e.Value;
        CompletedDateEntry.IsVisible = e.Value;
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        _ = new TaskViewModel();
        //date and times from entries need to be converted
        //all are null if not enabled

        //Due Date conversion
        if (DueDateEntry.IsEnabled)
        {
            taskForEdit.DueDate = (DateTime?)DueDateEntry.Date;
        }
        else if (DueDateEntry.IsEnabled == false)
        {
            taskForEdit.DueDate = null;
        }
        //Assigned Date conversion
        if (AssignedDateEntry.IsEnabled)
        {
            taskForEdit.AssignedDate = (DateTime?)AssignedDateEntry.Date;
        }
        else if (AssignedDateEntry.IsEnabled == false)
        {
            taskForEdit.AssignedDate = null;
        }
        //Completed Date conversion
        if (CompletedDateEntry.IsEnabled)
        {
            taskForEdit.CompletedDate = (DateTime?)CompletedDateEntry.Date;
        }
        else if (CompletedDateEntry.IsEnabled == false)
        {
            taskForEdit.CompletedDate = null;
        }

        //Estimated Time conversion
        if (EstimatedTimeEntry.IsEnabled)
        {
            taskForEdit.EstimatedTime = (TimeSpan?)EstimatedTimeEntry.Time;
        }
        else if (EstimatedTimeEntry.IsEnabled == false)
        {
            taskForEdit.EstimatedTime = null;
        }

        //check if empty
        if ((string.IsNullOrEmpty(NameEntry.Text) || NameEntry.Text.Length < 1))
        {
            await DisplayAlert("Warning", "Tasks must have a name", "OK");
            return;
        }
        if (EstimatedTimeEntry.Time == TimeSpan.Zero && EstimatedTimeSwitch.IsToggled == true)
        {
            await DisplayAlert("Warning", "Time must not be 0", "OK");
            return;
        }
        if (AssignedDateEntry.Date > DueDateEntry.Date && AssignedDateSwitch.IsToggled == true && DueDateSwitch.IsToggled == true)
        {
            await DisplayAlert("Warning", "Assigned date must be after the due date", "OK");
            return;
        }
        if (AssignedDateEntry.Date < DateTime.Today || DueDateEntry.Date < DateTime.Today)
        {
            await DisplayAlert("Warning", "Dates must be on or after today", "OK");
            return;
        }
        else
        {
            //save to database
            TaskViewModel.Current.SaveTask(taskForEdit);
            string alertInfo = taskForEdit.Name + " saved successfully.";
            await DisplayAlert("Success", alertInfo, "OK");
            await Navigation.PopAsync();
        }
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        _ = new TaskViewModel();
        string action = await DisplayActionSheet("Delete Task?", "Cancel", null, "Yes", "No");
        if (action == "Yes")
        {
            TaskViewModel.Current.DeleteTask(taskForEdit);
            await Navigation.PopAsync();
        }
    }
}
