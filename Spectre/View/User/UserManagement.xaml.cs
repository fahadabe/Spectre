namespace Spectre.View
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : UserControl
    {
        public UserManagement()
        {
            DataContext = new UserViewModel();
            InitializeComponent();
        }
    }
}