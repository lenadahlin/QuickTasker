namespace Quick_Tasker;
using Quick_Tasker.Pages;

public partial class App : Application
{
	public App()
	{
        //Register Syncfusion license
		//TODO Update key with community license when received
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1NpTHxbf1xzZFxMZFtbRXJPMyBoS35RdURjWX1eeXBSR2RZVUJz");
        InitializeComponent();
		MainPage = new AppShell();

		//MainPage = new NavigationPage(new DailyView());
	}
}

