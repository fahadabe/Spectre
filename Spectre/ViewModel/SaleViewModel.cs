namespace Spectre.ViewModel
{
    public partial class SaleViewModel : ViewModelBase
    {
        public SaleViewModel()
        {
            CurrentUser = CommonProperties.CurrentUser;
            ExecuteGetSaleCollection();
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

        private ObservableCollection<SaleModel>? _TodaySaleCollection = new();

        public ObservableCollection<SaleModel>? TodaySaleCollection
        {
            get { return _TodaySaleCollection; }
            set
            {
                _TodaySaleCollection = value;
                RaisePropertyChanged(nameof(TodaySaleCollection));
            }
        }

        private ObservableCollection<SaleModel>? _YesterdaySaleCollection = new();

        public ObservableCollection<SaleModel>? YesterdaySaleCollection
        {
            get { return _YesterdaySaleCollection; }
            set
            {
                _YesterdaySaleCollection = value;
                RaisePropertyChanged(nameof(YesterdaySaleCollection));
            }
        }

        private SaleModel? _Sale = new();

        public SaleModel? Sale
        {
            get { return _Sale; }
            set
            {
                _Sale = value;
                RaisePropertyChanged(nameof(Sale));
            }
        }

        private int _YesterdaySalesCount;

        public int YesterdaySalesCount
        {
            get { return _YesterdaySalesCount; }
            set
            {
                _YesterdaySalesCount = value;
                RaisePropertyChanged(nameof(YesterdaySalesCount));
            }
        }

        private double _YesterdaySalesTotal;

        public double YesterdaySalesTotal
        {
            get { return _YesterdaySalesTotal; }
            set
            {
                _YesterdaySalesTotal = value;
                RaisePropertyChanged(nameof(YesterdaySalesTotal));
            }
        }

        private int _TodaySalesCount;

        public int TodaySalesCount
        {
            get { return _TodaySalesCount; }
            set
            {
                _TodaySalesCount = value;
                RaisePropertyChanged(nameof(TodaySalesCount));
            }
        }

        private double _TodaySalesTotal;

        public double TodaySalesTotal
        {
            get { return _TodaySalesTotal; }
            set
            {
                _TodaySalesTotal = value;
                RaisePropertyChanged(nameof(TodaySalesTotal));
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

        public AsyncCommand GetSaleCollectionCommand => new AsyncCommand(ExecuteGetSaleCollection, CanUserViewReport);
        public AsyncCommand AddSaleCommand => new AsyncCommand(ExecuteAddSale, CanUserAddSale);
        public AsyncCommand<SaleModel> DeleteTodaySelectedSaleCommand => new AsyncCommand<SaleModel>(ExecuteDeleteTodaySelectedSale, CanUserDeleteSale);
        public AsyncCommand<SaleModel> DeleteYesterdaySelectedSaleCommand => new AsyncCommand<SaleModel>(ExecuteDeleteYesterdaySelectedSale, CanUserDeleteSale);

        #endregion Commands

        #region Command Functions

        private async Task ExecuteGetSaleCollection()
        {
            await GetTodaySale();
            await GetYesterdaySale();
        }

        private async Task ExecuteAddSale()
        {
            Sale.AddedBy = CurrentUser.Username;
            if (await SaleService.AddSaleAsync(Sale))
            {
                if (Sale.Date == DateTime.Today)
                {
                    await GetTodaySale();
                }
                else if (Sale.Date == DateTime.Today.AddDays(-1))
                {
                    await GetYesterdaySale();
                }

                Sale = null;
                Sale = new();
            }
        }

        private async Task ExecuteDeleteTodaySelectedSale(SaleModel obj)
        {
            if (obj is not null)
            {
                if (await SaleService.DeleteSale(obj))
                {
                    //await GetTodaySale();
                    TodaySaleCollection!.Remove(obj);
                    GenerateTodaySaleSummary();
                }
            }
        }

        private async Task ExecuteDeleteYesterdaySelectedSale(SaleModel obj)
        {
            if (obj is not null)
            {
                if (await SaleService.DeleteSale(obj))
                {
                    //await GetYesterdaySale();
                    YesterdaySaleCollection.Remove(obj);
                    GenerateYesterdaySaleSummary();
                }
            }
        }

        private bool CanUserViewReport()
        {
            return CurrentUser.CanViewReport;
        }

        private bool CanUserAddSale()
        {
            return CurrentUser.CanAddSale;
        }

        private bool CanUserDeleteSale(SaleModel obj)
        {
            return CurrentUser.CanDeleteSale;
        }

        #endregion Command Functions

        #region Functions

        private async Task GetTodaySale()
        {
            TodaySaleCollection = await SaleService.GetTodaySaleAsync();
            GenerateTodaySaleSummary();
        }

        private async Task GetYesterdaySale()
        {
            YesterdaySaleCollection = await SaleService.GetYesterdaySaleAsync();
            GenerateYesterdaySaleSummary();
        }

        private void GenerateYesterdaySaleSummary()
        {
            if (YesterdaySaleCollection is not null)
            {
                YesterdaySalesCount = YesterdaySaleCollection.Count;
                YesterdaySalesTotal = YesterdaySaleCollection.Sum(i => i.Amount);
            }
        }

        private void GenerateTodaySaleSummary()
        {
            if (TodaySaleCollection is not null)
            {
                TodaySalesCount = TodaySaleCollection.Count;
                TodaySalesTotal = TodaySaleCollection.Sum(i => i.Amount);
            }
        }

        #endregion Functions
    }
}