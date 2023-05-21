namespace Spectre.Model;

public static class ExpanseService
{
    public static async Task<bool> AddExpanseAsync(ExpanseModel objExpanse)
    {
        try
        {
            using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
            {
                string query = $"INSERT INTO tblExpanse(Date, Description, Amount, AddedBy) VALUES (@a, @b, @c, @d)";
                var result = await db.ExecuteAsync(query, new
                {
                    a = objExpanse.Date.ToString("yyyy-MM-dd"),
                    b = objExpanse.Description,
                    c = objExpanse.Amount,
                    d = objExpanse.AddedBy
                });
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    CustomDialog.ShowErrorMessage("Something went wrong, try again.");
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

    public static async Task<ObservableCollection<ExpanseModel>> GetAllCollectionAsync()
    {
        try
        {
            using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
            {
                var collection = await db.QueryAsync<ExpanseModel>("Select * From tblExpanse ORDER BY ExpanseID DESC");
                return collection.ToObservableCollection();
            }
        }
        catch (Exception e)
        {
            CustomDialog.ShowErrorMessage(e.Message);
            return null;
        }
    }

    public static async Task<ObservableCollection<ExpanseModel>> GetTodayExpanseAsync(string addedBy = "")
    {
        try
        {
            using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
            {
                var collection = await db.QueryAsync<ExpanseModel>("Select * From tblExpanse WHERE Date = DATE('now', 'localtime') ORDER BY ExpanseID DESC");
                return collection.ToObservableCollection();
            }
        }
        catch (Exception e)
        {
            CustomDialog.ShowErrorMessage(e.Message);
            return null;
        }
    }

    public static async Task<ObservableCollection<ExpanseModel>> GetYesterdayExpanseAsync(string addedBy = "")
    {
        try
        {
            using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
            {
                var collection = await db.QueryAsync<ExpanseModel>("Select * From tblExpanse WHERE Date = DATE('now', '-1 days', 'localtime') ORDER BY ExpanseID DESC");
                return collection.ToObservableCollection();
            }
        }
        catch (Exception e)
        {
            CustomDialog.ShowErrorMessage(e.Message);
            return null;
        }
    }

    public static async Task<ObservableCollection<ExpanseModel>>? GetExpanseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
            {
                var collection = await db.QueryAsync<ExpanseModel>($"SELECT * FROM tblExpanse WHERE DATE(Date) BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}' ORDER BY ExpanseID DESC");
                return collection.ToObservableCollection();
            }
        }
        catch (Exception e)
        {
            CustomDialog.ShowErrorMessage(e.Message);
            return null;
        }
    }

    public static async Task<bool> DeleteExpanse(ExpanseModel selectedExpanse)
    {
        try
        {
            using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
            {
                string query = $"DELETE FROM tblExpanse WHERE ExpanseID = @a";
                var result = await db.ExecuteAsync(query, new
                {
                    a = selectedExpanse.ExpanseID
                });
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    CustomDialog.ShowErrorMessage("Something went wrong, try again.");
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