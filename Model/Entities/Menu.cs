using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Model.Entities
{
    [SugarTable("tb_Menu")]
    public class Menu
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public bool Gcflag { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string Caption { get; set; }

        public string NameSpace { get; set; }
        
        public int Authorities { get; set; }

        public string ParentName { get; set; }
    }
}
