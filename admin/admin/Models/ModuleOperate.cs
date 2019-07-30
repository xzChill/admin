using System;
using System.ComponentModel.DataAnnotations;

namespace admin.Models
{
    public class ModuleOperate
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 模块操作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模块操作链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 所属模块
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}
