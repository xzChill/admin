using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using admin.Models;
using admin.Services;
using admin.Common;

namespace admin.Api
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        readonly RoleService _roleService;
        public RoleController()
        {
            _roleService = new RoleService();
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public OkObjectResult Get()
        {
            var list = _roleService.Query();
            return JsonRes.Success(list.ToArray());
        }

        /// <summary>
        /// 根据id获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Single")]
        public OkObjectResult Single(string id)
        {
            var entity = _roleService.QueryByID(id);
            return JsonRes.Success(entity);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Memo"></param>
        /// <returns></returns>
        [HttpPost]
        public OkObjectResult Post(string Id, string Name, string Memo)
        {
            Role exist = _roleService.QueryByID(Id);
            if(exist != null)
            {
                return JsonRes.Fail("角色已存在");
            }
            Role entity = new Role
            {
                Id = Id,
                Name = Name,
                Memo = Memo,
                CreateTime = DateTime.Now.ToLocalTime(),
            };
            string error = "";
            int res = _roleService.TryAdd(out error, entity);
            if (res == 0)
            {
                return JsonRes.Fail(entity, error);
            }
            return JsonRes.Success(entity);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="Memo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public OkObjectResult Put(string id, string Name, string Memo)
        {
            Role exist = _roleService.QueryByID(id);
            if (exist == null)
            {
                return JsonRes.Fail("角色不存在");
            }
            exist.Name = Name;
            exist.Memo = Memo;
            string error = "";
            int res = _roleService.TryUpdate(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public OkObjectResult Delete(string id)
        {
            var exist = _roleService.QueryByID(id);
            if (exist == null)
            {
                return JsonRes.Fail("角色不存在");
            }
            string error = "";
            int res = _roleService.TryDelete(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }
    }
}
