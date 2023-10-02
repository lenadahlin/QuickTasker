namespace Quick_Tasker.Pages;

public partial class Calendar : ContentPage
{
	public Calendar()
	{
		InitializeComponent();
	}

    private async void GoButton_Clicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new DailyView(AssignedDateEntry.Date));
    }
}
