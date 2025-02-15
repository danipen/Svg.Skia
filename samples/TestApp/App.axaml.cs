using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TestApp.ViewModels;
using TestApp.Views;

namespace TestApp;

public class App : Application
{
    private const string ConfigurationPath = "TestApp.json";

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var mainWindowViewModel = new MainWindowViewModel();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (File.Exists(ConfigurationPath))
            {
                using var stream = File.OpenRead(ConfigurationPath);
                mainWindowViewModel.LoadConfiguration(stream);
            }

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            desktop.Exit += (_, _) =>
            {
                using var stream = File.OpenWrite(ConfigurationPath);
                mainWindowViewModel.SaveConfiguration(stream);
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime single)
        {
            if (File.Exists(ConfigurationPath))
            {
                using var stream = File.OpenRead(ConfigurationPath);
                mainWindowViewModel.LoadConfiguration(stream);
            }

            single.MainView = new MainView
            {
                DataContext = mainWindowViewModel
            };

            single.MainView.DetachedFromVisualTree += (_, _) =>
            {
                using var stream = File.OpenWrite(ConfigurationPath);
                mainWindowViewModel.SaveConfiguration(stream);
            }; 
        }

        base.OnFrameworkInitializationCompleted();
    }
}
