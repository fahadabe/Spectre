namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            DataContext = new HomeViewModel();
            InitializeComponent();
        }
    }
}