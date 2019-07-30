using System;
using System.ComponentModel.DataAnnotations;

namespace admin.Models
{
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Key]
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string LastIP { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleId { get; set; }
    }
}
