using Syncfusion.Maui.Calendar;

namespace Quick_Tasker.Pages;

public partial class Calendar : ContentPage
{
	public Calendar()
	{
		InitializeComponent();
	}

    private async void OnCalendarSelectionChanged(System.Object sender, CalendarSelectionChangedEventArgs e)
    {
        await Navigation.PushAsync(new DailyView((DateTime)e.NewValue));
    }


    

}
