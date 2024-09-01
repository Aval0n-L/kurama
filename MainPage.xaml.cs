using Kurama.Services.Interfaces;
using Kurama.ViewModels;

namespace Kurama;

public partial class MainPage : ContentPage
{
    private readonly ISpeechRecognitionService _speechRecognitionService;
    //private readonly ISpeechSynthesisService _speechSynthesisService;

    public MainPage(
        VoiceAssistantViewModel viewModel, 
        ISpeechRecognitionService speechRecognitionService)
        //ISpeechSynthesisService speechSynthesisService)
    {
        InitializeComponent();
        BindingContext = viewModel;

        _speechRecognitionService = speechRecognitionService;
        //_speechSynthesisService = speechSynthesisService;

        //_speechSynthesisService.OnAmplitudeChanged += amplitudes =>
        //{
        //    Dispatcher.Dispatch(() =>
        //    {
        //        AudioVisualizer.UpdateAmplitudes(amplitudes);
        //    });
        //};
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _speechRecognitionService.InitializeAsync(); 
        _speechRecognitionService.StartRecognition();
    }
}
