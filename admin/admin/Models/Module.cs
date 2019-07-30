using System;
using System.ComponentModel.DataAnnotations;

namespace admin.Models
{
    public class Module
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 是否基础模块
        /// </summary>
        public int IsBase { get; set; }
    }
}
