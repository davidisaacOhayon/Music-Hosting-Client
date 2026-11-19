using System.Configuration;
using System.Data;
using System.Windows;

namespace fake_spotify;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{   
    protected override void OnStartup(StartupEventArgs args) {
       Console.WriteLine("Test runing here");
       MainExecution.Program.Init();
    }
    
    
}

