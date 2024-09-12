namespace InAndEx;
using System.IO;

public partial class App : Application
{
    public static string FolderPath { get; private set;}
    public static Data.ListDatabase Database { get; private set;}
    public App()
    {
        InitializeComponent();

        FolderPath = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData));
        Database = new Data.ListDatabase();
        MainPage = new Views.FlyoutSamplePage();
    }
}