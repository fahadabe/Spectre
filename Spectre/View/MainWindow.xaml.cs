namespace Spectre
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void optionsToggle_Click(object sender, RoutedEventArgs e)
        {
            optionsFlyout.IsOpen = true;
        }
    }
}