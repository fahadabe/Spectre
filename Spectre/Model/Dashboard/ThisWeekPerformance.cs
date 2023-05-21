namespace Spectre.Model
{
    public class ThisWeekPerformance : BindableBase
    {
        private string? _Identifier = "This Week";

        public string? Identifier
        {
            get { return _Identifier; }
            set
            {
                _Identifier = value;
                RaisePropertyChanged(nameof(Identifier));
            }
        }

        private double _Sale;

        public double Sale
        {
            get { return _Sale; }
            set
            {
                _Sale = value;
                RaisePropertyChanged(nameof(Sale));
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

        private double _Expanse;

        public double Expanse
        {
            get { return _Expanse; }
            set
            {
                _Expanse = value;
                RaisePropertyChanged(nameof(Expanse));
            }
        }

        private string _CurrencySymbol;

        public string CurrencySymbol
        {
            get { return _CurrencySymbol; }
            set
            {
                _CurrencySymbol = value;
                RaisePropertyChanged(nameof(CurrencySymbol));
            }
        }
    }
}