using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using InAndEx.Models;

namespace InAndEx.Views;
public partial class perYear : ContentPage
{
    ObservableCollection<Models.List> list { get; set; }
    public ICommand DeleteList { get; set; }
    public ICommand EditList { get; set; }

    public perYear()
	{
		InitializeComponent();
        list = new ObservableCollection<Models.List>();
        listView.ItemsSource = list;
    }

    public class ChartData
    {
        public string XValue { get; set; }
        public double YValue { get; set; }
    }

    public class ChartViewModel
    {
        public ObservableCollection<ChartData> Data { get; set; }
        public ChartViewModel(double income, double expenses)
        {
            Data = new ObservableCollection<ChartData>
                {
                    new ChartData { XValue = "รายรับ", YValue = income },
                    new ChartData { XValue = "รายจ่าย", YValue = expenses },
                };
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        list.Clear();
        LoadListFromSQLite();
    }

    private async void LoadListFromSQLite()
    {
        var listdb = await App.Database.GetListsAsync();
        foreach (var rowx in listdb)
            list.Add(rowx);
        FilterAndShowMoney();
    }

    private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new Edit
            {
                BindingContext = e.SelectedItem as List
            });
        }
    }

    private double TotalIncome()
    {
        double income = 0;
        var selectedDate = datePicker.Date;

        foreach (var item in list.Where(
            x => x.Date.Year == selectedDate.Year && x.Type == "Green"))
        {
            income += double.Parse(item.Money);
        }
        return income;
    }

    private double TotalExpenses()
    {
        double expenses = 0;
        var selectedDate = datePicker.Date;

        foreach (var item in list.Where(
            x => x.Date.Year == selectedDate.Year && x.Type == "Red"))
        {
            expenses += double.Parse(item.Money);
        }
        return expenses;
    }

    async void GotoIncome(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.income
        {
            BindingContext = new Models.List()
        });
    }

    async void GotoExpenses(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.expenses
        {
            BindingContext = new Models.List()
        });
    }

    private void ShowMoney()
    {
        double income = TotalIncome();
        double expenses = TotalExpenses();
        double total = income - expenses;

        string totalFormatted = total.ToString("N", CultureInfo.InvariantCulture);
        string incomeFormatted = income.ToString("N", CultureInfo.InvariantCulture);
        string expensesFormatted = expenses.ToString("N", CultureInfo.InvariantCulture);

        string TotalText = $"{totalFormatted} บาท";
        string IncomeText = $"{incomeFormatted} บาท";
        string ExpensesText = $"{expensesFormatted} บาท";

        Totalmoney.Text = TotalText;
        Totalincome.Text = IncomeText;
        Totalexpenses.Text = ExpensesText;

        UpdateChartData(income, expenses);
    }

    private void UpdateChartData(double income, double expenses)
    {
        // Create a new ChartViewModel with the updated values
        BindingContext = new ChartViewModel(income, expenses);
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        FilterAndShowMoney();
    }

    private void FilterAndShowMoney()
    {
        var selectedDate = datePicker.Date;
        var filteredList = list.Where(x => x.Date.Year == selectedDate.Year).ToList();

        listView.ItemsSource = new ObservableCollection<Models.List>(filteredList);
        ShowMoney();
    }

    private void OnPreviousClicked(object sender, EventArgs e)
    {
        datePicker.Date = datePicker.Date.AddYears(-1);
        FilterAndShowMoney();
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        datePicker.Date = datePicker.Date.AddYears(1);
        FilterAndShowMoney();
    }
}