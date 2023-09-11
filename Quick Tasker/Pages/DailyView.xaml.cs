using Quick_Tasker.ViewModels;
namespace Quick_Tasker.Pages;
using Quick_Tasker.Models;

public partial class DailyView : ContentPage
{
    private TaskViewModel viewModel;
    DateTime currentDate = DateTime.Today;
    public DailyView()
    {
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
        string formattedDate = currentDate.ToString("dddd\n d MMMM");
        CurrentDate.Text = formattedDate;
        //why cant i do this in the xaml :(
     //   CurrentDate.HorizontalTextAlignment = TextAlignment.Center;
        DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
        //NavigationPage.SetHasNavigationBar(this, false);
    }
    //refreshes tasks whenever page opens
    protected override void OnAppearing()
    {
        base.OnAppearing();
        DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
    }
    private void CheckedComplete(object sender, CheckedChangedEventArgs e)
    {

        if (sender is CheckBox checkBox && checkBox.BindingContext is Tasks task)
        {
            if (task.CompletedStatus && task.CompletedDate == null)
            {
                // if checkbox is checked, change CompletedDate to today
                task.CompletedDate = DateTime.Now;
                viewModel.SaveTask(task);
                DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
            }
            else if (!task.CompletedStatus && task.CompletedDate != null)
            {
                // if checkbox is un-checked, change CompletedDate to null
                task.CompletedDate = null;
                viewModel.SaveTask(task);
                DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
            }
        }
    }
    private void IncreaseDate(object sender, EventArgs args)
    {
        //setting date for label and database stuff
        currentDate = currentDate.AddDays(1);
        string formattedDate = currentDate.ToString("dddd\n d MMMM");
        CurrentDate.Text = formattedDate;
        DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
    }
    private void DecreaseDate(object sender, EventArgs args)
    {
        //setting date for label and database stuff
        currentDate = currentDate.AddDays(-1);
        string formattedDate = currentDate.ToString("dddd\n d MMMM");
        CurrentDate.Text = formattedDate;
        DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
    }
}
//private void NewTaskClicked(object sender, EventArgs args)
//{
//    Navigation.PushAsync(new NewTask());
//}

//private async void NewTaskClicked(object sender, EventArgs args)
//{
//    await Navigation.PushAsync(new NewTask());
//}
//private void OpenFlyout(object sender, EventArgs args)
//{
//    //shows flyout when clicked
//    Console.WriteLine("OpenFlyout method called");
//    Shell.Current.FlyoutIsPresented = true;
//}