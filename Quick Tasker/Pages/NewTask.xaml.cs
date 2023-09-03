namespace Quick_Tasker.Pages;

public partial class NewTask : ContentPage
{
	public NewTask()
	{
		InitializeComponent();
        Console.WriteLine("NewTask page constructor called");
    }
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
}
