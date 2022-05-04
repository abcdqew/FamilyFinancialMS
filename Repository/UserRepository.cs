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
    public class UserRepository : DefaultContext
    {

        //public List<User> QueryUser()
        //{
        //    var list = db.Queryable<User>().ToList();
        //    return list;
        //}
        //public void Demo()
        //{

        //    //创建数据库对象
        //    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        //    {
        //        ConnectionString = "Server=127.0.0.1;Database=DB_FamilyFinancial;user id=Sa;pwd=123456",
        //        DbType = DbType.SqlServer,
        //        IsAutoCloseConnection = true
        //    });
        //    //查询表的所有
        //    var list = db.Queryable<User>().ToList
        //}
        public List<User> QueryUser(User user)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                List<User> list = new List<User>();
                if (!string.IsNullOrWhiteSpace(user.Account))
                {
                    list = context.Client.Queryable<User>().Where(it => it.Account.Contains(user.Account)&&it.Gcflag==false).ToList();
                }
                else
                {
                    list = context.Client.Queryable<User>().Where(it=> it.Gcflag == false).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int AddUser(User user)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (user!=null)
                {
                   issuccess = context.Client.Insertable(user).ExecuteCommand();
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
        public int EditUser(User user)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (user != null)
                {
                    issuccess = context.Client.Updateable(user).ExecuteCommand();
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
        public int DelUser(User user)
        {
            int issuccess = 0;
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if (user != null)
                {
                    issuccess = context.Client.Updateable(user).ExecuteCommand();
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
        public List<User> GetUser(string account)
        {
            var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
            return context.Client.Queryable<User>().Where(it => it.Account == account&&it.Gcflag==false).ToList();
        }
        public int AddLoginCounter(User user)
        {
            try
            {
                var context = new DefaultContext(ConnectionStringsl.connectionStringsl, DbType.SqlServer);
                if(user!=null)
                {
                    return context.Client.Updateable(user).ExecuteCommand();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                return 0;
            }
           
        }
    }
}
