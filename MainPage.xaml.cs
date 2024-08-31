using Kurama.ViewModels;

namespace Kurama;

public partial class MainPage : ContentPage
{
    public MainPage(VoiceAssistantViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
