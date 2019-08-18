using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace admin.Api
{
    [EnableCors("cors")]
    [Route("api/[controller]")]
    public class CreateSQLController : ControllerBase
    {
        [HttpGet]
        public OkObjectResult Get(string ip,string dbName, string userId, string pwd)
        {
            try
            {
                string connStr = string.Format(@"Server={0};User ID={1};Password={2};", ip, userId, pwd);
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                //判断数据库是否存在
                string dbExistSql = "select * From master.dbo.sysdatabases where name='"+ dbName + "'";
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
    }
}