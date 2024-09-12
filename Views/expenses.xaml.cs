namespace InAndEx.Views;

public partial class expenses : ContentPage
{
	public expenses()
	{
		InitializeComponent();
	}

    async void SaveButtonClicked(object sender, EventArgs e)
    {
        Models.List list = (Models.List)BindingContext;
        list.Date = datePicker.Date;
        list.Money = string.IsNullOrWhiteSpace(list.Money)
            ? "0" : list.Money;
        list.Type = "Red";
        await App.Database.SaveListAsync(list);
        await Navigation.PopAsync();
    }
}