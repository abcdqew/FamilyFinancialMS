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
   public class MenuRepository: DefaultContext
    {
        public List<Menu> GetMenus()
        {
            var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
            return context.Client.Queryable<Menu>().Where(it => it.Authorities == 0).ToList();
        }
    }
}
