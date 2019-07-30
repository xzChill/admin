using System;
using System.Collections.Generic;
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
    public class PowerController : ControllerBase
    {
        PowerService _powerService;
        ModuleService _moduleService;
        ModuleOperateService _moduleOperateService;
        RoleService _roleService;
        public PowerController()
        {
            _powerService = new PowerService();
            _moduleService = new ModuleService();
            _moduleOperateService = new ModuleOperateService();
            _roleService = new RoleService();
        }
        [HttpGet]
        public OkObjectResult Get(string roleId)
        {
            var roleExist = _roleService.QueryByID(roleId);
            if (roleExist == null)
            {
                return JsonRes.Fail("角色不存在");
            }
            var moduleList = _moduleService.Query();
            var moduleOpList = _moduleOperateService.Query();
            var powerList = _powerService.Query(d => d.RoleId == roleId).ToList();
            var moduleOpIds = powerList.Select(d => d.ModuleOperateId).ToList();
            var moduleIds = moduleOpList.Where(d => moduleOpIds.Contains(d.Id)).Select(d => d.ModuleId).Distinct().ToList();
            var json = (from m in moduleList
                        orderby m.Sort
                        select new
                        {
                            m.Id,
                            title = m.Name,
                            spread = true,
                            //hasPower = moduleIds.Contains(m.Id),
                            children = (from mop in moduleOpList
                                        where mop.ModuleId == m.Id
                                        orderby mop.Sort
                                        select new
                                        {
                                            mop.Id,
                                            title = mop.Name,
                                            @checked = moduleOpIds.Contains(mop.Id)
                                        }).ToArray()
                        }).ToArray();
            return JsonRes.Success(json);
        }
        [HttpPost("UpdateModuleOp")]
        public OkObjectResult UpdateModuleOp(string roleId, string ModuleOpId, bool operate)
        {
            var roleExist = _roleService.QueryByID(roleId);
            if (roleExist == null)
            {
                return JsonRes.Fail("角色不存在");
            }
            var powerExist = _powerService.Query(d => d.RoleId == roleId && d.ModuleOperateId == ModuleOpId).FirstOrDefault();
            string error = "";
            if (operate)
            {
                if (powerExist == null)
                {
                    var entity = new Power
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = roleId,
                        ModuleOperateId = ModuleOpId
                    };
                    int res = _powerService.TryAdd(out error, entity);
                    if (res == 0)
                    {
                        return JsonRes.Fail(entity, error);
                    }
                    return JsonRes.Success(entity);                    
                }
            }
            else
            {
                if (powerExist != null)
                {
                    int res = _powerService.TryDelete(out error, powerExist);
                    if (res == 0)
                    {
                        return JsonRes.Fail(powerExist, error);
                    }
                    return JsonRes.Success(powerExist);
                }
            }
            return JsonRes.Success(null);
        }
    }
}