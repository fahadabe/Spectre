namespace Spectre.Model
{
    public class MonthlyExpanseModel : BindableBase
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
    }
}