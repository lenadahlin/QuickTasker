namespace Quick_Tasker.Pages;

using System.Diagnostics;
using Quick_Tasker.Models;
using Quick_Tasker.ViewModels;
public partial class TaskGenerator : ContentPage
{
    private readonly TaskViewModel viewModel;
    public TaskGenerator()
	{
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AssignedDateEntry.Date = DateTime.Now;
        EstimatedTimeEntry.Time = TimeSpan.Zero;
    }
    private async void GenerateButton_Clicked(System.Object sender, System.EventArgs e)
    {
        //error checking for 0 or date before today
        if (EstimatedTimeEntry.Time == TimeSpan.Zero)
        {
            await DisplayAlert("Warning", "Time must not be 0", "OK");
            return;
        }
        if (AssignedDateEntry.Date < DateTime.Today)
        {
            await DisplayAlert("Warning", "Date must be on or after today", "OK");
            return;
        }
        viewModel.PrintAllTasks();
        Tasks randomTask = viewModel.GetRandomTask(EstimatedTimeEntry.Time);
        Debug.WriteLine("Available Time entry: " + EstimatedTimeEntry.Time);
        Debug.WriteLine("AssignedDate: " + randomTask.AssignedDate);
        if (randomTask == null)
        {
            await DisplayAlert("Warning", "There are no tasks within the given parameters", "OK");
            return;
        } else
        {
            BindingContext = randomTask;
            string action = await DisplayActionSheet("Assign Task?", "Cancel", null, "Email", "Twitter", "Facebook");
        }

    }
}
