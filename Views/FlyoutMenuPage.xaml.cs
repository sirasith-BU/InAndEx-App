namespace InAndEx.Views;

public partial class FlyoutMenuPage : ContentPage
{
	public FlyoutMenuPage()
	{
		InitializeComponent();
	}
	
    private void GotoPerDay(object sender, TappedEventArgs e)
    {
		var navPage = App.Current.MainPage as FlyoutSamplePage;
		navPage.Detail = new NavigationPage(new Views.perDay());
    }

    private void GotoPerMonth(object sender, TappedEventArgs e)
    {
        var navPage = App.Current.MainPage as FlyoutSamplePage;
        navPage.Detail = new NavigationPage(new Views.perMonth());
    }

    private void GotoPerYear(object sender, TappedEventArgs e)
    {
        var navPage = App.Current.MainPage as FlyoutSamplePage;
        navPage.Detail = new NavigationPage(new Views.perYear());
    }
}