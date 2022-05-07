using AduSkin.Controls.Data;
using AduSkin.Controls.Metro;
using FamilyFinancialMS.Common;
using FamilyFinancialMS.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Entities;
using Model.Helper;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FamilyFinancialMS.ViewModel
{
   public class UserViewModel: ViewModelBase
    {
       
        public  UserViewModel()
        {
            if(LoginViewModel.LoginUser.FlagAdmin)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
            GetPageData();
        }
        #region 定义
        private Visibility visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get { return visibility; }
            set { visibility = value; RaisePropertyChanged(); }
        }
        private int tabpageIndex;
        public int TabPageIndex
        { 
            get { return tabpageIndex; } 
            set { tabpageIndex = value; RaisePropertyChanged(); }
        }
        public string Mode = string.Empty;
        public UserRepository server = new UserRepository();
        private string queryText = string.Empty;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryText
        {
            get { return queryText; }
            set { queryText = value;RaisePropertyChanged(); }
        }
        private int id = 0;
        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(); }
        }
        private DateTime createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; RaisePropertyChanged(); }
        }
        private string createUser;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; RaisePropertyChanged(); }
        }
        private DateTime updateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; RaisePropertyChanged(); }
        }
        private string updateuser;
        /// <summary>
        /// 更新人
        /// </summary>
        public string Updateuser
        {
            get { return updateuser; }
            set { updateuser = value; RaisePropertyChanged(); }
        }
        private bool gcflag = false;
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Gcflag
        {
            get { return gcflag; }
            set { gcflag = value; RaisePropertyChanged(); }
        }
        private string account = string.Empty;
        /// <summary>
        /// 账户
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }
        private string passWord = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }
        private string userName = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }
        private string tel = string.Empty;
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            get { return tel; }
            set { tel = value; RaisePropertyChanged(); }
        }
        private string email = string.Empty;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(); }
        }
        private bool isLocked = false;
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; RaisePropertyChanged(); }
        }
        private bool flagAdmin = false;
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool FlagAdmin
        {
            get { return flagAdmin; }
            set { flagAdmin = value; RaisePropertyChanged(); }
        }
        private int loginCounter = 0;
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCounter
        {
            get { return loginCounter; }
            set { loginCounter = value; RaisePropertyChanged(); }
        }
        private DateTime? lastLoginTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? LastLoginTime
        {
            get { return lastLoginTime; }
            set { lastLoginTime = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<User> usersList = new ObservableCollection<User>();
        /// <summary>
        /// 用户列表
        /// </summary>TotalUserList
        public ObservableCollection<User> UsersList
        {
            get { return usersList; }
            set { usersList = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<User> totalUserList = new ObservableCollection<User>();
        /// <summary>
        /// 总用户列表
        /// </summary>
        public ObservableCollection<User> TotalUserList
        {
            get { return totalUserList; }
            set { totalUserList = value; RaisePropertyChanged(); }
        }
        private User selectList =null;
        public User SelectList
        {
            get { return selectList; }
            set { selectList = value;RaisePropertyChanged(); }
        }
        #endregion
        #region 执行事件
        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(() => Add());
                }
                return addCommand;
            }
        }
        private RelayCommand editCommand;

        public RelayCommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(() => Edit());
                }
                return editCommand;
            }
        }

      

        private RelayCommand delCommand;

        public RelayCommand DelCommand
        {
            get
            {
                if (delCommand == null)
                {
                    delCommand = new RelayCommand(() => Del());
                }
                return delCommand;
            }
        }


        private RelayCommand queryCommand;

        public RelayCommand QueryCommand
        {
            get
            {
                if (queryCommand == null)
                {
                    queryCommand = new RelayCommand(() => GetPageData());
                }
                return queryCommand;
            }
        }
        private RelayCommand resetCommand;

        public RelayCommand ResetCommand
        {
            get
            {
                if (resetCommand == null)
                {
                    resetCommand = new RelayCommand(() => ResetQuery());
                }
                return resetCommand;
            }
        }
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(() => Save());
                }
                return saveCommand;
            }
        }

        private RelayCommand cancelCommand;

        public RelayCommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(() => Cancel());
                }
                return cancelCommand;
            }
        }
        /// <summary>
        ///     页码改变命令
        /// </summary>
        public RelayCommand<FunctionEventArgs<int>> PageUpdatedCmd =>
            new Lazy<RelayCommand<FunctionEventArgs<int>>>(() =>
                new RelayCommand<FunctionEventArgs<int>>(PageUpdated)).Value;
        /// <summary>
        ///     页码改变
        /// </summary>
        private void PageUpdated(FunctionEventArgs<int> info)
        {
            UsersList = new ObservableCollection<User>(TotalUserList.Skip((info.Info - 1) * 10).Take(10).ToList());
        }
        public void Save()
        {
            User user = new User();
            user.Account = Account;
            user.UserName = UserName;
            user.Password = PassWord;
            user.Tel = Tel;
            user.Email = Email;
            user.IsLocked = IsLocked;
            user.FlagAdmin = FlagAdmin;
            user.Gcflag = false;
            user.Id = Id;
            user.LoginCounter = LoginCounter;
            user.LastLoginTime = DateTime.Now;
            if(string.IsNullOrWhiteSpace(user.Account)|| string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Tel))
            {
                AduMessageBox.Show(Application.Current.FindResource("RequiredNotSpace").ToString(), Application.Current.FindResource("Save").ToString());
                return;
            }
            bool accountformat = false;
            bool passwordformat = false;
            accountformat = Helper.IsNumberAndWord(user.Account);
            passwordformat = Helper.IsNumberAndWord(user.Password);
            if(!accountformat)
            {
                AduMessageBox.Show(Application.Current.FindResource("User_accountformat").ToString());
                return;
            }
            if (!passwordformat)
            {
                AduMessageBox.Show(Application.Current.FindResource("User_passwordformat").ToString());
                return;
            }
            var isIdentical=server.IdenticalAccount(user.Account);
            if(isIdentical>0)
            {
                AduMessageBox.Show(Application.Current.FindResource("User_IsIdenticalAccount").ToString());
                return;
            }
            if (Mode=="Add")
            {
                user.CreateTime = DateTime.Now;
                user.CreateUser = LoginViewModel.LoginUser.Account;
                user.UpdateTime = DateTime.Now;
                user.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.AddUser(user);
                if(isAdd>0)
                {
                    AduMessageBox.Show(Application.Current.FindResource("AddSuccess").ToString(), Application.Current.FindResource("Save").ToString());
                    TabPageIndex = 0;
                    ResetQuery();
                }
                else
                {
                    AduMessageBox.Show(Application.Current.FindResource("AddFill").ToString(), Application.Current.FindResource("Save").ToString());
                }
            }   
            else if(Mode=="Edit")
            {
                user.CreateTime = CreateTime;
                user.CreateUser = CreateUser;
                user.UpdateTime = DateTime.Now;
                user.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.EditUser(user);
                if (isAdd > 0)
                {
                    AduMessageBox.Show(Application.Current.FindResource("EditSuccess").ToString(), Application.Current.FindResource("Edit").ToString());
                    TabPageIndex = 0;
                    ResetQuery();
                }
                else
                {
                    AduMessageBox.Show(Application.Current.FindResource("EditFill").ToString(), Application.Current.FindResource("Edit").ToString());
                }
            }
            ResetQuery();
        }

      

        public void Cancel()
        {
            TabPageIndex = 0;
            ResetQuery();
        }

        public void Add()
        {
            TabPageIndex = 1;
            Mode = "Add";
        }
        public void Edit()
        {
            if (SelectList != null)
            {
                TabPageIndex = 1;
                Mode = "Edit";
                Id = SelectList.Id;
                CreateTime = SelectList.CreateTime;
                CreateUser = SelectList.CreateUser;
                UpdateTime = SelectList.UpdateTime;
                Updateuser = SelectList.UpdateUser;
                Gcflag = SelectList.Gcflag;
                Account = SelectList.Account;
                PassWord = SelectList.Password;
                UserName = SelectList.UserName;
                Tel = SelectList.Tel;
                Email = SelectList.Email;
                IsLocked = SelectList.IsLocked;
                FlagAdmin = SelectList.FlagAdmin;
                lastLoginTime = SelectList.LastLoginTime;
                LoginCounter = SelectList.LoginCounter;
            }
        }

        public void Del()
        {
            if (SelectList != null)
            {
                var isok = AduMessageBox.Show(Application.Current.FindResource("IsDel").ToString(), Application.Current.FindResource("Del").ToString(), MessageBoxButton.OKCancel).ToString();
                if(isok=="OK")
                {
                    SelectList.Gcflag = true;
                    var isdel = server.DelUser(SelectList);
                    if (isdel > 0)
                    {
                        AduMessageBox.Show(Application.Current.FindResource("DelSuccess").ToString(), Application.Current.FindResource("Del").ToString());
                        TabPageIndex = 0;
                        ResetQuery();
                    }
                    else
                    {
                        AduMessageBox.Show(Application.Current.FindResource("DelFill").ToString(), Application.Current.FindResource("Del").ToString());
                    }
                }
            }
        }
        public void ResetQuery()
        {
            QueryText = string.Empty;
            Account = string.Empty;
            PassWord = string.Empty;
            UserName = string.Empty;
            Tel = string.Empty;
            Email = string.Empty;
            IsLocked = false;
            FlagAdmin = false;
            GetPageData();
        }
        public void GetPageData()
         {

            UsersList.Clear();
            TotalUserList.Clear();
            User user = new User();
            user.Account = QueryText;
            var list = server.QueryUser(user);
            list.ForEach(it =>
            {
                TotalUserList.Add(it);
            });
            int i = 0;
            foreach(var item in TotalUserList)
            {
                i++;
                if (i <= 10)
                {
                    UsersList.Add(item);
                }
                else
                    break;
            }
        }
        #endregion
    }
}
