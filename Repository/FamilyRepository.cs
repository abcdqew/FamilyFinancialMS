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
   public class FamilyRepository : DefaultContext
    {
        public List<Family> QueryFamily(Family family)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                List<Family> list = new List<Family>();
                if (!string.IsNullOrWhiteSpace(family.Name) || !string.IsNullOrWhiteSpace(family.Name))
                {
                    list = context.Client.Queryable<Family>().Where(it => it.Name.Contains(family.Name)&&it.Gcflag==false).ToList();
                }
                else
                {
                    list = context.Client.Queryable<Family>().Where(it=> it.Gcflag == false).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int AddFamily(Family family)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (family != null)
                {
                    issuccess = context.Client.Insertable(family).ExecuteCommand();
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
        public int EditFamily(Family family)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (family != null)
                {
                    issuccess = context.Client.Updateable(family).ExecuteCommand();
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
        public int DelFamily(Family family)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (family != null)
                {
                    issuccess = context.Client.Updateable(family).ExecuteCommand();
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
