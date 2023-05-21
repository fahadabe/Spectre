namespace Spectre.Helper
{
    public static class CustomDialog
    {
        public static void ShowInfoMessage(string message, string title = "Info")
        {
            ThemedMessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowErrorMessage(string message, string title = "Error")
        {
            ThemedMessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}