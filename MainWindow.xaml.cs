using AduSkin.Controls.Metro;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FamilyFinancialMS.ViewModel;
using FamilyFinancialMS.View;
using Model.Entities;
using MaterialDesignThemes.Wpf;

namespace FamilyFinancialMS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel();
            InitializeComponent();
            //定义窗口打开方式最大显示
            this.WindowState = WindowState.Maximized;
            //SystemParameters.WorkArea.Heigh:得到屏幕工作区域高度
            //SystemParameters.WorkArea.Width:得到屏幕工作区域宽度
            //定义窗口最小高度与宽度
            this.MinHeight = this.MaxHeight = this.Height = SystemParameters.WorkArea.Height + 17;
            this.MaxWidth = this.MinWidth = this.Width = SystemParameters.WorkArea.Width + 17;

            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("账号管理", new UserView()));
            menuRegister.Add(new SubItem("家庭成员管理", new FamilyView()));
            menuRegister.Add(new SubItem("收支类型管理", new IncExpTypeView()));
            menuRegister.Add(new SubItem("收入管理",new IncomeView()));
            menuRegister.Add(new SubItem("支出管理",new ExpenditureView()));
            menuRegister.Add(new SubItem("账目统计",new StatisticsView()));
            menuRegister.Add(new SubItem("固定资产", new FixedAssetsView()));
            menuRegister.Add(new SubItem("数据库备份", new BackupDatabaseView()));
            var item1 = new ItemMenu(Application.Current.FindResource("FamilyFinancialMS").ToString(), menuRegister, PackIconKind.Register);
            Menu.Children.Add(new UserControlMenuItem(item1, this));
        }
        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
