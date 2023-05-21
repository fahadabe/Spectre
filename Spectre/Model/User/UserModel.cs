namespace Spectre.Model
{
    public class UserModel : BindableBase
    {
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set
            {
                _UserID = value;
                RaisePropertyChanged(nameof(UserID));
            }
        }

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private bool _CanAddExpanse;

        public bool CanAddExpanse
        {
            get { return _CanAddExpanse; }
            set
            {
                _CanAddExpanse = value;
                RaisePropertyChanged(nameof(CanAddExpanse));
            }
        }

        private bool _CanDeleteExpanse;

        public bool CanDeleteExpanse
        {
            get { return _CanDeleteExpanse; }
            set
            {
                _CanDeleteExpanse = value;
                RaisePropertyChanged(nameof(CanDeleteExpanse));
            }
        }

        private bool _CanAddSale;

        public bool CanAddSale
        {
            get { return _CanAddSale; }
            set
            {
                _CanAddSale = value;
                RaisePropertyChanged(nameof(CanAddSale));
            }
        }

        private bool _CanDeleteSale;

        public bool CanDeleteSale
        {
            get { return _CanDeleteSale; }
            set
            {
                _CanDeleteSale = value;
                RaisePropertyChanged(nameof(CanDeleteSale));
            }
        }

        private bool _CanAddPurchase;

        public bool CanAddPurchase
        {
            get { return _CanAddPurchase; }
            set
            {
                _CanAddPurchase = value;
                RaisePropertyChanged(nameof(CanAddPurchase));
            }
        }

        private bool _CanDeletePurchase;

        public bool CanDeletePurchase
        {
            get { return _CanDeletePurchase; }
            set
            {
                _CanDeletePurchase = value;
                RaisePropertyChanged(nameof(CanDeletePurchase));
            }
        }

        private bool _CanViewReport;

        public bool CanViewReport
        {
            get { return _CanViewReport; }
            set
            {
                _CanViewReport = value;
                RaisePropertyChanged(nameof(CanViewReport));
            }
        }

        private bool _ManageUsers;

        public bool ManageUsers
        {
            get { return _ManageUsers; }
            set
            {
                _ManageUsers = value;
                RaisePropertyChanged(nameof(ManageUsers));
            }
        }
    }
}