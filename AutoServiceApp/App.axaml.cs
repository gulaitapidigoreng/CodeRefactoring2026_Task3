using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AutoServiceApp.Services;
using AutoServiceApp.ViewModels;

namespace AutoServiceApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var globalManager = new AutoServiceManager();

            globalManager.Load();

            desktop.MainWindow = new MainWindow(globalManager)
            {
                DataContext = new MainWindowViewModel(globalManager)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}