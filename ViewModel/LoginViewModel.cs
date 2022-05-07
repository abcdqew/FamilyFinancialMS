using AduSkin.Controls.Metro;
using FamilyFinancialMS.Common;
using FamilyFinancialMS.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyFinancialMS.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        public static User loginUser = null;
        public static User LoginUser
        {
            get;set;
        }
        UserRepository server = new UserRepository();
        public LoginView LoginView;
        public LoginViewModel()
        {
           
        }
        #region 字段定义
        private string account = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; }
        }
        private string password = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value;  }
        }

        private bool toClose = false;
        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        public bool ToClose
        {
            get
            {
                return toClose;
            }
            set
            {
                toClose = value;
                if (toClose)
                {
                    this.RaisePropertyChanged("ToClose");
                }
            }
        }
        #endregion

            #region 执行事件
        private RelayCommand signCommand;

        public RelayCommand SignCommand
        {
            get
            {
                if (signCommand == null)
                {
                    signCommand = new RelayCommand(() => Login());
                }
                return signCommand;
            }
        }

        private RelayCommand resetCommand;

        public RelayCommand ResetCommand
        {
            get
            {
                if (resetCommand == null)
                {
                    resetCommand = new RelayCommand(() => Reset());
                }
                return resetCommand;
            }
        }

        public void Reset()
        {
            Account = string.Empty;
            Password = string.Empty;
        }

        public  void Login()
        {
            try
            {
                var list = server.GetUser(Account) ;
                if(list.Count>0)
                {
                    
                    if (list.Exists(l => l.Password == Password))
                    {
                        var user = list.FirstOrDefault();
                        if(user.IsLocked)
                        {
                            AduMessageBox.Show(Application.Current.FindResource("AccountLocked").ToString());
                            return;
                        }
                        user.LoginCounter += 1;
                        user.LastLoginTime = DateTime.Now;
                        server.AddLoginCounter(user);
                        LoginUser = user;
                        WindowManager.Show("MainWindow", null);
                        ToClose = true;
                        //new MainWindow().Show();
                        //LoginView.Close();
                        
                    }
                    else
                    {
                        AduMessageBox.Show(Application.Current.FindResource("PassWrodFill").ToString());
                    }
                }
                else
                {
                    AduMessageBox.Show(Application.Current.FindResource("AccountFill").ToString());
                }
            }
            catch (Exception)
            {

                return;
            }
        }
        #endregion

        #region 数据库
        public void GetPageData()
        {
           
        }
        #endregion
    }
}
