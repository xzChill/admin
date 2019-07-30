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
    public class ModuleOperateController : ControllerBase
    {
        ModuleOperateService _moduleOperateService;
        public ModuleOperateController()
        {
            _moduleOperateService = new ModuleOperateService();
        }

        [HttpGet]
        public OkObjectResult Get(string moduleId)
        {
            var list = _moduleOperateService.Query(d => d.ModuleId == moduleId);
            return JsonRes.Success(list.ToArray());
        }

        [HttpGet("Single")]
        public OkObjectResult Single(string id)
        {
            var entity = _moduleOperateService.QueryByID(id);
            return JsonRes.Success(entity);
        }

        [HttpPost]
        public OkObjectResult Post(string Name, string Link, string ModuleId, string Icon)
        {
            var list = _moduleOperateService.Query(d => d.ModuleId == ModuleId);
            int sort = 1;
            if (list.Count() != 0)
            {
                list.Sort((a, b) => { return a.Sort - b.Sort; });
                sort = list.LastOrDefault().Sort + 1;
            }
            var entity = new ModuleOperate
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Link = Link,
                ModuleId = ModuleId,
                Icon = Icon,
                Sort = sort
            };
            string error = "";
            int res = _moduleOperateService.TryAdd(out error, entity);
            if (res == 0)
            {
                return JsonRes.Fail(entity, error);
            }
            return JsonRes.Success(entity);
        }

        [HttpPut]
        public OkObjectResult Put(string Id, string Name, string Link, string Icon)
        {
            var exist = _moduleOperateService.QueryByID(Id);
            if (exist == null)
            {
                return JsonRes.Fail("模块不存在");
            }
            exist.Name = Name;
            exist.Link = Link;
            exist.Icon = Icon;
            string error = "";
            int res = _moduleOperateService.TryUpdate(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }

        [HttpDelete("{id}")]
        public OkObjectResult Delete(string id)
        {
            var exist = _moduleOperateService.QueryByID(id);
            if (exist == null)
            {
                return JsonRes.Fail("模块不存在");
            }

            string error = "";
            int res = _moduleOperateService.TryDelete(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }
    }
}