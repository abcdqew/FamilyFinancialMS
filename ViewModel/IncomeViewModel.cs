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
   public class IncomeViewModel : ViewModelBase
    {
        public IncomeViewModel()
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
        public IncomeRepsitory server = new IncomeRepsitory();
        private string queryText = string.Empty;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryText
        {
            get { return queryText; }
            set { queryText = value; RaisePropertyChanged(); }
        }
        private DateTime startDate=DateTime.Now.Date.AddMonths(-1);
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; RaisePropertyChanged(); }
        }
        private DateTime endDate= DateTime.Now.Date;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; RaisePropertyChanged(); }
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
        private string incomeCode = string.Empty;
        /// <summary>
        /// 收入Code（雪花算法生成
        /// </summary>
        public string IncomeCode
        {
            get { return incomeCode; }
            set { incomeCode = value; RaisePropertyChanged(); }
        }
        private string incomeName = string.Empty;
        /// <summary>
        /// 收入名称
        /// </summary>
        public string IncomeName
        {
            get { return incomeName; }
            set { incomeName = value; RaisePropertyChanged(); }
        }
        private string familyName = string.Empty;
        /// <summary>
        /// 收入人姓名
        /// </summary>
        public string FamilyName
        {
            get { return familyName; }
            set { familyName = value; RaisePropertyChanged(); }
        }
        private double incomeNumber = 0;
        /// <summary>
        /// 收入金额
        /// </summary>
        public double IncomeNumber
        {
            get { return incomeNumber; }
            set { incomeNumber = value; RaisePropertyChanged(); }
        }
        private string familyTel = string.Empty;
        /// <summary>
        /// 收入人电话
        /// </summary>
        public string FamilyTel
        {
            get { return familyTel; }
            set { familyTel = value; RaisePropertyChanged(); }
        }
        private DateTime incomedate=DateTime.Today;
        /// <summary>
        /// 收入日期
        /// </summary>
        public DateTime Incomedate
        {
            get { return incomedate; }
            set { incomedate = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Income> incomeList = new ObservableCollection<Income>();
        /// <summary>
        /// 收入列表
        /// </summary>TotalUserList
        public ObservableCollection<Income> IncomeList
        {
            get { return incomeList; }
            set { incomeList = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Income> totalIncomeList = new ObservableCollection<Income>();
        /// <summary>
        /// 总收入列表
        /// </summary>
        public ObservableCollection<Income> TotalIncomeList
        {
            get { return totalIncomeList; }
            set { totalIncomeList = value; RaisePropertyChanged(); }
        }
        private Income selectList = null;
        public Income SelectList
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
        private RelayCommand exportCommand;

        public RelayCommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new RelayCommand(() => Export());
                }
                return exportCommand;
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
            IncomeList = new ObservableCollection<Income>(TotalIncomeList.Skip((info.Info - 1) * 10).Take(10).ToList());
        }
        public void Save()
        {
            Income income = new Income();
            income.IncomeCode = Helper.nextId().ToString();
            income.IncomeName = IncomeName;
            income.FamilyName = FamilyName;
            income.FamilyTel = FamilyTel;
            income.IncomeNumber = IncomeNumber;
            income.Incomedate = Incomedate;
            income.Gcflag = false;
            income.Id = Id;
            if (Mode == "Add")
            {
                income.CreateTime = DateTime.Now;
                income.CreateUser = LoginViewModel.LoginUser.Account;
                income.UpdateTime = DateTime.Now;
                income.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.AddIncome(income);
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
                income.CreateTime = CreateTime;
                income.CreateUser = CreateUser;
                income.UpdateTime = DateTime.Now;
                income.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.EditIncome(income);
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
                IncomeCode = SelectList.IncomeCode;
                IncomeName = SelectList.IncomeName;
                Incomedate = SelectList.Incomedate;
                IncomeNumber = SelectList.IncomeNumber;
                FamilyName = SelectList.FamilyName;
                FamilyTel = SelectList.FamilyTel;
               
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
                    var isdel = server.DelIncome(SelectList);
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
            IncomeCode = string.Empty;
            IncomeName = string.Empty;
            Incomedate = DateTime.Today;
            IncomeNumber = 0;
            FamilyName = string.Empty;
            FamilyTel = string.Empty;
            StartDate = DateTime.Now.Date.AddMonths(-1);
            EndDate = DateTime.Now.Date;
            GetPageData();
        }
        public void GetPageData()
        {

            IncomeList.Clear();
            TotalIncomeList.Clear();
            Query query = new Query();
            query.QueryText = QueryText;
            query.StartDate = StartDate;
            query.EndDate = EndDate;
            var list = server.QueryIncome(query);
            list.ForEach(it =>
            {
                TotalIncomeList.Add(it);
            });
            int i = 0;
            foreach (var item in TotalIncomeList)
            {
                i++;
                if (i <= 10)
                {
                    IncomeList.Add(item);
                }
                else
                    break;
            }
        }

        private void Export()
        {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = "C:\\";
            string filename = string.Empty;
            string filetime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                 filename = dlg.FileName;

                XSSFWorkbook workBook = new XSSFWorkbook();  //实例化XSSF
                XSSFSheet sheet = (XSSFSheet)workBook.CreateSheet();  //创建一个sheet

                IRow frow0 = sheet.CreateRow(0);  // 添加一行（一般第一行是表头）
                frow0.CreateCell(0).SetCellValue(Application.Current.FindResource("Income_IncomeCode").ToString());
                frow0.CreateCell(1).SetCellValue(Application.Current.FindResource("Income_IncomeName").ToString());
                frow0.CreateCell(2).SetCellValue(Application.Current.FindResource("Income_Incomedate").ToString());   //表头内容
                frow0.CreateCell(3).SetCellValue(Application.Current.FindResource("Income_IncomeNumber").ToString());
                frow0.CreateCell(4).SetCellValue(Application.Current.FindResource("Income_FamilyName").ToString());
                frow0.CreateCell(5).SetCellValue(Application.Current.FindResource("Income_FamilyTel").ToString());
                for (int i = 0; i < IncomeList.Count; i++)  //循环添加list中的内容放到表格里
                {
                    IRow frow1 = sheet.CreateRow(i + 1);  //之所以从i+1开始 因为第一行已经有表头了
                    frow1.CreateCell(0).SetCellValue(IncomeList[i].IncomeCode);
                    frow1.CreateCell(1).SetCellValue(IncomeList[i].IncomeName);
                    frow1.CreateCell(2).SetCellValue(IncomeList[i].Incomedate);
                    frow1.CreateCell(3).SetCellValue(IncomeList[i].IncomeNumber);
                    frow1.CreateCell(4).SetCellValue(IncomeList[i].FamilyName);
                    frow1.CreateCell(5).SetCellValue(IncomeList[i].FamilyTel);
                }
                string saveFileName = filename + "\\" + Application.Current.FindResource("Income_TotalIncome").ToString() + filetime + ".xlsx";
                try
                {
                    using (FileStream fs = new FileStream(saveFileName, FileMode.Create, FileAccess.Write))
                    {
                        workBook.Write(fs);  //写入文件
                        workBook.Close();  //关闭
                    }
                    AduMessageBox.Show("导出成功");
                }
                catch (Exception)
                {
                    workBook.Close();
                }
            }
           
        }
        #endregion
    }

}
