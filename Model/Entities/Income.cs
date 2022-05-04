using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    [SugarTable("tb_Income")]
   public class Income
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public bool Gcflag { get; set; }

        public string IncomeCode { get; set; }
        public string IncomeName { get; set; }
        public string FamilyName { get; set; }
        public double IncomeNumber { get; set; }
        public DateTime Incomedate { get; set; }
        public string FamilyTel { get; set; }
    }
}
