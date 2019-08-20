using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using admin.Models;
using admin.Services;
using admin.Common;

namespace admin.Api
{
    [EnableCors("cors")]
    [Route("api/[controller]")]
    public class CreateSQLController : ControllerBase
    {
        readonly ModuleService _moduleService;
        readonly UserService _userService;
        readonly RoleService _roleService;
        readonly ModuleOperateService _moduleOperateService;
        readonly PowerService _powerService;

        public CreateSQLController()
        {
            _moduleService = new ModuleService();
            _userService = new UserService();
            _roleService = new RoleService();
            _moduleOperateService = new ModuleOperateService();
            _powerService = new PowerService();
        }
        [HttpGet]
        public OkObjectResult Get(string ip, string dbName, string userId, string pwd)
        {
            try
            {
                string connStr = string.Format(@"Server={0};User ID={1};Password={2};", ip, userId, pwd);
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                //判断数据库是否存在
                string dbExistSql = "select * From master.dbo.sysdatabases where name='" + dbName + "'";
                SqlCommand dbExistCmd = new SqlCommand(dbExistSql, conn);
                var dbExistReader = dbExistCmd.ExecuteReader();
                // 如果数据库不存在
                if (!dbExistReader.HasRows)
                {
                    dbExistReader.Close();
                    string createDbSql = "CREATE DATABASE " + dbName;
                    SqlCommand createDbCmd = new SqlCommand(createDbSql, conn);
                    createDbCmd.ExecuteNonQuery();
                }
                conn.Close();

                conn = new SqlConnection(string.Format(@"Server={0};Database={1};User ID={2};Password={3};", ip, dbName, userId, pwd));
                conn.Open();
                var sqlPath = Path.Combine(AppContext.BaseDirectory, "1zm4kjic.sql");
                var sqlStream = new FileStream(sqlPath, FileMode.Open);
                var reader = new StreamReader(sqlStream);
                //string sqlStr = reader.ReadToEnd().Replace("\n", " ").Replace("\r", " ").Replace("GO", " ");
                string sqlStr = reader.ReadToEnd().Replace("GO", " ");
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                int status = cmd.ExecuteNonQuery();
                reader.Close();
                sqlStream.Close();
                conn.Close();
                return Ok(new
                {
                    status
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    e.Message
                });
            }
        }

        [HttpGet("InsertData")]
        public OkObjectResult InsertData()
        {
            // 模块数据
            List<Module> modules = new List<Module>
            {
                new Module{Name = "用户管理", Link = "/user", Icon = "layui-icon-username"},
                new Module{Name = "角色管理", Link = "/role", Icon = "layui-icon-face-smile"},
                new Module{Name = "模块管理", Link = "/module", Icon = "layui-icon-template-1"},
                new Module{Name = "权限管理", Link = "/power", Icon = "layui-icon-password"},
            };
            List<Module> insertModules = new List<Module>();
            modules.ForEach((module) =>
            {
                var exist = _moduleService.QuerySingle(d => d.Name == module.Name && d.Link == module.Link);
                if(exist != null)
                {
                    return;
                }
                module.Id = Guid.NewGuid().ToString();
                module.Sort = modules.IndexOf(module) + 1;
                module.IsBase = 1;
                insertModules.Add(module);
            });
            string error;
            var moduleNum = _moduleService.TryAdd(out error, insertModules.ToArray());
            return JsonRes.Success(new {
                moduleNum,
                error
            });
        }
    }
}