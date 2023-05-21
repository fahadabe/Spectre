namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            DataContext = new DashboardViewModel();
            InitializeComponent();
        }
    }
}