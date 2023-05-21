namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Sale : UserControl
    {
        public Sale()
        {
            DataContext = new SaleViewModel();
            InitializeComponent();
        }
    }
}