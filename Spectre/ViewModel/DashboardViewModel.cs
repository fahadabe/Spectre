namespace Spectre.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel()
        {
            CurrentUser = CommonProperties.CurrentUser;
        }

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

        private TodayPerformance? _TodayPerformance = new();

        public TodayPerformance? TodayPerformance
        {
            get { return _TodayPerformance; }
            set
            {
                _TodayPerformance = value;
                RaisePropertyChanged(nameof(TodayPerformance));
            }
        }

        private YesterdayPerformance? _YesterdayPerformance = new();

        public YesterdayPerformance? YesterdayPerformance
        {
            get { return _YesterdayPerformance; }
            set
            {
                _YesterdayPerformance = value;
                RaisePropertyChanged(nameof(YesterdayPerformance));
            }
        }

        private ThisWeekPerformance? _ThisWeekPerformance = new();

        public ThisWeekPerformance? ThisWeekPerformance
        {
            get { return _ThisWeekPerformance; }
            set
            {
                _ThisWeekPerformance = value;
                RaisePropertyChanged(nameof(ThisWeekPerformance));
            }
        }

        private ThisMonthPerformance? _ThisMonthPerformance = new();

        public ThisMonthPerformance? ThisMonthPerformance
        {
            get { return _ThisMonthPerformance; }
            set
            {
                _ThisMonthPerformance = value;
                RaisePropertyChanged(nameof(ThisMonthPerformance));
            }
        }

        private YTDPerformance? _YTDPerformance = new();

        public YTDPerformance? YTDPerformance
        {
            get { return _YTDPerformance; }
            set
            {
                _YTDPerformance = value;
                RaisePropertyChanged(nameof(YTDPerformance));
            }
        }

        private AllPerformance? _AllPerformance = new();

        public AllPerformance? AllPerformance
        {
            get { return _AllPerformance; }
            set
            {
                _AllPerformance = value;
                RaisePropertyChanged(nameof(AllPerformance));
            }
        }

        private ObservableCollection<MonthlySaleModel>? _MonthlySale = new();

        public ObservableCollection<MonthlySaleModel>? MonthlySale
        {
            get { return _MonthlySale; }
            set
            {
                _MonthlySale = value;
                RaisePropertyChanged(nameof(MonthlySale));
            }
        }

        private ObservableCollection<MonthlyExpanseModel>? _MonthlyExpanse = new();

        public ObservableCollection<MonthlyExpanseModel>? MonthlyExpanse
        {
            get { return _MonthlyExpanse; }
            set
            {
                _MonthlyExpanse = value;
                RaisePropertyChanged(nameof(MonthlyExpanse));
            }
        }

        private ObservableCollection<MonthlyPurchaseModel>? _MonthlyPurchase = new();

        public ObservableCollection<MonthlyPurchaseModel>? MonthlyPurchase
        {
            get { return _MonthlyPurchase; }
            set
            {
                _MonthlyPurchase = value;
                RaisePropertyChanged(nameof(MonthlyPurchase));
            }
        }

        private string? _MonthlySalePerformanceTitle = "This year";

        public string? MonthlySalePerformanceTitle
        {
            get { return _MonthlySalePerformanceTitle; }
            set
            {
                _MonthlySalePerformanceTitle = value;
                RaisePropertyChanged(nameof(MonthlySalePerformanceTitle));
            }
        }

        private bool _OnlyCurrentYear;

        public bool OnlyCurrentYear
        {
            get { return _OnlyCurrentYear; }
            set
            {
                _OnlyCurrentYear = value;
                RaisePropertyChanged(() => OnlyCurrentYear);

                if (OnlyCurrentYear)
                {
                    MonthlySalePerformanceTitle = "This year";
                }
                else
                {
                    MonthlySalePerformanceTitle = "All year";
                }

                GenerateMonthlySaleExpanseAndPurchaseChartData();
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

        public DelegateCommand GetDataCommand => new DelegateCommand(ExecuteGetData, CanViewReport);

        private void ExecuteGetData()
        {
            OnlyCurrentYear = true;
            TodayPerformance = DashboardService.GetTodayPerformance();
            YesterdayPerformance = DashboardService.GetYesterdayPerformance();
            ThisWeekPerformance = DashboardService.GetThisWeekPerformance();
            ThisMonthPerformance = DashboardService.GetThisMonhPerformance();
            YTDPerformance = DashboardService.GetYTDPerformance();
            AllPerformance = DashboardService.GetAllPerformance();
            TodayPerformance.CurrencySymbol = CurrencySymbol;
            YesterdayPerformance.CurrencySymbol = CurrencySymbol;
            ThisWeekPerformance.CurrencySymbol = CurrencySymbol;
            ThisMonthPerformance.CurrencySymbol = CurrencySymbol;
            YTDPerformance.CurrencySymbol = CurrencySymbol;
            AllPerformance.CurrencySymbol = CurrencySymbol;
            //GenerateMonthlySaleExpanseAndPurchaseChartData();
        }

        private void GenerateMonthlySaleExpanseAndPurchaseChartData()
        {
            if (CanViewReport())
            {
                MonthlySale.Clear();
                MonthlyExpanse.Clear();
                MonthlyPurchase.Clear();
                MonthlySale = DashboardService.GetSaleChartData(OnlyCurrentYear);
                MonthlyExpanse = DashboardService.GetExpanseChartData(OnlyCurrentYear);
                MonthlyPurchase = DashboardService.GetPurchaseChartData(OnlyCurrentYear);
            }
        }

        private bool CanViewReport()
        {
            return CurrentUser.CanViewReport;
        }
    }
}