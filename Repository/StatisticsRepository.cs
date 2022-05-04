using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyFinancialMS;
using Model.Entities;
using SqlSugar;


namespace Repository
{
   public class StatisticsRepository : DefaultContext
    {
        public List<Statistics> QuerySum(DateTime startDate,DateTime endDate)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                List<Statistics> list = new List<Statistics>();
                Statistics statistics = new Statistics();
                double incomesum = 0;
                double expendituresum = 0;
                if (startDate!=null&&startDate!=null)
                {
                    statistics.StatisticsStartDate = startDate.Date;
                    statistics.StatisticsEndDate = endDate.Date;
                    statistics.StatisticsTotalIncome = context.Client.Queryable<Income>().Where(it => it.Incomedate.Date >= startDate.Date && it.Incomedate.Date <= endDate.Date).Sum(it => it.IncomeNumber);
                    statistics.StatisticsTotalExpenditure = context.Client.Queryable<Expenditure>().Where(it => it.Expendituredate.Date >= startDate.Date && it.Expendituredate.Date <= endDate.Date).Sum(it => it.ExpenditureNumber);
                    statistics.StatisticsBalance = statistics.StatisticsTotalIncome - statistics.StatisticsTotalExpenditure;
                    list.Add(statistics);
                }
                else
                {
                    startDate= DateTime.Now.Date.AddMonths(-1);
                    endDate = DateTime.Now.Date;
                    incomesum = context.Client.Queryable<Income>().Where(it => it.Incomedate.Date >= startDate.Date && it.Incomedate.Date <= endDate.Date).Sum(it => it.IncomeNumber);
                    expendituresum = context.Client.Queryable<Expenditure>().Where(it => it.Expendituredate.Date >= startDate.Date && it.Expendituredate.Date <= endDate.Date).Sum(it => it.ExpenditureNumber);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
