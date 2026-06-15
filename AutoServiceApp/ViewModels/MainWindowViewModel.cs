using AutoServiceApp.Services;

namespace AutoServiceApp.ViewModels;

public class MainWindowViewModel
{
    public AutoServiceManager Manager { get; }
    public string Title { get; }

    public MainWindowViewModel(AutoServiceManager manager)
    {
        Manager = manager;
        Title = "Auto Service";
    }
}