using AduSkin.Controls.Metro;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class DefaultContext
    {
        public SqlSugarClient Client { get; }
        public DefaultContext()
        {

        }
        //public static SqlSugarScope db = new SqlSugarScope(new ConnectionConfig()
        //{
        //    ConnectionString = "Server=127.0.0.1;Database=DB_FamilyFinancial;user id=Sa;pwd=123456",//"Data Source=./demo.db",
        //    DbType = DbType.SqlServer,
        //    IsAutoCloseConnection = true,
        //});
        public DefaultContext(string connectionString, DbType dbType)
        {
            Client = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connectionString,//"Data Source=./demo.db",
                DbType = dbType,
                IsAutoCloseConnection = true,
            });
        }
        
    }
}
