using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using admin.Models;
using admin.Services;
using admin.Common;
using Microsoft.AspNetCore.Cors;

namespace admin.Api
{
    [EnableCors("cors")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        readonly UserService _userService;
        readonly RoleService _roleService;
        public UserController()
        {
            _userService = new UserService();
            _roleService = new RoleService();
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public OkObjectResult Get()
        {
            var list = _userService.Query();
            var roleList = _roleService.Query();
            return JsonRes.Success(from p in list
                                   select new
                                   {
                                       p.Username,
                                       p.Email,
                                       p.RoleId,
                                       roleName = roleList.Where(d => d.Id == p.RoleId).FirstOrDefault().Name,
                                       p.CreateTime
                                   });
        }

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("Single")]
        public OkObjectResult Single(string username)
        {
            var entity = _userService.QueryByID(username);
            return JsonRes.Success(entity);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Email"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpPost]
        public OkObjectResult Post(string Username, string Password, string Email, string RoleId)
        {
            User exist = _userService.QueryByID(Username);
            if(exist != null)
            {
                return JsonRes.Fail("用户名已存在");
            }
            User entity = new User
            {
                Username = Username,
                Password = Password,
                Email = Email,
                RoleId = RoleId,
                CreateTime = DateTime.Now.ToLocalTime(),
                UpdateTime = DateTime.Now.ToLocalTime()
            };
            string error = "";
            int res = _userService.TryAdd(out error, entity);
            if (res == 0)
            {
                return JsonRes.Fail(entity, error);
            }
            return JsonRes.Success(entity);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Email"></param>
        /// <param name="RoleId"></param>
        [HttpPut]
        public OkObjectResult Put(string Username, string Email, string RoleId)
        {
            User exist = _userService.QueryByID(Username);
            if (exist == null)
            {
                return JsonRes.Fail("用户名不存在");
            }
            exist.Email = Email;
            exist.RoleId = RoleId;
            exist.UpdateTime = DateTime.Now.ToLocalTime();
            string error = "";
            int res = _userService.TryUpdate(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPut("ModifyPassword")]
        public OkObjectResult ModifyPassword(string Username, string OldPassword, string NewPassword)
        {
            User exist = _userService.QueryByID(Username);
            if (exist == null)
            {
                return JsonRes.Fail("用户名不存在");
            }
            if(exist.Password != OldPassword)
            {
                return JsonRes.Fail("原密码不正确");
            }
            exist.Password = NewPassword;
            string error = "";
            int res = _userService.TryUpdate(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Username"></param>
        [HttpDelete("{Username}")]
        public OkObjectResult Delete(string Username)
        {
            User exist = _userService.QueryByID(Username);
            if (exist == null)
            {
                return JsonRes.Fail("用户名不存在");
            }
            string error = "";
            int res = _userService.TryDelete(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }
    }
}
