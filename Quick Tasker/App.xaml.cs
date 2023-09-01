namespace Quick_Tasker;
using Quick_Tasker.Pages;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();

		//MainPage = new NavigationPage(new DailyView());
	}
}

