namespace Spectre.Model
{
    public partial class PurchaseModel : BindableBase
    {
        public int _PurchaseID;

        public int PurchaseID
        {
            get { return _PurchaseID; }
            set
            {
                _PurchaseID = value;
                RaisePropertyChanged(nameof(PurchaseID));
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