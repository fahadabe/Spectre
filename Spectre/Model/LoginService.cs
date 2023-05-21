namespace Spectre.Model
{
    public class LoginService
    {
        public static UserModel LoginAsync(string username, string password)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    string query = $"SELECT * from tblUser WHERE Username = @username AND Password = @password";
                    var user = db.QueryFirstOrDefault<UserModel>(query, new
                    {
                        username = username,
                        password = password
                    });
                    return user;
                }
            }
            catch (System.Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message, "Error");
                return null;
            }
        }
    }
}