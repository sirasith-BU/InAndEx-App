using System.Collections.ObjectModel;

namespace InAndEx
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ChartViewModel();
        }
    }

    public class ChartData
    {
        public string XValue { get; set; }
        public double YValue { get; set; }
    }

    public class ChartViewModel
    {
        public ObservableCollection<ChartData> Data { get; set; }

        public ChartViewModel()
        {
            Data = new ObservableCollection<ChartData>
        {
            new ChartData { XValue = "Category1", YValue = 35 },
            new ChartData { XValue = "Category2", YValue = 25 },
            new ChartData { XValue = "Category3", YValue = 15 },
            new ChartData { XValue = "Category4", YValue = 25 }
        };
        }
    }
}