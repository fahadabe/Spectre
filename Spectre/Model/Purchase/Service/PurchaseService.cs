namespace Spectre.Model
{
    public static class PurchaseService
    {
        public static async Task<bool> AddPurchaseAsync(PurchaseModel objPurchase)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    string query = $"INSERT INTO tblPurchase(Date, Description, Amount, AddedBy) VALUES (@a, @b, @c, @d)";
                    var result = await db.ExecuteAsync(query, new
                    {
                        a = objPurchase.Date.ToString("yyyy-MM-dd"),
                        b = objPurchase.Description,
                        c = objPurchase.Amount,
                        d = objPurchase.AddedBy
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

        public static async Task<ObservableCollection<PurchaseModel>?> GetAllCollectionAsync()
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<PurchaseModel>("Select * From tblPurchase ORDER BY PurchaseID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<ObservableCollection<PurchaseModel>?> GetTodayPurchaseAsync()
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<PurchaseModel>("Select * From tblPurchase WHERE Date = DATE('now', 'localtime') ORDER BY PurchaseID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<ObservableCollection<PurchaseModel>?> GetYesterdayPurchaseAsync()
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<PurchaseModel>("Select * From tblPurchase WHERE Date = DATE('now', '-1 days', 'localtime') ORDER BY PurchaseID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<ObservableCollection<PurchaseModel>?> GetPurchaseBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<PurchaseModel>($"SELECT * FROM tblPurchase WHERE DATE(Date) BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}' ORDER BY PurchaseID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<bool> DeletePurchase(PurchaseModel selectedPurchase)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    string query = $"DELETE FROM tblPurchase WHERE PurchaseID = @a";
                    var result = await db.ExecuteAsync(query, new
                    {
                        a = selectedPurchase.PurchaseID
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
}