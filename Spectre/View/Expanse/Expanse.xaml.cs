namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for Expanse.xaml
    /// </summary>
    public partial class Expanse : UserControl
    {
        public Expanse()
        {
            DataContext = new ExpanseViewModel();
            InitializeComponent();
        }
    }
}