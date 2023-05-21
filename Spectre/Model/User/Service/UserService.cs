namespace Spectre.Model
{
    public static class UserService
    {
        public static async Task<ObservableCollection<UserModel>?> GetUserListAsync()
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<UserModel>("SELECT * FROM tblUser");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message, "Error");

                return null;
            }
        }

        public static bool CheckIfUserExist(string username)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    string query = $"SELECT count(*) FROM tblUser WHERE Username = @username";
                    var result = db.ExecuteScalar<bool>(query, new
                    {
                        username,
                    });
                    return result;
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message, "Error");
                return false;
            }
        }

        public static async Task<bool> AddNewUser(UserModel newUser)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    db.Open();

                    string query3 = "INSERT INTO tblUser(Username, Password, CanAddExpanse, CanDeleteExpanse, CanAddSale, CanDeleteSale, CanAddPurchase, CanDeletePurchase, CanViewReport, ManageUsers) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)";
                    var result = await db.ExecuteAsync(query3, new
                    {
                        a = newUser.Username,
                        b = newUser.Password,
                        c = newUser.CanAddExpanse ? 1 : 0,
                        d = newUser.CanDeleteExpanse ? 1 : 0,
                        e = newUser.CanAddSale ? 1 : 0,
                        f = newUser.CanDeleteSale ? 1 : 0,
                        g = newUser.CanAddPurchase ? 1 : 0,
                        h = newUser.CanDeletePurchase ? 1 : 0,
                        i = newUser.CanViewReport ? 1 : 0,
                        j = newUser.ManageUsers ? 1 : 0,
                    });
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        db.Close();
                        CustomDialog.ShowErrorMessage("Something went wrong, try again.", "Error");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message, "Error");
                return false;
            }
        }

        public static async Task<bool> UpdateUser(UserModel updateUser)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    db.Open();

                    string query =
                 "UPDATE tblUser SET Username = @a, Password = @b, CanAddExpanse = @c, CanDeleteExpanse = @d, CanAddSale = @e, CanDeleteSale = @f, CanAddPurchase = @g, " +
                 "CanDeletePurchase = @h, CanViewReport = @i, ManageUsers = @j WHERE UserID = @id";
                    var result = await db.ExecuteAsync(query, new
                    {
                        a = updateUser.Username,
                        b = updateUser.Password,
                        c = updateUser.CanAddExpanse ? 1 : 0,
                        d = updateUser.CanDeleteExpanse ? 1 : 0,
                        e = updateUser.CanAddSale ? 1 : 0,
                        f = updateUser.CanDeleteSale ? 1 : 0,
                        g = updateUser.CanAddPurchase ? 1 : 0,
                        h = updateUser.CanDeletePurchase ? 1 : 0,
                        i = updateUser.CanViewReport ? 1 : 0,
                        j = updateUser.ManageUsers ? 1 : 0,
                        id = updateUser.UserID
                    });
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        db.Close();
                        CustomDialog.ShowErrorMessage("Something went wrong, try again.", "Error");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message, "Error");
                return false;
            }
        }

        public static async Task<bool> DeleteUserAsync(UserModel ObjDeleteUser)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    string query = "DELETE FROM tblUser WHERE UserID = @a";

                    var result = await db.ExecuteAsync(query, new
                    {
                        a = ObjDeleteUser.UserID
                    });
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        CustomDialog.ShowErrorMessage("Something went wrong, try again.", "Error");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return false;
            }
        }
    }
}