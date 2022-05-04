using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    [SugarTable("tb_User")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public bool Gcflag { get; set; }

        public string Account { get; set; }
        public string UserName { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool FlagAdmin { get; set; }

        /// <summary>
        /// 登陆次数
        /// </summary>
        public int LoginCounter { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
    }

}
