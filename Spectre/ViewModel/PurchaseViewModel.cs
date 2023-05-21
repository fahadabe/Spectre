namespace Spectre.ViewModel
{
    public partial class PurchaseViewModel : ViewModelBase
    {
        public PurchaseViewModel()
        {
            CurrentUser = CommonProperties.CurrentUser;
        }

        #region Properties

        private UserModel _CurrentUser = new();

        public UserModel CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }

        private ObservableCollection<PurchaseModel>? _TodayPurchaseCollection = new();

        public ObservableCollection<PurchaseModel>? TodayPurchaseCollection
        {
            get { return _TodayPurchaseCollection; }
            set
            {
                _TodayPurchaseCollection = value;
                RaisePropertyChanged(nameof(TodayPurchaseCollection));
            }
        }

        private ObservableCollection<PurchaseModel>? _YesterdayPurchaseCollection = new();

        public ObservableCollection<PurchaseModel>? YesterdayPurchaseCollection
        {
            get { return _YesterdayPurchaseCollection; }
            set
            {
                _YesterdayPurchaseCollection = value;
                RaisePropertyChanged(nameof(YesterdayPurchaseCollection));
            }
        }

        private PurchaseModel? _Purchase = new();

        public PurchaseModel? Purchase
        {
            get { return _Purchase; }
            set
            {
                _Purchase = value;
                RaisePropertyChanged(nameof(Purchase));
            }
        }

        private int _YesterdayPurchasesCount;

        public int YesterdayPurchasesCount
        {
            get { return _YesterdayPurchasesCount; }
            set
            {
                _YesterdayPurchasesCount = value;
                RaisePropertyChanged(nameof(YesterdayPurchasesCount));
            }
        }

        private double _YesterdayPurchasesTotal;

        public double YesterdayPurchasesTotal
        {
            get { return _YesterdayPurchasesTotal; }
            set
            {
                _YesterdayPurchasesTotal = value;
                RaisePropertyChanged(nameof(YesterdayPurchasesTotal));
            }
        }

        private int _TodayPurchasesCount;

        public int TodayPurchasesCount
        {
            get { return _TodayPurchasesCount; }
            set
            {
                _TodayPurchasesCount = value;
                RaisePropertyChanged(nameof(TodayPurchasesCount));
            }
        }

        private double _TodayPurchasesTotal;

        public double TodayPurchasesTotal
        {
            get { return _TodayPurchasesTotal; }
            set
            {
                _TodayPurchasesTotal = value;
                RaisePropertyChanged(nameof(TodayPurchasesTotal));
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

        #endregion Properties

        #region Commands

        public AsyncCommand GetPurchaseCollectionCommand => new AsyncCommand(ExecuteGetPurchaseCollection, CanUserViewReport);
        public AsyncCommand AddPurchaseCommand => new AsyncCommand(ExecuteAddPurchase, CanUserAddPurchase);
        public AsyncCommand<PurchaseModel> DeleteTodaySelectedPurchaseCommand => new AsyncCommand<PurchaseModel>(ExecuteDeleteTodaySelectedPurchase, CanUserDeletePurchase);
        public AsyncCommand<PurchaseModel> DeleteYesterdaySelectedPurchaseCommand => new AsyncCommand<PurchaseModel>(ExecuteDeleteYesterdaySelectedPurchase, CanUserDeletePurchase);

        #endregion Commands

        #region Command Functions

        private async Task ExecuteGetPurchaseCollection()
        {
            await GetTodayPurchase();
            await GetYesterdayPurchase();
        }

        private async Task ExecuteAddPurchase()
        {
            Purchase.AddedBy = CurrentUser.Username;
            if (await PurchaseService.AddPurchaseAsync(Purchase))
            {
                if (Purchase.Date == DateTime.Today)
                {
                    await GetTodayPurchase();
                }
                else if (Purchase.Date == DateTime.Today.AddDays(-1))
                {
                    await GetYesterdayPurchase();
                }

                Purchase = null;
                Purchase = new();
            }
        }

        private async Task ExecuteDeleteTodaySelectedPurchase(PurchaseModel obj)
        {
            if (obj is not null)
            {
                if (await PurchaseService.DeletePurchase(obj))
                {
                    //await GetTodayPurchase();
                    TodayPurchaseCollection.Remove(obj);
                    GenerateTodayPurchasesSummary();
                }
            }
        }

        private async Task ExecuteDeleteYesterdaySelectedPurchase(PurchaseModel obj)
        {
            if (obj is not null)
            {
                if (await PurchaseService.DeletePurchase(obj))
                {
                    YesterdayPurchaseCollection.Remove(obj);
                    GenerateYesterdayPurchasesSummary();
                    //await GetYesterdayPurchase();
                }
            }
        }

        private bool CanUserViewReport()
        {
            return CurrentUser.CanViewReport;
        }

        private bool CanUserAddPurchase()
        {
            return CurrentUser.CanAddPurchase;
        }

        private bool CanUserDeletePurchase(PurchaseModel obj)
        {
            return CurrentUser.CanDeletePurchase;
        }

        #endregion Command Functions

        #region Functions

        private async Task GetTodayPurchase()
        {
            TodayPurchaseCollection = await PurchaseService.GetTodayPurchaseAsync();
            GenerateTodayPurchasesSummary();
        }

        private async Task GetYesterdayPurchase()
        {
            YesterdayPurchaseCollection = await PurchaseService.GetYesterdayPurchaseAsync();
            GenerateYesterdayPurchasesSummary();
        }

        private void GenerateYesterdayPurchasesSummary()
        {
            if (YesterdayPurchaseCollection is not null)
            {
                YesterdayPurchasesCount = YesterdayPurchaseCollection.Count;
                YesterdayPurchasesTotal = YesterdayPurchaseCollection.Sum(i => i.Amount);
            }
        }

        private void GenerateTodayPurchasesSummary()
        {
            if (TodayPurchaseCollection is not null)
            {
                TodayPurchasesCount = TodayPurchaseCollection.Count;
                TodayPurchasesTotal = TodayPurchaseCollection.Sum(i => i.Amount);
            }
        }

        #endregion Functions
    }
}