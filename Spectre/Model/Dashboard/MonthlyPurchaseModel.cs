namespace Spectre.Model
{
    public class MonthlyPurchaseModel : BindableBase
    {
        private string? _Month;

        public string? Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                RaisePropertyChanged(nameof(Month));
            }
        }

        private double _Purchase;

        public double Purchase
        {
            get { return _Purchase; }
            set
            {
                _Purchase = value;
                RaisePropertyChanged(nameof(Purchase));
            }
        }
    }
}