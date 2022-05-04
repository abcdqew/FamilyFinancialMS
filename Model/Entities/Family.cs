using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    [SugarTable("tb_Family")]
    public class Family
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public bool Gcflag { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Tel { get; set; }
        public DateTime Birthday { get; set; }
        public bool Iswork { get; set; }
    }
}
