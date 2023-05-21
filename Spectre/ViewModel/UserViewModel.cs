namespace Spectre.ViewModel
{
    public class UserViewModel : BindableBase
    {
        public UserViewModel()
        {
            CurrentUser = CommonProperties.CurrentUser;
        }

        #region Properties

        private UserModel _CurrentUser = new();

        public UserModel CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }

        private UserModel _NewUser = new();

        public UserModel NewUser
        {
            get { return _NewUser; }
            set
            {
                _NewUser = value;
                RaisePropertyChanged(nameof(NewUser));
            }
        }

        private UserModel _SelectedUser = new();

        public UserModel SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                _SelectedUser = value;
                RaisePropertyChanged(nameof(SelectedUser));
            }
        }

        private ObservableCollection<UserModel> _UserList = new();

        public ObservableCollection<UserModel> UserList
        {
            get { return _UserList; }
            set
            {
                _UserList = value;
                RaisePropertyChanged(nameof(UserList));
            }
        }

        private bool _IsAllChecked;

        public bool IsAllChecked
        {
            get { return _IsAllChecked; }
            set
            {
                _IsAllChecked = value;
                RaisePropertyChanged(nameof(IsAllChecked));
                CheckOrUncheckAll();
            }
        }

        #endregion Properties

        #region Commands

        public AsyncCommand GetDataCommand => new AsyncCommand(ExecuteGetData, CanManageUser);

        public AsyncCommand AddUserCommand => new AsyncCommand(ExecuteAdduser, CanManageUser);

        public AsyncCommand DeleteUserCommand => new AsyncCommand(ExecuteDeleteUser, CanManageUser);

        public DelegateCommand UpdateUserWindowCommand => new DelegateCommand(ExecuteUpdateUserWindow, CanManageUser);

        public AsyncCommand UpdateUserCommand => new AsyncCommand(ExecuteUpdateUser, CanManageUser);

        #endregion Commands

        #region Command Functions

        private async Task ExecuteGetData()
        {
            await GetUsers();
        }

        private async Task ExecuteAdduser()
        {
            if (NewUser is not null)
            {
                if (!string.IsNullOrWhiteSpace(NewUser.Username) && !string.IsNullOrWhiteSpace(NewUser.Password))
                {
                    if (await UserService.AddNewUser(NewUser))
                    {
                        NewUser = null;
                        NewUser = new();
                        IsAllChecked = false;
                        await GetUsers();
                        CheckOrUncheckAll();
                    }
                }
                else
                {
                    CustomDialog.ShowInfoMessage("Usename & Password cannot be empty", "Information");
                }
            }
        }

        private async Task ExecuteDeleteUser()
        {
            if (SelectedUser is not null)
            {
                if (UserList.Count == 1)
                {
                    CustomDialog.ShowInfoMessage("Cannot delete last user.", "Information");
                }
                else
                {
                    if (!string.Equals(SelectedUser.Username, CommonProperties.CurrentUser.Username))
                    {
                        MessageBoxResult dialogResult = ThemedMessageBox.Show($"Are you sure to delete '{SelectedUser.Username}'?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (dialogResult == MessageBoxResult.Yes)
                        {
                            if (await UserService.DeleteUserAsync(SelectedUser))
                            {
                                await GetUsers();
                            }
                        }
                    }
                    else
                    {
                        CustomDialog.ShowInfoMessage("Cannot delete yourself", "Informaion");
                    }
                }
            }
        }

        private void ExecuteUpdateUserWindow()
        {
            if (SelectedUser is not null)
            {
                UpdateUser updateUser = new UpdateUser
                {
                    DataContext = this
                };
                updateUser.ShowDialog();
            }
        }

        private async Task ExecuteUpdateUser()
        {
            if (SelectedUser is not null)
            {
                if (await UserService.UpdateUser(SelectedUser))
                {
                    await GetUsers();
                }
            }
        }

        private bool CanManageUser()
        {
            return CurrentUser.ManageUsers;
        }

        #endregion Command Functions

        #region Functions

        private async Task GetUsers()
        {
            UserList = await UserService.GetUserListAsync();
        }

        private void CheckOrUncheckAll()
        {
            if (NewUser is not null)
            {
                if (IsAllChecked)
                {
                    NewUser.CanAddExpanse = true;
                    NewUser.CanDeleteExpanse = true;
                    NewUser.CanAddPurchase = true;
                    NewUser.CanDeletePurchase = true;
                    NewUser.CanAddSale = true;
                    NewUser.CanDeleteSale = true;
                    NewUser.CanViewReport = true;
                    NewUser.ManageUsers = true;
                }
                else
                {
                    NewUser.CanAddExpanse = false;
                    NewUser.CanDeleteExpanse = false;
                    NewUser.CanAddPurchase = false;
                    NewUser.CanDeletePurchase = false;
                    NewUser.CanAddSale = false;
                    NewUser.CanDeleteSale = false;
                    NewUser.CanViewReport = false;
                    NewUser.ManageUsers = false;
                }
            }
        }

        #endregion Functions
    }
}