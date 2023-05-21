namespace Spectre.Model
{
    public static class SaleService
    {
        public static async Task<bool> AddSaleAsync(SaleModel objSale)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    string query = $"INSERT INTO tblSale(Date, Description, Amount, AddedBy) VALUES (@a, @b, @c, @d)";
                    var result = await db.ExecuteAsync(query, new
                    {
                        a = objSale.Date.ToString("yyyy-MM-dd"),
                        b = objSale.Description,
                        c = objSale.Amount,
                        d = objSale.AddedBy
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
                CustomDialog.ShowErrorMessage(e.Message, "Error");
                return false;
            }
        }

        public static async Task<ObservableCollection<SaleModel>?> GetAllCollectionAsync()
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<SaleModel>("Select * From tblSale ORDER BY SaleID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<ObservableCollection<SaleModel>?> GetTodaySaleAsync()
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<SaleModel>("Select * From tblSale WHERE Date = DATE('now', 'localtime') ORDER BY SaleID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<ObservableCollection<SaleModel>?> GetYesterdaySaleAsync()
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<SaleModel>("Select * From tblSale WHERE Date = DATE('now', '-1 days', 'localtime') ORDER BY SaleID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<ObservableCollection<SaleModel>?> GetSaleBetweenTwoDatesAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    var collection = await db.QueryAsync<SaleModel>($"SELECT * FROM tblSale WHERE DATE(Date) BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}' ORDER BY SaleID DESC");
                    return collection.ToObservableCollection();
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static async Task<bool> DeleteSale(SaleModel selectedSale)
        {
            try
            {
                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    string query = $"DELETE FROM tblSale WHERE SaleID = @a";
                    var result = await db.ExecuteAsync(query, new
                    {
                        a = selectedSale.SaleID
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
                CustomDialog.ShowErrorMessage(e.Message, "Error");
                return false;
            }
        }
    }
}