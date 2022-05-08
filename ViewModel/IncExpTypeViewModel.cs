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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace FamilyFinancialMS.ViewModel
{
   public class IncExpTypeViewModel : ViewModelBase
    {
        public IncExpTypeViewModel()
        {
            if (LoginViewModel.LoginUser.FlagAdmin)
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
        public IncExpTypeRepository server = new IncExpTypeRepository();
        private string queryText = string.Empty;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryText
        {
            get { return queryText; }
            set { queryText = value; RaisePropertyChanged(); }
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
        private string code = string.Empty;
        /// <summary>
        /// 类型Code（雪花算法生成
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(); }
        }
        private string name = string.Empty;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        private string type = string.Empty;
        /// <summary>
        /// 收支类型
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<IncExpType> incExpTypeList = new ObservableCollection<IncExpType>();
        /// <summary>
        /// 收入列表
        /// </summary>TotalUserList
        public ObservableCollection<IncExpType> IncExpTypeList
        {
            get { return incExpTypeList; }
            set { incExpTypeList = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<IncExpType> totalIncExpTypeList = new ObservableCollection<IncExpType>();
        /// <summary>
        /// 总收入列表
        /// </summary>
        public ObservableCollection<IncExpType> TotalIncExpTypeList
        {
            get { return totalIncExpTypeList; }
            set { totalIncExpTypeList = value; RaisePropertyChanged(); }
        }
        private IncExpType selectList = null;
        public IncExpType SelectList
        {
            get { return selectList; }
            set { selectList = value; RaisePropertyChanged(); }
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
            IncExpTypeList = new ObservableCollection<IncExpType>(TotalIncExpTypeList.Skip((info.Info - 1) * 10).Take(10).ToList());
        }
        public void Save()
        {
            IncExpType incExpTypeList = new IncExpType();
            incExpTypeList.Code = Helper.nextId().ToString();
            incExpTypeList.Name = Name;
            incExpTypeList.Type = Type;
            incExpTypeList.Gcflag = false;
            incExpTypeList.Id = Id;
            if (string.IsNullOrWhiteSpace(incExpTypeList.Name)  || string.IsNullOrWhiteSpace(incExpTypeList.Type))
            {
                AduMessageBox.Show(Application.Current.FindResource("RequiredNotSpace").ToString());
                return;
            }
            if (Mode == "Add")
            {
                incExpTypeList.CreateTime = DateTime.Now;
                incExpTypeList.CreateUser = LoginViewModel.LoginUser.Account;
                incExpTypeList.UpdateTime = DateTime.Now;
                incExpTypeList.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.AddIncExpType(incExpTypeList);
                if (isAdd > 0)
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
            else if (Mode == "Edit")
            {
                incExpTypeList.CreateTime = CreateTime;
                incExpTypeList.CreateUser = CreateUser;
                incExpTypeList.UpdateTime = DateTime.Now;
                incExpTypeList.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.EditIncExpType(incExpTypeList);
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
                Code = SelectList.Code;
                Name = SelectList.Name;
                Type = SelectList.Type;
            }
        }

        public void Del()
        {
            if (SelectList != null)
            {
                var isok = AduMessageBox.Show(Application.Current.FindResource("IsDel").ToString(), Application.Current.FindResource("Del").ToString(), MessageBoxButton.OKCancel).ToString();
                if (isok == "OK")
                {
                    SelectList.Gcflag = true;
                    var isdel = server.DelIncExpType(SelectList);
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
            Code = string.Empty;
            Name = string.Empty;
            Type = string.Empty;
            GetPageData();
        }
        public void GetPageData()
        {
            IncExpTypeList.Clear();
            TotalIncExpTypeList.Clear();
            Query query = new Query();
            query.QueryText = QueryText;
            var list = server.QueryIncExpType(query);
            list.ForEach(it =>
            {
                TotalIncExpTypeList.Add(it);
            });
            int i = 0;
            foreach (var item in TotalIncExpTypeList)
            {
                i++;
                if (i <= 10)
                {
                    IncExpTypeList.Add(item);
                }
                else
                    break;
            }
        }
        #endregion
    }
}
