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
    public class FixedAssetsRepository : DefaultContext
    {
        public List<FixedAssets> QueryFixedAssets(Query model)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                List<FixedAssets> list = new List<FixedAssets>();
                if (!string.IsNullOrWhiteSpace(model.QueryText))
                {
                    list = context.Client.Queryable<FixedAssets>().Where(it => it.Name.Contains(model.QueryText) && it.Gcflag == false && it.PurchaseDate >= model.StartDate && it.PurchaseDate <= model.EndDate).ToList();
                }
                else
                {
                    list = context.Client.Queryable<FixedAssets>().Where(it => it.Gcflag == false && it.PurchaseDate >= model.StartDate && it.PurchaseDate <= model.EndDate).ToList();
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
        public int AddFixedAssets(FixedAssets fixedAssets)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (fixedAssets != null)
                {
                    issuccess = context.Client.Insertable(fixedAssets).ExecuteCommand();
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
        public int EditFixedAssets(FixedAssets fixedAssets)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (fixedAssets != null)
                {
                    issuccess = context.Client.Updateable(fixedAssets).ExecuteCommand();
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
        public int DelFixedAssets(FixedAssets fixedAssets)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (fixedAssets != null)
                {
                    issuccess = context.Client.Updateable(fixedAssets).ExecuteCommand();
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
