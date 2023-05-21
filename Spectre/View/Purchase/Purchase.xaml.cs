namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for Purchase.xaml
    /// </summary>
    public partial class Purchase : UserControl
    {
        public Purchase()
        {
            DataContext = new PurchaseViewModel();
            InitializeComponent();
        }
    }
}