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
   public class ExpenditureRepository : DefaultContext
    {
        public List<Expenditure> QueryExpenditure(Query model)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                List<Expenditure> list = new List<Expenditure>();
                if (!string.IsNullOrWhiteSpace(model.QueryText))
                {
                    list = context.Client.Queryable<Expenditure>().Where(it => it.ExpenditureName.Contains(model.QueryText) && it.Gcflag == false && it.Gcflag == false && it.Expendituredate >= model.StartDate && it.Expendituredate <= model.EndDate).ToList();
                }
                else
                {
                    list = context.Client.Queryable<Expenditure>().Where(it => it.Gcflag == false && it.Gcflag == false && it.Expendituredate >= model.StartDate && it.Expendituredate <= model.EndDate).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int QueryFamily(string name, string tel)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var family = context.Client.Queryable<Family>().Where(it => it.Name == name).First();
                    if (family != null)
                    {
                        issuccess = 1;
                        if (tel == family.Tel)
                        {
                            issuccess = 2;
                        }
                    }
                }
                return issuccess;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int AddExpenditure(Expenditure expenditure)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (expenditure != null)
                {
                    issuccess = context.Client.Insertable(expenditure).ExecuteCommand();
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
        public int EditExpenditure(Expenditure expenditure)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (expenditure != null)
                {
                    issuccess = context.Client.Updateable(expenditure).ExecuteCommand();
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
        public int DelExpenditure(Expenditure expenditure)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (expenditure != null)
                {
                    issuccess = context.Client.Updateable(expenditure).ExecuteCommand();
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
