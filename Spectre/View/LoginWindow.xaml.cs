namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : ThemedWindow
    {
        public LoginWindow()
        {
            DataContext = new LoginViewModel();
            InitializeComponent();
        }
    }
}