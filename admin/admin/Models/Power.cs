using System;
using System.ComponentModel.DataAnnotations;

namespace admin.Models
{
    public class Power
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 模块操作
        /// </summary>
        public string ModuleOperateId { get; set; }
    }
}
