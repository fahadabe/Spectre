namespace Spectre.Model;

public class NavigationTab : ViewModelBase
{
    #region Commands

    public DelegateCommand<string> NavigateCommand => new DelegateCommand<string>(Executenavigate);

    #endregion Commands

    #region Properties

    private object _Content;

    private string _UniversalSearchTerm = string.Empty;
    private bool _IsLoading = false;

    public bool IsLoading
    {
        get { return _IsLoading; }
        set { _IsLoading = value; RaisePropertyChanged(nameof(IsLoading)); }
    }

    public object Content
    {
        get { return _Content; }
        set
        {
            _Content = value;
            RaisePropertyChanged(nameof(Content));
        }
    }

    private string _Header;

    public string Header
    {
        get { return _Header; }
        set
        {
            _Header = value;
            RaisePropertyChanged(nameof(Header));
        }
    }

    public MainViewModel parent { set; get; }

    #endregion Properties

    #region Command Functions

    private void Executenavigate(string str)
    {
        switch (str)
        {
            case "Dashboard":
                OpenDashboard();
                break;

            case "Sale":
                OpenNewSale();
                break;

            case "Purchase":
                OpenNewPurchase();
                break;

            case "Expanse":
                OpenExpanse();
                break;

            case "Reports":
                OpenReports();
                break;

            case "Home":
                OpenHome();
                break;

            case "User":
                OpenUsermanagement();
                break;

            default:
                break;
        }
    }

    #endregion Command Functions

    #region Private Functions

    private void NavigateToAsync(string header, object vm)
    {
        foreach (var item in parent.Tabs)
        {
            if (item.Header == header)
            {
                parent.SelectedTab = item;
                return;
            }
        }

        NavigationTab tab = new()
        {
            Header = header,
            Content = vm
        };

        parent.Tabs.Remove(parent.SelectedTab);
        parent.Tabs.Add(tab);
        parent.SelectedTab = tab;
    }

    private void OpenDashboard()
    {
        NavigateToAsync("Dashboard", new DashboardViewModel());
    }

    private void OpenNewPurchase()
    {
        NavigateToAsync("Purchase", new PurchaseViewModel());
    }

    private void OpenNewSale()
    {
        NavigateToAsync("Sale", new SaleViewModel());
    }

    private void OpenExpanse()
    {
        NavigateToAsync("Expanse", new ExpanseViewModel());
    }

    private void OpenReports()
    {
        NavigateToAsync("Reports", new ReportViewModel());
    }

    private void OpenHome()
    {
        NavigateToAsync("Home", new HomeViewModel());
    }

    private void OpenUsermanagement()
    {
        NavigateToAsync("Users", new UserViewModel());
    }

    #endregion Private Functions
}