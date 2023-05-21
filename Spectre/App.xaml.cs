using System.Threading;

namespace Spectre
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationThemeHelper.UpdateApplicationThemeName();
            const string appName = "Spectre";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                CustomDialog.ShowInfoMessage("An instance is already running.");
                Current.Shutdown();
            }
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            ApplicationThemeHelper.SaveApplicationThemeName();
        }
    }
}