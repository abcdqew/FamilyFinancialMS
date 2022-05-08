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
   public class IncExpTypeRepository : DefaultContext
    {
        public List<IncExpType> QueryIncExpType(Query model)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                List<IncExpType> list = new List<IncExpType>();
                if (!string.IsNullOrWhiteSpace(model.QueryText))
                {
                    list = context.Client.Queryable<IncExpType>().Where(it => it.Name.Contains(model.QueryText)).ToList();
                }
                else
                {
                    list = context.Client.Queryable<IncExpType>().Where(it => it.Gcflag == false).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int QueryFamily(string name)
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
                    }
                }
                return issuccess;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int AddIncExpType(IncExpType incExpType)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (incExpType != null)
                {
                    issuccess = context.Client.Insertable(incExpType).ExecuteCommand();
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
        public int EditIncExpType(IncExpType incExpType)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (incExpType != null)
                {
                    issuccess = context.Client.Updateable(incExpType).ExecuteCommand();
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
        public int DelIncExpType(IncExpType incExpType)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (incExpType != null)
                {
                    issuccess = context.Client.Updateable(incExpType).ExecuteCommand();
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
