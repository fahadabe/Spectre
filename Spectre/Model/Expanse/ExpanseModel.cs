namespace Spectre.Model
{
    public class ExpanseModel : BindableBase
    {
        private bool _ShowUsername;

        public bool ShowUsername
        {
            get { return _ShowUsername; }
            set
            {
                _ShowUsername = value;
                RaisePropertyChanged(nameof(ShowUsername));
            }
        }

        private int _ExpanseID;

        public int ExpanseID
        {
            get { return _ExpanseID; }
            set
            {
                _ExpanseID = value;
                RaisePropertyChanged(nameof(ExpanseID));
            }
        }

        private DateTime _Date = DateTime.Today;

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                RaisePropertyChanged(nameof(Date));
            }
        }

        private string? _Description;

        public string? Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private double _Amount;

        public double Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        private string? _AddedBy;

        public string? AddedBy
        {
            get { return _AddedBy; }
            set
            {
                _AddedBy = value;
                RaisePropertyChanged(nameof(AddedBy));
            }
        }
    }
}