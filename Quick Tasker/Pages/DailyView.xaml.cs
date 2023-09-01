namespace Quick_Tasker.Pages;


public partial class DailyView : ContentPage
{
    public DailyView()
    {
        InitializeComponent();
        DateTime currentDate = DateTime.Now;
        string formattedDate = currentDate.ToString("dddd\n d MMMM");
        CurrentDate.HorizontalTextAlignment = TextAlignment.Center;
        CurrentDate.Text = formattedDate;
        Console.WriteLine("DailyView page constructor called");
        NavigationPage.SetHasNavigationBar(this, false);
    }
    //private void NewTaskClicked(object sender, EventArgs args)
    //{
    //    Navigation.PushAsync(new NewTask());
    //}

    private async void NewTaskClicked(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new NewTask());
    }
    private void OpenFlyout(object sender, EventArgs args)
    {
        //shows flyout when clicked
        Console.WriteLine("OpenFlyout method called");
        Shell.Current.FlyoutIsPresented = true;
    }
}