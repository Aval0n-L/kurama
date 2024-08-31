using Kurama.Services;
using Kurama.ViewModels;

namespace Kurama;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
