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
    public class StatisticsViewModel : ViewModelBase
    {
        public StatisticsViewModel()
        {
            GetPageData();
        }

        #region 定义
        public StatisticsRepository server = new StatisticsRepository();
       

        private DateTime statisticsStartDate = DateTime.Now.Date.AddMonths(-1);
        public DateTime StatisticsStartDate
        {
            get { return statisticsStartDate; }
            set { statisticsStartDate = value; RaisePropertyChanged(); }
        }
        private DateTime statisticsEndDate = DateTime.Now.Date;
        public DateTime StatisticsEndDate
        {
            get { return statisticsEndDate; }
            set { statisticsEndDate = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Statistics> statisticsList = new ObservableCollection<Statistics>();
        /// <summary>
        /// 收入列表
        /// </summary>TotalUserList
        public ObservableCollection<Statistics> StatisticsList
        {
            get { return statisticsList; }
            set { statisticsList = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Statistics> totalStatisticsList = new ObservableCollection<Statistics>();
        /// <summary>
        /// 总收入列表
        /// </summary>
        public ObservableCollection<Statistics> TotalStatisticsList
        {
            get { return totalStatisticsList; }
            set { totalStatisticsList = value; RaisePropertyChanged(); }
        }
        private Statistics selectList = null;
        public Statistics SelectList
        {
            get { return selectList; }
            set { selectList = value; RaisePropertyChanged(); }
        }
        #endregion
        #region 执行事件
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
            StatisticsList = new ObservableCollection<Statistics>(TotalStatisticsList.Skip((info.Info - 1) * 10).Take(10).ToList());
        }

        private void GetPageData()
        {
            StatisticsList.Clear();
            TotalStatisticsList.Clear();
            var incomesum = server.QuerySum(StatisticsStartDate, StatisticsEndDate);
            incomesum.ForEach(it =>
            {
                TotalStatisticsList.Add(it);
            });
            int i = 0;
            foreach (var item in TotalStatisticsList)
            {
                i++;
                if (i <= 10)
                {
                   StatisticsList.Add(item);
                }
                else
                    break;
            }
        }
        public void ResetQuery()
        {
            StatisticsStartDate = DateTime.Now.Date.AddMonths(-1);
            StatisticsEndDate = DateTime.Now.Date;
            GetPageData();
        }
        #endregion
    }
}
