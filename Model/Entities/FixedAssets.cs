using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    [SugarTable("tb_FixedAssets")]
    public class FixedAssets
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
        public string Purchaser { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
