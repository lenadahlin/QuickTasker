namespace Quick_Tasker.Pages;

public partial class TaskGenerator : ContentPage
{
	public TaskGenerator()
	{
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
    }
}
