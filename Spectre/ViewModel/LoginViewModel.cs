namespace Spectre.ViewModel
{
    public class LoginViewModel : BindableBase
    {
        private string newConnectionStng;

        public LoginViewModel()
        {
            

            string dataBasePath = ConnectionHelper.Connection();
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder(dataBasePath);
            if (!File.Exists(builder.DataSource))
            {
                HideLogin();
            }
            else
            {
                if (!CheckDatabaseIsValid(dataBasePath))
                {
                    HideLogin();
                }
                else
                {
                    ShowLogin();
                }
            }
        }

        private readonly string Commands =

        "CREATE TABLE tblExpanse (ExpanseID	INTEGER NOT NULL UNIQUE, Date TEXT, Description	TEXT DEFAULT 'default', Amount NUMERIC DEFAULT 0, AddedBy TEXT, PRIMARY KEY(ExpanseID AUTOINCREMENT));" +

        "CREATE TABLE tblPurchase (PurchaseID INTEGER NOT NULL UNIQUE, Date TEXT, Description TEXT DEFAULT 'default', Amount NUMERIC DEFAULT 0, AddedBy TEXT, PRIMARY KEY(PurchaseID AUTOINCREMENT));" +

        "CREATE TABLE tblSale (SaleID INTEGER NOT NULL UNIQUE, Date TEXT, Description TEXT DEFAULT 'default', Amount NUMERIC DEFAULT 0, AddedBy TEXT, PRIMARY KEY(SaleID AUTOINCREMENT));" +

        "CREATE TABLE tblUser(UserID INTEGER NOT NULL UNIQUE, Username TEXT, Password TEXT, CanAddExpanse INTEGER DEFAULT 0, CanDeleteExpanse INTEGER DEFAULT 0, CanAddSale INTEGER DEFAULT 0," +
        " CanDeleteSale INTEGER DEFAULT 0, CanAddPurchase INTEGER DEFAULT 0, CanDeletePurchase INTEGER DEFAULT 0, CanViewReport INTEGER DEFAULT 0, ManageUsers INTEGER DEFAULT 0," +
        " PRIMARY KEY(UserID AUTOINCREMENT));";

        private readonly string TableSchemaCommand =
            "SELECT name FROM PRAGMA_TABLE_INFO('tblExpanse')" +
            "UNION ALL " +
            "SELECT name FROM PRAGMA_TABLE_INFO('tblPurchase')" +
            "UNION ALL " +
            "SELECT name FROM PRAGMA_TABLE_INFO('tblSale')" +
            "UNION ALL " +
            "SELECT name FROM PRAGMA_TABLE_INFO('tblUser')";

        private List<string> DatabaseTableSchema = new List<string>
            {
                 "ExpanseID",
                 "Date",
                 "Description",
                 "Amount",
                 "AddedBy",
                 "PurchaseID",
                 "Date",
                 "Description",
                 "Amount",
                 "AddedBy",
                 "SaleID",
                 "Date",
                 "Description",
                 "Amount",
                 "AddedBy",
                 "UserID",
                 "Username",
                 "Password",
                 "CanAddExpanse",
                 "CanDeleteExpanse",
                 "CanAddSale",
                 "CanDeleteSale",
                 "CanAddPurchase",
                 "CanDeletePurchase",
                 "CanViewReport",
                 "ManageUsers",
            };

        public DelegateCommand<ThemedWindow> LoginCommand => new DevExpress.Mvvm.DelegateCommand<ThemedWindow>(ExecuteLogin);
        public DelegateCommand<ThemedWindow> CloseWindowCommand => new DevExpress.Mvvm.DelegateCommand<ThemedWindow>(ExecuteCloseWindow);
        public DelegateCommand SelectDatabaseCommand => new DelegateCommand(ExecuteSelectDatabase);
        public AsyncCommand CreateNewDataBaseCommand => new AsyncCommand(ExecuteCreateNewDatabase);

        private bool _DatabaseFound;

        public bool DatabaseFound
        {
            get
            {
                return _DatabaseFound;
            }
            set
            {
                _DatabaseFound = value;
                RaisePropertyChanged(nameof(DatabaseFound));
            }
        }

        private string _Username = Settings.Default.LastUsedUsername;

        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        private string _Password;

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private UserModel _NewUser = new();

        public UserModel NewUser
        {
            get
            {
                return _NewUser;
            }
            set
            {
                _NewUser = value;
                RaisePropertyChanged(nameof(NewUser));
            }
        }

        private void ExecuteCloseWindow(ThemedWindow obj)
        {
            if (obj is not null)
            {
                obj.Close();
                Application.Current.Shutdown();
            }
        }

        private bool CheckDatabaseIsValid(string connectionString)
        {
            try
            {
                List<string> TablesHeader = new List<string>();
                string dataBasePath = connectionString;
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder(dataBasePath);
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(TableSchemaCommand, con))
                    {
                        using (SQLiteDataReader drr = cmd.ExecuteReader())
                        {
                            while (drr.Read())
                            {
                                TablesHeader.Add(drr.GetString(0));
                            }
                        }
                    }
                }
                return Enumerable.SequenceEqual(TablesHeader, DatabaseTableSchema);
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return false;
            }
        }

        private bool CreateTables(string connectionString)
        {
            try
            {
                string dataBasePath = connectionString;
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder(dataBasePath);
                using (IDbConnection db = new SQLiteConnection(connectionString, true))
                {
                    db.Open();
                    using (var transaction = db.BeginTransaction())
                    {
                        db.Execute(Commands);
                        transaction.Commit();
                        db.Close();
                        return transaction.ReturnSuccess();
                    }
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
                return false;
            }
        }

        

        private async Task ExecuteCreateNewDatabase()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            try
            {
                saveFileDialog.Title = "Save Database File";
                saveFileDialog.Filter = "Database files (*.db, *.sqlite, *.sqlite3) | *.db; *.sqlite; *.sqlite3";
                if (saveFileDialog.ShowDialog() == true)
                {
                    SQLiteConnection.CreateFile(saveFileDialog.FileName);
                    newConnectionStng = string.Format(format: @"Data Source={0};Version=3;", saveFileDialog.FileName);
                    UpdateConnectionString(newConnectionStng);
                    if (CreateTables(newConnectionStng))
                    {
                        UserModel newUser = new UserModel()
                        {
                            Username = "admin",
                            Password = "12345",
                            CanAddExpanse = true,
                            CanDeleteExpanse = true,
                            CanAddPurchase = true,
                            CanDeletePurchase = true,
                            CanAddSale = true,
                            CanDeleteSale = true,
                            CanViewReport = true,
                            ManageUsers = true,
                        };
                        if (await UserService.AddNewUser(newUser))
                        {
                            ShowLogin();
                            CustomDialog.ShowInfoMessage($"Use these credentials to login\nUsername: admin \nPassword: 12345\nYou can change them later in settings.");
                            Username = "admin";
                            Password = "12345";
                        }
                        else
                        {
                            CustomDialog.ShowErrorMessage("Unexpected Error, restart application");
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
            }
        }

        private void ExecuteLogin(ThemedWindow window)
        {
            Settings.Default.LastUsedUsername = Username;
            Settings.Default.Save();
            try
            {
                var user = LoginService.LoginAsync(Username, Password);
                if (user is not null)
                {
                    CommonProperties.CurrentUser = user;
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    if (window is not null)
                    {
                        window.Close();
                    }
                }
                else
                {
                    CustomDialog.ShowInfoMessage("Username or Password Incorrect.");
                }
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
            }
        }

        private void ExecuteSelectDatabase()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Select Database File";
                dlg.Filter = "Database files (*.db, *.sqlite, *.sqlite3) | *.db; *.sqlite; *.sqlite3";
                if (dlg.ShowDialog() == true)
                {
                    if (!string.IsNullOrEmpty(dlg.FileName))
                    {
                        string newConnectionStng = string.Format(format: "Data Source={0};Version=3;", dlg.FileName);
                        if (CheckDatabaseIsValid(newConnectionStng))
                        {
                            UpdateConnectionString(newConnectionStng);
                            ShowLogin();
                        }
                        else
                        {
                            HideLogin();
                            CustomDialog.ShowInfoMessage("Some of the tables not found, bad database.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                HideLogin();
                CustomDialog.ShowErrorMessage(e.Message);
            }
        }

        private void HideLogin()
        {
            DatabaseFound = false;
        }

        private void ShowLogin()
        {
            DatabaseFound = true;
        }

        private void UpdateConnectionString(string newConnectionString)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionStringsSection section = (ConnectionStringsSection)config.GetSection("connectionStrings");
                section.ConnectionStrings["Default"].ConnectionString = newConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
                Settings.Default.Save();
                Settings.Default.Reload();
            }
            catch (Exception e)
            {
                CustomDialog.ShowErrorMessage(e.Message);
            }
        }
    }
}