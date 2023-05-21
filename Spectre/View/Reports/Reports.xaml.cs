namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        public Reports()
        {
            DataContext = new ReportViewModel();
            InitializeComponent();
        }
    }
}