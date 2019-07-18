using admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Models
{
    public class AdminContext : DbContext
    {
        public AdminContext() : base() { }
        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // 获取appsettings.json配置信息
        //    var config = new ConfigurationBuilder()
        //                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
        //                    .AddJsonFile("appsettings.json")
        //                    .Build();
        //    // 获取数据库连接字符串
        //    string conn = config.GetConnectionString("SqlConn");
        //    //连接数据库
        //    optionsBuilder.UseSqlServer(conn);
        //}
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
