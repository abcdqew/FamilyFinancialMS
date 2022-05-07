using FamilyFinancialMS;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository
{
    public class SaveServer
    {
        public SaveServer()
        {

        }
        // 连接字符串
        private string connectionString = ConnectionStringsl.connectionStringsl;
        public bool BackupFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "备份数据库";
            saveFileDialog.Filter = "备份文件(*.bak)|*.bak";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                bool ok = Backup("DB_FamilyFinancial", saveFileDialog.FileName);
                return true;
            }
            return false;
        }
        // 备份数据库
        private bool Backup(string dbName, string filePath)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = string.Format("backup database {0} to disk = '{1}'", dbName, filePath);

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool RestoreFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "还原数据库";
            openFileDialog.Filter = "备份文件(*.bak)|*.bak";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog()==true)
            {
                bool ok = Restore("DB_FamilyFinancial", openFileDialog.FileName);
                if(ok)
                {
                    return true;
                }
            }
            return false;
        }
        // 还原数据库
        private bool Restore(string dbName, string filePath)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = string.Format("select spid from sysprocesses,sysdatabases where sysprocesses.dbid=sysdatabases.dbid and sysdatabases.Name='{0}'", dbName);

            // 获取当前所有连接进程
            List<short> list = new List<short>();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetInt16(0));
                }
                reader.Close();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            //// 杀死当前所有连接进程
            ////try
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        connection.Open();
            //        command = new SqlCommand(string.Format("kill {0}", list[i].ToString()), connection);
            //        command.ExecuteNonQuery();
            //        connection.Close();
            //    }
            //}
            ////catch
            //{
            //    //return false;
            //}
            ////finally
            //{
            //    connection.Close();
            //}

            // 还原数据库
            connection.Open();
            command.CommandText = string.Format("use master;restore database {0} from disk = '{1}' with replace", dbName, filePath);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
    }
}
