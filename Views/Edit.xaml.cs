namespace InAndEx.Views;

public partial class Edit : ContentPage
{
	public Edit()
	{
		InitializeComponent();
	}

    async void SaveButtonClicked(object sender, EventArgs e)
    {
        Models.List list = (Models.List)BindingContext;
        list.Date = datePicker.Date;
        list.Money = string.IsNullOrWhiteSpace(list.Money)
            ? "0" : list.Money;
        await App.Database.SaveListAsync(list);
        await Navigation.PopAsync();
    }

    async void DelButtonClicked(object sender, EventArgs e)
    {
        Models.List list = (Models.List)BindingContext;
        await App.Database.DeleteListAsync(list);
        await Navigation.PopAsync();
    }
}