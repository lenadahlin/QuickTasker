using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
namespace Quick_Tasker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        // https://help.syncfusion.com/maui/calendar/overview for calendar
        builder.ConfigureSyncfusionCore();
        builder
			.UseMauiApp<App>()

			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("FontAwesomeBrands.otf", "FABrands");
				fonts.AddFont("FontAwesomeRegular.otf, FARegular");
				fonts.AddFont("FontAwesomeSolid.otf, FASolid");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

