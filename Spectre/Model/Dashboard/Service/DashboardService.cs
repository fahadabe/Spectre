namespace Spectre.Model
{
    public static class DashboardService
    {
        public static TodayPerformance GetTodayPerformance()
        {
            try
            {
                TodayPerformance todayPerformance = new();
                double sale = 0;
                double purchase = 0;
                double expanse = 0;

                string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date = DATE('now', 'localtime')";
                string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date = DATE('now', 'localtime')";
                string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date = DATE('now', 'localtime')";

                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    sale = db.ExecuteScalar<double>(saleQuery);
                    purchase = db.ExecuteScalar<double>(purchaseQuery);
                    expanse = db.ExecuteScalar<double>(expanseQuery);
                }

                todayPerformance.Sale = sale;
                todayPerformance.Purchase = purchase;
                todayPerformance.Expanse = expanse;

                return todayPerformance;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return new TodayPerformance();
            }
        }

        public static YesterdayPerformance GetYesterdayPerformance()
        {
            try
            {
                YesterdayPerformance yesterdayPerformance = new();
                double sale = 0;
                double purchase = 0;
                double expanse = 0;

                string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date = DATE('now', '-1 days', 'localtime')";
                string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date = DATE('now', '-1 days', 'localtime')";
                string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date = DATE('now', '-1 days', 'localtime')";

                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    sale = db.ExecuteScalar<double>(saleQuery);
                    purchase = db.ExecuteScalar<double>(purchaseQuery);
                    expanse = db.ExecuteScalar<double>(expanseQuery);
                }

                yesterdayPerformance.Sale = sale;
                yesterdayPerformance.Purchase = purchase;
                yesterdayPerformance.Expanse = expanse;

                return yesterdayPerformance;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return new YesterdayPerformance();
            }
        }

        public static ThisWeekPerformance GetThisWeekPerformance()
        {
            try
            {
                ThisWeekPerformance thisWeekPerformance = new();
                double sale = 0;
                double purchase = 0;
                double expanse = 0;

                string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE strftime('%W/%Y', Date) == strftime('%W/%Y', 'now')";
                string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE strftime('%W/%Y', Date) == strftime('%W/%Y', 'now')";
                string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE strftime('%W/%Y', Date) == strftime('%W/%Y', 'now')";

                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    sale = db.ExecuteScalar<double>(saleQuery);
                    purchase = db.ExecuteScalar<double>(purchaseQuery);
                    expanse = db.ExecuteScalar<double>(expanseQuery);
                }

                thisWeekPerformance.Sale = sale;
                thisWeekPerformance.Purchase = purchase;
                thisWeekPerformance.Expanse = expanse;

                return thisWeekPerformance;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return new ThisWeekPerformance();
            }
        }

        public static ThisMonthPerformance GetThisMonhPerformance()
        {
            try
            {
                ThisMonthPerformance thisMonthPerformance = new();
                double sale = 0;
                double purchase = 0;
                double expanse = 0;

                string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date BETWEEN DATE('now', 'start of month') AND DATE('now', 'localtime')";
                string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date BETWEEN DATE('now', 'start of month') AND DATE('now', 'localtime')";
                string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date BETWEEN DATE('now', 'start of month') AND DATE('now', 'localtime')";

                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    sale = db.ExecuteScalar<double>(saleQuery);
                    purchase = db.ExecuteScalar<double>(purchaseQuery);
                    expanse = db.ExecuteScalar<double>(expanseQuery);
                }

                thisMonthPerformance.Sale = sale;
                thisMonthPerformance.Purchase = purchase;
                thisMonthPerformance.Expanse = expanse;

                return thisMonthPerformance;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return new ThisMonthPerformance();
            }
        }

        public static YTDPerformance GetYTDPerformance()
        {
            try
            {
                YTDPerformance ytdPerformance = new();
                double sale = 0;
                double purchase = 0;
                double expanse = 0;

                string saleQuery = $"SELECT SUM(Amount) FROM tblSale WHERE Date BETWEEN DATE('now', 'start of year') AND DATE('now', 'localtime')";
                string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase WHERE Date BETWEEN DATE('now', 'start of year') AND DATE('now', 'localtime')";
                string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse WHERE Date BETWEEN DATE('now', 'start of year') AND DATE('now', 'localtime')";

                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    sale = db.ExecuteScalar<double>(saleQuery);
                    purchase = db.ExecuteScalar<double>(purchaseQuery);
                    expanse = db.ExecuteScalar<double>(expanseQuery);
                }

                ytdPerformance.Sale = sale;
                ytdPerformance.Purchase = purchase;
                ytdPerformance.Expanse = expanse;

                return ytdPerformance;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return new YTDPerformance();
            }
        }

        public static AllPerformance GetAllPerformance()
        {
            try
            {
                AllPerformance allPerformance = new();
                double sale = 0;
                double purchase = 0;
                double expanse = 0;

                string saleQuery = $"SELECT SUM(Amount) FROM tblSale";
                string purchaseQuery = $"SELECT SUM(Amount) FROM tblPurchase";
                string expanseQuery = $"SELECT SUM(Amount) FROM tblExpanse";

                using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    sale = db.ExecuteScalar<double>(saleQuery);
                    purchase = db.ExecuteScalar<double>(purchaseQuery);
                    expanse = db.ExecuteScalar<double>(expanseQuery);
                }

                allPerformance.Sale = sale;
                allPerformance.Purchase = purchase;
                allPerformance.Expanse = expanse;

                return allPerformance;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return new AllPerformance();
            }
        }

        //public static ObservableCollection<MonthlySaleModel> GetSaleChartData(bool onlyCurrentYear)
        //{
        //    ObservableCollection<MonthlySaleModel> allSales = new();
        //    IEnumerable<MonthlySaleModel> collection;
        //    using (IDbConnection db = new SQLiteConnection(ConnectionHelper.Connection()))
        //    {
        //        collection = db.Query<MonthlySaleModel>($"SELECT STRFTIME('%Y-%m', Date) AS Month, sum(Amount) AS Sale FROM tblSale GROUP BY STRFTIME('%Y-%m', Date)");
        //    }

        //    foreach (var item in collection)
        //    {
        //        string[] tokens = item.Month.ToString().Split('-');
        //        string year = tokens[0];
        //        string month = tokens[1];
        //        if (onlyCurrentYear)
        //        {
        //            if (year == DateTime.Now.Year.ToString())
        //            {
        //                allSales.Add(new MonthlySaleModel
        //                {
        //                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
        //                    Sale = Convert.ToInt32(item.Sale)
        //                });
        //            }
        //        }
        //        else
        //        {
        //            allSales.Add(new MonthlySaleModel
        //            {
        //                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
        //                Sale = Convert.ToInt32(item.Sale)
        //            });
        //        }
        //    }

        //    return allSales;
        //}

        public static ObservableCollection<MonthlySaleModel>? GetSaleChartData(bool onlyCurrentYear)
        {
            try
            {
                ObservableCollection<MonthlySaleModel> monthlySales = new();
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    conn.Open();
                    using (SQLiteCommand cmmd = new SQLiteCommand($"SELECT STRFTIME('%Y-%m', Date) AS Month, sum(Amount) AS Value FROM tblSale GROUP BY STRFTIME('%Y-%m', Date)", conn))
                    {
                        using (SQLiteDataReader drr = cmmd.ExecuteReader())
                        {
                            while (drr.Read())
                            {
                                if (!string.IsNullOrWhiteSpace(drr["Month"].ToString()))
                                {
                                    string[] tokens = drr["Month"].ToString().Split('-');
                                    string year = tokens[0];
                                    string month = tokens[1];
                                    if (onlyCurrentYear)
                                    {
                                        if (year == DateTime.Now.Year.ToString())
                                        {
                                            monthlySales.Add(new MonthlySaleModel
                                            {
                                                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                                Sale = Convert.ToInt32(drr["Value"])
                                            });
                                        }
                                    }
                                    else
                                    {
                                        monthlySales.Add(new MonthlySaleModel
                                        {
                                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                            Sale = Convert.ToInt32(drr["Value"])
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                return monthlySales;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static ObservableCollection<MonthlyExpanseModel>? GetExpanseChartData(bool onlyCurrentYear)
        {
            try
            {
                ObservableCollection<MonthlyExpanseModel> monthlyExpanses = new();
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    conn.Open();
                    using (SQLiteCommand cmmd = new SQLiteCommand("SELECT STRFTIME('%Y-%m', Date) AS Month, sum(Amount) AS Value FROM tblExpanse GROUP BY STRFTIME('%Y-%m', Date)", conn))
                    {
                        using (SQLiteDataReader drr = cmmd.ExecuteReader())
                        {
                            while (drr.Read())
                            {
                                if (!string.IsNullOrWhiteSpace(drr["Month"].ToString()))
                                {
                                    string[] tokens = drr["Month"].ToString().Split('-');
                                    string year = tokens[0];
                                    string month = tokens[1];
                                    if (onlyCurrentYear)
                                    {
                                        if (year == DateTime.Now.Year.ToString())
                                        {
                                            monthlyExpanses.Add(new MonthlyExpanseModel
                                            {
                                                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                                Expanse = Convert.ToInt32(drr["Value"])
                                            });
                                        }
                                    }
                                    else
                                    {
                                        monthlyExpanses.Add(new MonthlyExpanseModel
                                        {
                                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                            Expanse = Convert.ToInt32(drr["Value"])
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                return monthlyExpanses;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }

        public static ObservableCollection<MonthlyPurchaseModel>? GetPurchaseChartData(bool onlyCurrentYear)
        {
            try
            {
                ObservableCollection<MonthlyPurchaseModel> monthlyPurchaseExpanses = new();
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionHelper.Connection()))
                {
                    conn.Open();
                    using (SQLiteCommand cmmd = new SQLiteCommand($"SELECT STRFTIME('%Y-%m', Date) AS Month, sum(Amount) AS Value FROM tblPurchase GROUP BY STRFTIME('%Y-%m', Date)", conn))
                    {
                        using (SQLiteDataReader drr = cmmd.ExecuteReader())
                        {
                            while (drr.Read())
                            {
                                if (!string.IsNullOrWhiteSpace(drr["Month"].ToString()))
                                {
                                    string[] tokens = drr["Month"].ToString().Split('-');
                                    string year = tokens[0];
                                    string month = tokens[1];
                                    if (onlyCurrentYear)
                                    {
                                        if (year == DateTime.Now.Year.ToString())
                                        {
                                            monthlyPurchaseExpanses.Add(new MonthlyPurchaseModel
                                            {
                                                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                                Purchase = Convert.ToInt32(drr["Value"])
                                            });
                                        }
                                    }
                                    else
                                    {
                                        monthlyPurchaseExpanses.Add(new MonthlyPurchaseModel
                                        {
                                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(month)) + "-" + year,
                                            Purchase = Convert.ToInt32(drr["Value"])
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                return monthlyPurchaseExpanses;
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return null;
            }
        }
    }
}