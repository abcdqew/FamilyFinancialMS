using AduSkin.Controls.Data;
using AduSkin.Controls.Metro;
using FamilyFinancialMS.Common;
using FamilyFinancialMS.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyFinancialMS.ViewModel
{
   public class FamilyViewModel: ViewModelBase
    {
        public FamilyRepository server = new FamilyRepository();
        public  FamilyViewModel()
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
        private string queryText = string.Empty;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryText
        {
            get { return queryText; }
            set { queryText = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Family> familysList = new ObservableCollection<Family>();
        /// <summary>
        /// 用户列表
        /// </summary>
        public ObservableCollection<Family> FamilysList
        {
            get { return familysList; }
            set { familysList = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Family> totalFamilysList = new ObservableCollection<Family>();
        /// <summary>
        /// 总用户列表
        /// </summary>
        public ObservableCollection<Family> TotalFamilysList
        {
            get { return totalFamilysList; }
            set { totalFamilysList = value; RaisePropertyChanged(); }
        }
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        private string sex = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set { sex = value; RaisePropertyChanged(); }
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
        private DateTime birthday=DateTime.Today;
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; RaisePropertyChanged(); }
        }
        private bool iswork = false;
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Iswork
        {
            get { return iswork; }
            set { iswork = value; RaisePropertyChanged(); }
        }
        private Family selectList = null;
        public Family SelectList
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
            FamilysList = new ObservableCollection<Family>(TotalFamilysList.Skip((info.Info - 1) * 10).Take(10).ToList());
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
                Name = SelectList.Name;
                Sex = SelectList.Sex;
                Tel = SelectList.Tel;
                Birthday = SelectList.Birthday;
                Iswork = SelectList.Iswork;
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
                    var isdel = server.DelFamily(SelectList);
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
        public void Save()
        {
            Family family = new Family();
            family.Name = Name;
            family.Sex = Sex;
            family.Tel = Tel;
            family.Birthday = Birthday;
            family.Iswork = Iswork;
            family.Gcflag = false;
            family.Id = Id;
            if (Mode == "Add")
            {
                family.CreateTime = DateTime.Now;
                family.CreateUser = LoginViewModel.LoginUser.Account;
                family.UpdateTime = DateTime.Now;
                family.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.AddFamily(family);
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
                family.CreateTime = CreateTime;
                family.CreateUser = CreateUser;
                family.UpdateTime = DateTime.Now;
                family.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.EditFamily(family);
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
        private void ResetQuery()
        {
            QueryText = string.Empty;
            Name = string.Empty;
            Sex = string.Empty;
            Tel = string.Empty;
            Birthday = DateTime.Today;
            Iswork = false;
            GetPageData();
        }

        private void GetPageData()
        {
            FamilysList.Clear();
            TotalFamilysList.Clear();
            Family family = new Family();
            family.Name = QueryText;
            var list = server.QueryFamily(family);
            list.ForEach(it =>
            {
                TotalFamilysList.Add(it);
            });
            int i = 0;
            foreach (var item in TotalFamilysList)
            {
                i++;
                if (i <= 10)
                {
                    FamilysList.Add(item);
                }
                else
                    break;
            }
        }
        #endregion
    }
}
