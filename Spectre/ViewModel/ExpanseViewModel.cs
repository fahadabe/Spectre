namespace Spectre.ViewModel
{
    public partial class ExpanseViewModel : ViewModelBase
    {
        public ExpanseViewModel()
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

        private ObservableCollection<ExpanseModel>? _TodayExpanseCollection = new();

        public ObservableCollection<ExpanseModel>? TodayExpanseCollection
        {
            get { return _TodayExpanseCollection; }
            set
            {
                _TodayExpanseCollection = value;
                RaisePropertyChanged(nameof(TodayExpanseCollection));
            }
        }

        private ObservableCollection<ExpanseModel>? _YesterdayExpanseCollection = new();

        public ObservableCollection<ExpanseModel>? YesterdayExpanseCollection
        {
            get { return _YesterdayExpanseCollection; }
            set
            {
                _YesterdayExpanseCollection = value;
                RaisePropertyChanged(nameof(YesterdayExpanseCollection));
            }
        }

        private ExpanseModel? _Expanse = new();

        public ExpanseModel? Expanse
        {
            get { return _Expanse; }
            set
            {
                _Expanse = value;
                RaisePropertyChanged(nameof(Expanse));
            }
        }

        private int _YesterdayExpansesCount;

        public int YesterdayExpansesCount
        {
            get { return _YesterdayExpansesCount; }
            set
            {
                _YesterdayExpansesCount = value;
                RaisePropertyChanged(nameof(YesterdayExpansesCount));
            }
        }

        private double _YesterdayExpansesTotal;

        public double YesterdayExpansesTotal
        {
            get { return _YesterdayExpansesTotal; }
            set
            {
                _YesterdayExpansesTotal = value;
                RaisePropertyChanged(nameof(YesterdayExpansesTotal));
            }
        }

        private int _TodayExpansesCount;

        public int TodayExpansesCount
        {
            get { return _TodayExpansesCount; }
            set
            {
                _TodayExpansesCount = value;
                RaisePropertyChanged(nameof(TodayExpansesCount));
            }
        }

        private double _TodayExpansesTotal;

        public double TodayExpansesTotal
        {
            get { return _TodayExpansesTotal; }
            set
            {
                _TodayExpansesTotal = value;
                RaisePropertyChanged(nameof(TodayExpansesTotal));
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

        public AsyncCommand GetExpanseCollectionCommand => new AsyncCommand(ExecuteGetExpanseCollection, CanUserViewReport);
        public AsyncCommand AddExpanseCommand => new AsyncCommand(ExecuteAddExpanse, CanUserAddExpanse);

        public AsyncCommand<ExpanseModel> DeleteTodaySelectedExpanseCommand => new AsyncCommand<ExpanseModel>(ExecuteDeleteTodaySelectedExpanse, CanUserDeleteExpanse);
        public AsyncCommand<ExpanseModel> DeleteYesterdaySelectedExpanseCommand => new AsyncCommand<ExpanseModel>(ExecuteDeleteYesterdaySelectedExpanse, CanUserDeleteExpanse);

        #endregion Commands

        #region Command Functions

        private async Task ExecuteGetExpanseCollection()
        {
            await GetTodayExpanse();
            await GetYesterdayExpanse();
        }

        private async Task ExecuteAddExpanse()
        {
            Expanse.AddedBy = CurrentUser.Username;
            if (await ExpanseService.AddExpanseAsync(Expanse))
            {
                if (Expanse.Date == DateTime.Today)
                {
                    await GetTodayExpanse();
                }
                else if (Expanse.Date == DateTime.Today.AddDays(-1))
                {
                    await GetYesterdayExpanse();
                }

                Expanse = null;
                Expanse = new();
            }
        }

        private async Task ExecuteDeleteTodaySelectedExpanse(ExpanseModel obj)
        {
            if (obj is not null)
            {
                if (await ExpanseService.DeleteExpanse(obj))
                {
                    //await GetTodayExpanse();
                    TodayExpanseCollection!.Remove(obj);
                    GenerateTodayExpansesSummary();
                }
            }
        }

        private async Task ExecuteDeleteYesterdaySelectedExpanse(ExpanseModel obj)
        {
            if (obj is not null)
            {
                if (await ExpanseService.DeleteExpanse(obj))
                {
                    //await GetYesterdayExpanse();
                    YesterdayExpanseCollection!.Remove(obj);
                    GenerateYesterdayExpansesSummary();
                }
            }
        }

        private bool CanUserViewReport()
        {
            return CurrentUser.CanViewReport;
        }

        private bool CanUserAddExpanse()
        {
            return CurrentUser.CanAddExpanse;
        }

        private bool CanUserDeleteExpanse(ExpanseModel obj)
        {
            return CurrentUser.CanDeleteExpanse;
        }

        #endregion Command Functions

        #region Functions

        private async Task GetTodayExpanse()
        {
            TodayExpanseCollection = await ExpanseService.GetTodayExpanseAsync();
            GenerateTodayExpansesSummary();
        }

        private async Task GetYesterdayExpanse()
        {
            YesterdayExpanseCollection = await ExpanseService.GetYesterdayExpanseAsync();
            GenerateYesterdayExpansesSummary();
        }

        private void GenerateYesterdayExpansesSummary()
        {
            if (YesterdayExpanseCollection is not null)
            {
                YesterdayExpansesCount = YesterdayExpanseCollection.Count;
                YesterdayExpansesTotal = YesterdayExpanseCollection.Sum(i => i.Amount);
            }
        }

        private void GenerateTodayExpansesSummary()
        {
            if (TodayExpanseCollection is not null)
            {
                TodayExpansesCount = TodayExpanseCollection.Count;
                TodayExpansesTotal = TodayExpanseCollection.Sum(i => i.Amount);
            }
        }

        #endregion Functions
    }
}