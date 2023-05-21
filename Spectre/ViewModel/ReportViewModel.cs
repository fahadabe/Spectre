namespace Spectre.ViewModel
{
    public partial class ReportViewModel : ViewModelBase
    {
        public ReportViewModel()
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

        private bool _Today = true;

        public bool Today
        {
            get { return _Today; }
            set
            {
                _Today = value;
                RaisePropertyChanged(nameof(Today));
            }
        }

        private bool _Yesterday;

        public bool Yesterday
        {
            get { return _Yesterday; }
            set
            {
                _Yesterday = value;
                RaisePropertyChanged(nameof(Yesterday));
            }
        }

        private bool _All;

        public bool All
        {
            get { return _All; }
            set
            {
                _All = value;
                RaisePropertyChanged(nameof(All));
            }
        }

        private bool _BetweenDates;

        public bool BetweenDates
        {
            get { return _BetweenDates; }
            set
            {
                _BetweenDates = value;
                RaisePropertyChanged(nameof(BetweenDates));
            }
        }

        private ObservableCollection<ExpanseModel>? _ExpanseCollection;

        public ObservableCollection<ExpanseModel>? ExpanseCollection
        {
            get { return _ExpanseCollection; }
            set
            {
                _ExpanseCollection = value;
                RaisePropertyChanged(nameof(ExpanseCollection));
            }
        }

        private ObservableCollection<SaleModel>? _SaleCollection;

        public ObservableCollection<SaleModel>? SaleCollection
        {
            get { return _SaleCollection; }
            set
            {
                _SaleCollection = value;
                RaisePropertyChanged(nameof(SaleCollection));
            }
        }

        private ObservableCollection<PurchaseModel>? _PurchaseCollection;

        public ObservableCollection<PurchaseModel>? PurchaseCollection
        {
            get { return _PurchaseCollection; }
            set
            {
                _PurchaseCollection = value;
                RaisePropertyChanged(nameof(PurchaseCollection));
            }
        }

        private DateTime _FromDate = DateTime.Today;

        public DateTime FromDate
        {
            get { return _FromDate; }
            set
            {
                _FromDate = value;
                RaisePropertyChanged(nameof(FromDate));
            }
        }

        private DateTime _ToDate = DateTime.Today;

        public DateTime ToDate
        {
            get { return _ToDate; }
            set
            {
                _ToDate = value;
                RaisePropertyChanged(nameof(ToDate));
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

        #region Command

        public AsyncCommand GetCollectionCommand => new AsyncCommand(ExecuteGetCollection, CanUserViewReport);

        public AsyncCommand<ExpanseModel> DeleteSelectedExpanseCommand => new AsyncCommand<ExpanseModel>(ExecuteDeleteSelectedExpanse, CanUserDeleteExpanse);

        public AsyncCommand<PurchaseModel> DeleteSelectedPurchaseCommand => new AsyncCommand<PurchaseModel>(ExecuteDeleteSelectedPurchase, CanUserDeletePurchase);

        public AsyncCommand<SaleModel> DeleteSelectedSaleCommand => new AsyncCommand<SaleModel>(ExecuteDeleteSelectedSale, CanUserDeleteSale);

        #endregion Command

        #region CommandFunctions

        private async Task ExecuteGetCollection()
        {
            if (Today)
            {
                FromDate = DateTime.Today;
                ToDate = DateTime.Today;
                await GetTodayCollections();
            }
            else if (Yesterday)
            {
                FromDate = DateTime.Today;
                ToDate = DateTime.Today;
                await GetYesterdayCollections();
            }
            else if (All)
            {
                FromDate = DateTime.Today;
                ToDate = DateTime.Today;
                await GetAllCollections();
            }
            else if (BetweenDates)
            {
                await GetCollectionsBetweenDates();
            }
        }

        private async Task ExecuteDeleteSelectedExpanse(ExpanseModel obj)
        {
            if (obj is not null)
            {
                if (await ExpanseService.DeleteExpanse(obj))
                {
                    ExpanseCollection!.Remove(obj);
                }
            }
        }

        private async Task ExecuteDeleteSelectedPurchase(PurchaseModel obj)
        {
            if (obj is not null)
            {
                if (await PurchaseService.DeletePurchase(obj))
                {
                    PurchaseCollection!.Remove(obj);
                }
            }
        }

        private async Task ExecuteDeleteSelectedSale(SaleModel obj)
        {
            if (obj is not null)
            {
                if (await SaleService.DeleteSale(obj))
                {
                    SaleCollection!.Remove(obj);
                }
            }
        }

        private bool CanUserViewReport()
        {
            return CurrentUser.CanViewReport;
        }

        private bool CanUserDeleteSale(SaleModel obj)
        {
            return CurrentUser.CanDeleteSale;
        }

        private bool CanUserDeletePurchase(PurchaseModel obj)
        {
            return CurrentUser.CanDeletePurchase;
        }

        private bool CanUserDeleteExpanse(ExpanseModel obj)
        {
            return CurrentUser.CanDeleteExpanse;
        }

        #endregion CommandFunctions

        #region Functions

        #region TodayCollection

        private async Task GetTodayCollections()
        {
            await GetTodayExpanse();
            await GetTodaySale();
            await GetTodayPurchase();
        }

        private async Task GetTodayExpanse()
        {
            ExpanseCollection = await ExpanseService.GetTodayExpanseAsync();
        }

        private async Task GetTodaySale()
        {
            SaleCollection = await SaleService.GetTodaySaleAsync();
        }

        private async Task GetTodayPurchase()
        {
            PurchaseCollection = await PurchaseService.GetTodayPurchaseAsync();
        }

        #endregion TodayCollection

        #region YesterdayCollection

        private async Task GetYesterdayCollections()
        {
            await GetYesterdayExpanse();
            await GetYesterdaySale();
            await GetYesterdayPurchase();
        }

        private async Task GetYesterdayExpanse()
        {
            ExpanseCollection = await ExpanseService.GetYesterdayExpanseAsync();
        }

        private async Task GetYesterdaySale()
        {
            SaleCollection = await SaleService.GetYesterdaySaleAsync();
        }

        private async Task GetYesterdayPurchase()
        {
            PurchaseCollection = await PurchaseService.GetYesterdayPurchaseAsync();
        }

        #endregion YesterdayCollection

        #region AllCollection

        private async Task GetAllCollections()
        {
            await GetAllExpanse();
            await GetAllSale();
            await GetAllPurchase();
        }

        private async Task GetAllExpanse()
        {
            ExpanseCollection = await ExpanseService.GetAllCollectionAsync();
        }

        private async Task GetAllSale()
        {
            SaleCollection = await SaleService.GetAllCollectionAsync();
        }

        private async Task GetAllPurchase()
        {
            PurchaseCollection = await PurchaseService.GetAllCollectionAsync();
        }

        #endregion AllCollection

        #region BetweenDatesCollection

        private async Task GetCollectionsBetweenDates()
        {
            await GetExpanseBetweenDate();
            await GetSaleBetweenDate();
            await GetPurchaseBetweenDate();
        }

        private async Task GetExpanseBetweenDate()
        {
            ExpanseCollection = await ExpanseService.GetExpanseBetweenTwoDatesAsync(FromDate, ToDate);
        }

        private async Task GetSaleBetweenDate()
        {
            SaleCollection = await SaleService.GetSaleBetweenTwoDatesAsync(FromDate, ToDate);
        }

        private async Task GetPurchaseBetweenDate()
        {
            PurchaseCollection = await PurchaseService.GetPurchaseBetweenTwoDatesAsync(FromDate, ToDate);
        }

        #endregion BetweenDatesCollection

        #endregion Functions
    }
}