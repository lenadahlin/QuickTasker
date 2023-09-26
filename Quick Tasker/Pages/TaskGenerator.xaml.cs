namespace Quick_Tasker.Pages;
using System.Diagnostics;
using System.Threading.Tasks;
using Quick_Tasker.Models;
using Quick_Tasker.ViewModels;


public partial class TaskGenerator : ContentPage
{
    private readonly TaskViewModel viewModel;
    //shake flag
    private bool shakeDetected = false;
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

    private void ToggleShakeOn()
    {
        if (Accelerometer.Default.IsSupported)
        {
            if (!Accelerometer.Default.IsMonitoring)
            {
                // Turn on accelerometer
                Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.Default.Start(SensorSpeed.Game);
            }
        }
    }

    private void ToggleShakeOff()
    {
        if (Accelerometer.Default.IsSupported)
        {
            if (Accelerometer.Default.IsMonitoring)
            {
                // Turn off accelerometer
                Accelerometer.Default.Stop();
                Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
            }
        }
    }

    private void Accelerometer_ShakeDetected(object sender, EventArgs e)
    {
        shakeDetected = true;

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
        Tasks randomTask = viewModel.GetRandomTask(EstimatedTimeEntry.Time, AssignedDateEntry.Date);
        //Shake is toggled on 
        ToggleShakeOn();
        if (randomTask == null)
        {
            await DisplayAlert("Warning", "There are no tasks within the given parameters", "OK");
            return;
        }
        else
        {
            //infinite loop to refresh when Generate Again is clicked
            while (true)
            {
                BindingContext = randomTask;
                string taskInfo = "Task Name: " + randomTask.Name + "\nEstimated Time: " + randomTask.EstimatedTime;
                string action = await DisplayActionSheet(taskInfo, "Cancel", null, "Assign Task", "Generate Again");
                if (action == "Generate Again" || shakeDetected)
                {
                    randomTask = viewModel.GetRandomTask(EstimatedTimeEntry.Time, AssignedDateEntry.Date);
                    //reset shake flag
                    shakeDetected = false;
                }
                else if (action == "Cancel")
                {
                    ToggleShakeOff();
                    break;
                }
                else if (action == "Assign Task")
                {
                    randomTask.AssignedDate = AssignedDateEntry.Date;
                    viewModel.SaveTask(randomTask);
                    await Navigation.PushAsync(new DailyView(AssignedDateEntry.Date));
                    break;
                }

            }
        }
    }
}
