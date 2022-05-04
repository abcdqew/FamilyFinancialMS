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
    public class IncomeRepsitory : DefaultContext
    {
        public List<Income> QueryIncome(Query model)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                List<Income> list = new List<Income>();
                if (!string.IsNullOrWhiteSpace(model.QueryText))
                {
                    list = context.Client.Queryable<Income>().Where(it => it.IncomeName.Contains(model.QueryText) && it.Gcflag == false&&it.Incomedate>=model.StartDate&&it.Incomedate<=model.EndDate).ToList();
                }
                else
                {
                    list = context.Client.Queryable<Income>().Where(it => it.Gcflag == false && it.Incomedate >= model.StartDate && it.Incomedate <= model.EndDate).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int AddIncome(Income income)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (income != null)
                {
                    issuccess = context.Client.Insertable(income).ExecuteCommand();
                    return issuccess;
                }
                else
                {
                    return issuccess;
                }
            }
            catch (Exception ex)
            {
                return issuccess;
            }
        }
        public int EditIncome(Income income)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (income != null)
                {
                    issuccess = context.Client.Updateable(income).ExecuteCommand();
                    return issuccess;
                }
                else
                {
                    return issuccess;
                }
            }
            catch (Exception ex)
            {
                return issuccess;
            }
        }
        public int DelIncome(Income income)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (income != null)
                {
                    issuccess = context.Client.Updateable(income).ExecuteCommand();
                    return issuccess;
                }
                else
                {
                    return issuccess;
                }
            }
            catch (Exception ex)
            {
                return issuccess;
            }
        }
    }
}
