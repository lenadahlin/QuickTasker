namespace Quick_Tasker.Pages;
using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;
using AndroidX.Lifecycle;

public partial class NewTask : ContentPage
{
    public NewTask()
    {
        InitializeComponent();
    }

    //clears any text/dates and sets switches to false
    protected override void OnAppearing()
    {
        base.OnAppearing();
        NameEntry.Text = "";
        DueDateEntry.Date = DateTime.Now;
        DueDateSwitch.IsToggled = false;
        AssignedDateEntry.Date = DateTime.Now;
        AssignedDateSwitch.IsToggled = false;
        EstimatedTimeEntry.Time = TimeSpan.Zero;
        EstimatedTimeSwitch.IsToggled = false;
    }

    //Task information toggles
    private void DueDateSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        DueDateEntry.Date = DateTime.Now;
        DueDateEntry.IsEnabled = e.Value;
        DueDateEntry.IsVisible = e.Value;
    }
    private void AssignedDateSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        AssignedDateEntry.Date = DateTime.Now;
        AssignedDateEntry.IsEnabled = e.Value;
        AssignedDateEntry.IsVisible = e.Value;
    }
    private void EstimatedTimeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        EstimatedTimeEntry.Time = TimeSpan.Zero;
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
            //constructor
            Tasks newTask = new Tasks
            {
                Name = NameEntry.Text,
                DueDate = dueDateOnly,
                AssignedDate = assignedDateOnly,
                EstimatedTime = estimatedTimeOnly,
                CompletedDate = null, // null until completed
                CompletedStatus = false
            };
            //save to database
            TaskViewModel.Current.SaveTask(newTask);

            await Navigation.PushAsync(new DailyView(AssignedDateEntry.Date));
        }

    }

}
