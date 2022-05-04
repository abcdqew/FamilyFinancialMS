using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Statistics
    {
        public DateTime StatisticsStartDate { get; set; }
        public DateTime StatisticsEndDate { get; set; }
        public double StatisticsTotalIncome { get; set; }
        public double StatisticsTotalExpenditure { get; set; }
        public double StatisticsBalance { get; set; }
    }
}
