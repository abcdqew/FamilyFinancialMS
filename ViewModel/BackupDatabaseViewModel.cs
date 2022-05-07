using AduSkin.Controls.Data;
using AduSkin.Controls.Metro;
using FamilyFinancialMS.Common;
using FamilyFinancialMS.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyFinancialMS.ViewModel
{
     public class BackupDatabaseViewModel
    {
        public SaveServer server = new SaveServer();
        public BackupDatabaseViewModel()
        {

        }
        private RelayCommand backupCommand;
        public RelayCommand BackupCommand
        {
            get
            {
                if (backupCommand == null)
                {
                    backupCommand = new RelayCommand(() => Backup());
                }
                return backupCommand;
            }
        }
        private RelayCommand restoreCommand;
        public RelayCommand RestoreCommand
        {
            get
            {
                if (restoreCommand == null)
                {
                    restoreCommand = new RelayCommand(() => Restore());
                }
                return restoreCommand;
            }
        }

        public void Restore()
        {
            var isok = server.RestoreFileDialog();
            if (isok)
            {
                AduMessageBox.Show(Application.Current.FindResource("BackupDatabase_RestoreSucceeded").ToString());
            }
            else
            {
                AduMessageBox.Show(Application.Current.FindResource("BackupDatabase_RestoreFailed").ToString());
            }
        }

        public void Backup()
        {
            var isok=server.BackupFileDialog();
            if(isok)
            {
                AduMessageBox.Show(Application.Current.FindResource("BackupDatabase_BackupSucceeded").ToString());
            }
            else
            {
                AduMessageBox.Show(Application.Current.FindResource("BackupDatabase_BackupFailed").ToString());
            }
        }
    }
}
