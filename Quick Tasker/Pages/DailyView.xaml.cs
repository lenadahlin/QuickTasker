using Quick_Tasker.ViewModels;
namespace Quick_Tasker.Pages;
using Quick_Tasker.Models;

public partial class DailyView : ContentPage
{
    private TaskViewModel viewModel;
    DateTime currentDate = DateTime.Today;
    public DailyView()
    {
        Initialize(DateTime.Today);
    }
    public DailyView(DateTime selectedDate)
    {
        Initialize(selectedDate);
    }

    //initialize for specific date
    private void Initialize(DateTime selectedDate)
    {
        currentDate = selectedDate;
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
        string formattedDate = currentDate.ToString("dddd\n d MMMM");
        CurrentDate.Text = formattedDate;
        DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
    }
    //refreshes tasks whenever page opens
    protected override void OnAppearing()
    {
        base.OnAppearing();
        DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);

    }
    //updates item source and updates database when tasks are checked as completed
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

    //swipe handler for increasing and decreasing dates
    void OnSwiped(object sender, SwipedEventArgs e)
    {
        if (e.Direction == SwipeDirection.Left)
        {
            currentDate = currentDate.AddDays(1);
            string formattedDate = currentDate.ToString("dddd\n d MMMM");
            CurrentDate.Text = formattedDate;
            DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
        }
        if (e.Direction == SwipeDirection.Right)
        {
            currentDate = currentDate.AddDays(-1);
            string formattedDate = currentDate.ToString("dddd\n d MMMM");
            CurrentDate.Text = formattedDate;
            DailyListView.ItemsSource = viewModel.GetAssignedTasks(currentDate);
        }
    }

    private void EditButton_Clicked(System.Object sender, System.EventArgs e)
    {
        if (sender is ImageButton imgButton && imgButton.BindingContext is Tasks task)
        {
            Navigation.PushAsync(new EditTask((task)));
        }
    }
}
