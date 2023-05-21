namespace Spectre.Model;

public class MonthlySaleModel : BindableBase
{
    private string _Month;

    public string Month
    {
        get { return _Month; }
        set
        {
            _Month = value;
            RaisePropertyChanged(nameof(Month));
        }
    }

    private decimal _Sale;

    public decimal Sale
    {
        get { return _Sale; }
        set
        {
            _Sale = value;
            RaisePropertyChanged(nameof(Sale));
        }
    }
}