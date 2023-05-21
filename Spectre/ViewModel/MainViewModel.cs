namespace Spectre.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        public DelegateCommand<ThemedWindow> LogoutCommand { get; }

        public MainViewModel()
        {
            CheckingSettings();
            ExecuteNewTab();
            LogoutCommand = new DelegateCommand<ThemedWindow>(ExecuteLogout);
        }

        #region Properties

        private NavigationTab? _SelectedTab = new();

        public NavigationTab? SelectedTab
        {
            get { return _SelectedTab; }
            set
            {
                _SelectedTab = value;
                RaisePropertyChanged(nameof(SelectedTab));
            }
        }

        private ObservableCollection<NavigationTab>? _Tabs = new();

        public ObservableCollection<NavigationTab>? Tabs
        {
            get { return _Tabs; }
            set
            {
                _Tabs = value;
                RaisePropertiesChanged(nameof(Tabs));
            }
        }

        private bool _ShowUserInRecords = new();

        public bool ShowUserInRecords
        {
            get { return _ShowUserInRecords; }
            set
            {
                _ShowUserInRecords = value;
                RaisePropertyChanged(nameof(ShowUserInRecords));
            }
        }

        #endregion Properties

        #region Commands

        public DelegateCommand<string> ChangeThemeCommand => new DelegateCommand<string>(ExecuteChangeTheme);
        public DelegateCommand NewTabCommand => new DelegateCommand(ExecuteNewTab);

        public DelegateCommand SaveSettingsCommand => new DelegateCommand(ExecuteSaveSettings);

        private void ExecuteChangeTheme(string name)
        {
            switch (name)
            {
                case "MetropolisLight":
                    SetMetropolisLightTheme();
                    break;

                case "MetropolisDark":
                    SetMetropolisDarkTheme();
                    break;

                case "Win11":
                    SetWin11Theme();
                    break;

                default:
                    break;
            }
        }

        #endregion Commands

        #region Functions

        private void SetMetropolisLightTheme()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisLightName;
        }

        private void SetMetropolisDarkTheme()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisDarkName;
        }

        private void SetWin11Theme()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win11LightName;
        }

        private void CheckingSettings()
        {
            ShowUserInRecords = Settings.Default.ShowUserInRecords;
        }

        private void ExecuteSaveSettings()
        {
            Settings.Default.ShowUserInRecords = ShowUserInRecords;
            Settings.Default.Save();
        }

        #endregion Functions

        #region Command Functions

        private void ExecuteNewTab()
        {
            NavigationTab Home = new NavigationTab()
            {
                Header = "Main",
                Content = "",
                parent = this
            };
            Tabs.Add(Home);
            SelectedTab = Home;
        }

        private void ExecuteLogout(ThemedWindow window)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            if (window is not null)
            {
                window.Close();
            }
        }

        #endregion Command Functions
    }
}