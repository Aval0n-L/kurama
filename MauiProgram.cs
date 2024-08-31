using Kurama.Services;
using Kurama.Services.Interfaces;
using Kurama.ViewModels;
using Microsoft.Extensions.Logging;

namespace Kurama;

public static class MauiProgram
{
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


        builder.Services.AddSingleton<ISpeechRecognitionService, SpeechRecognitionService>();
        builder.Services.AddSingleton<ISpeechSynthesisService, SpeechSynthesisService>();
        builder.Services.AddSingleton<VoiceAssistantViewModel>();
        builder.Services.AddSingleton<MainPage>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
