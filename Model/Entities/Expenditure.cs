using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    [SugarTable("tb_Expenditure")]
   public class Expenditure
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public bool Gcflag { get; set; }

        public string ExpenditureCode { get; set; }
        public string ExpenditureName { get; set; }
        public string FamilyName { get; set; }
        public double ExpenditureNumber { get; set; }
        public DateTime Expendituredate { get; set; }
        public string FamilyTel { get; set; }
    }
}
