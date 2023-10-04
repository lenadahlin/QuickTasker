namespace Quick_Tasker;
using Quick_Tasker.Pages;

public partial class App : Application
{
	public App()
	{
        //Register Syncfusion license with key
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9edHRTQ2NYVkN1WUE=");
        InitializeComponent();
		MainPage = new AppShell();
	}
}

