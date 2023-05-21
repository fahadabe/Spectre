namespace Spectre.DatabaseHelper
{
    public static class ConnectionHelper
    {
        public static string Connection()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
    }
}