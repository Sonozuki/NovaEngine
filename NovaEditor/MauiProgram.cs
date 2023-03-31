using Microsoft.Extensions.Logging;

namespace NovaEditor;

/// <summary>The application entry point.</summary>
public static class MauiProgram
{
    /*********
    ** Public Methods
    *********/
    /// <summary>The application entry point.</summary>
    /// <returns>The configured app.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
