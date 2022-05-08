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
    public class FixedAssetsViewModel : ViewModelBase
    {
        public FixedAssetsViewModel()
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
        public FixedAssetsRepository server = new FixedAssetsRepository();
        private string queryText = string.Empty;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryText
        {
            get { return queryText; }
            set { queryText = value; RaisePropertyChanged(); }
        }
        private DateTime startDate = DateTime.Now.Date.AddMonths(-1);
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; RaisePropertyChanged(); }
        }
        private DateTime endDate = DateTime.Now.Date;
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
        private string code = string.Empty;
        /// <summary>
        /// 资产Code（雪花算法生成
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(); }
        }
        private string name = string.Empty;
        /// <summary>
        /// 资产名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        private string purchaser = string.Empty;
        /// <summary>
        /// 购买人姓名
        /// </summary>
        public string Purchaser
        {
            get { return purchaser; }
            set { purchaser = value; RaisePropertyChanged(); }
        }
        private double unitPrice = 0;
        /// <summary>
        /// 单价
        /// </summary>
        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; RaisePropertyChanged(); }
        }
        private double quantity = 0;
        /// <summary>
        /// 数量
        /// </summary>
        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; RaisePropertyChanged(); }
        }
        private double totalPrice = 0;
        /// <summary>
        /// 总金额
        /// </summary>
        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; RaisePropertyChanged(); }
        }
        private DateTime purchaseDate = DateTime.Today;
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<FixedAssets> fxedAssetsList = new ObservableCollection<FixedAssets>();
        /// <summary>
        /// 收入列表
        /// </summary>TotalUserList
        public ObservableCollection<FixedAssets> FixedAssetsList
        {
            get { return fxedAssetsList; }
            set { fxedAssetsList = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<FixedAssets> totalFixedAssetsList = new ObservableCollection<FixedAssets>();
        /// <summary>
        /// 总收入列表
        /// </summary>
        public ObservableCollection<FixedAssets> TotalFixedAssetsList
        {
            get { return totalFixedAssetsList; }
            set { totalFixedAssetsList = value; RaisePropertyChanged(); }
        }
        private FixedAssets selectList = null;
        public FixedAssets SelectList
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
            FixedAssetsList = new ObservableCollection<FixedAssets>(TotalFixedAssetsList.Skip((info.Info - 1) * 10).Take(10).ToList());
        }
        public void Save()
        {
            FixedAssets fixedAssets = new FixedAssets();
            fixedAssets.Code = Helper.nextId().ToString();
            fixedAssets.Name = Name;
            fixedAssets.Purchaser = Purchaser;
            fixedAssets.PurchaseDate = PurchaseDate;
            fixedAssets.UnitPrice = UnitPrice;
            fixedAssets.Quantity = Quantity;
            fixedAssets.TotalPrice =UnitPrice * Quantity;
            fixedAssets.Gcflag = false;
            fixedAssets.Id = Id;
            if (string.IsNullOrWhiteSpace(fixedAssets.Name) || fixedAssets.UnitPrice == 0 || fixedAssets.Quantity == 0 || string.IsNullOrWhiteSpace(fixedAssets.Purchaser))
            {
                AduMessageBox.Show(Application.Current.FindResource("RequiredNotSpace").ToString());
                return;
            }
            var isfamily = server.QueryFamily(fixedAssets.Purchaser);
            if (isfamily == 0)
            {
                AduMessageBox.Show(Application.Current.FindResource("Income_NoFamilyName").ToString());
                return;
            }
            
            if (Mode == "Add")
            {
                fixedAssets.CreateTime = DateTime.Now;
                fixedAssets.CreateUser = LoginViewModel.LoginUser.Account;
                fixedAssets.UpdateTime = DateTime.Now;
                fixedAssets.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.AddFixedAssets(fixedAssets);
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
                fixedAssets.CreateTime = CreateTime;
                fixedAssets.CreateUser = CreateUser;
                fixedAssets.UpdateTime = DateTime.Now;
                fixedAssets.UpdateUser = LoginViewModel.LoginUser.Account;
                var isAdd = server.EditFixedAssets(fixedAssets);
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
                PurchaseDate = SelectList.PurchaseDate;
                Purchaser = SelectList.Purchaser;
                UnitPrice = SelectList.UnitPrice;
                Quantity = SelectList.Quantity;
                TotalPrice = SelectList.TotalPrice;
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
                    var isdel = server.DelFixedAssets(SelectList);
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
            PurchaseDate = DateTime.Today;
            UnitPrice = 0;
            Quantity = 0;
            TotalPrice = 0;
            Purchaser = string.Empty;
            StartDate = DateTime.Now.Date.AddMonths(-1);
            EndDate = DateTime.Now.Date;
            GetPageData();
        }
        public void GetPageData()
        {
            FixedAssetsList.Clear();
            TotalFixedAssetsList.Clear();
            Query query = new Query();
            query.QueryText = QueryText;
            query.StartDate = StartDate;
            query.EndDate = EndDate;
            var list = server.QueryFixedAssets(query);
            list.ForEach(it =>
            {
                TotalFixedAssetsList.Add(it);
            });
            int i = 0;
            foreach (var item in TotalFixedAssetsList)
            {
                i++;
                if (i <= 10)
                {
                    FixedAssetsList.Add(item);
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
                for (int i = 0; i < FixedAssetsList.Count; i++)  //循环添加list中的内容放到表格里
                {
                    IRow frow1 = sheet.CreateRow(i + 1);  //之所以从i+1开始 因为第一行已经有表头了
                    frow1.CreateCell(0).SetCellValue(FixedAssetsList[i].Code);
                    frow1.CreateCell(1).SetCellValue(FixedAssetsList[i].Name);
                    frow1.CreateCell(2).SetCellValue(FixedAssetsList[i].PurchaseDate.ToString("d"));
                    frow1.CreateCell(3).SetCellValue(FixedAssetsList[i].UnitPrice);
                    frow1.CreateCell(4).SetCellValue(FixedAssetsList[i].Quantity);
                    frow1.CreateCell(5).SetCellValue(FixedAssetsList[i].TotalPrice);
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
