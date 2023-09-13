using AndroidX.Lifecycle;

namespace Quick_Tasker.Pages;

public partial class RecentlyDeleted : ContentPage
{
	public RecentlyDeleted()
	{
		InitializeComponent();
	}
    void OnSwiped(object sender, SwipedEventArgs e)
    {
        if (e.Direction == SwipeDirection.Left)
        {
            Shell.Current.GoToAsync("//TaskBank");
        }
        if (e.Direction == SwipeDirection.Right)
        {
            Shell.Current.GoToAsync("//TaskBank");
        }
    }
}
