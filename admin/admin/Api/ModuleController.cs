using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using admin.Models;
using admin.Services;
using admin.Common;

namespace admin.Api
{
    [EnableCors("cors")]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        ModuleService _moduleService;
        public ModuleController()
        {
            _moduleService = new ModuleService();
        }

        [HttpGet]
        public OkObjectResult Get()
        {
            var list = _moduleService.Query();
            return JsonRes.Success(list.ToArray());
        }

        [HttpGet("Single")]
        public OkObjectResult Single(string id)
        {
            var entity = _moduleService.QueryByID(id);
            return JsonRes.Success(entity);
        }

        [HttpPost]
        public OkObjectResult Post(string Name, string Link, string Icon)
        {
            var list = _moduleService.Query();
            int sort = 1;
            if (list.Count() != 0)
            {
                list.Sort((a, b) => { return a.Sort - b.Sort; });
                sort = list.LastOrDefault().Sort + 1;
            }
            
            var entity = new Module
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Link = Link,
                Sort = sort,
                Icon = Icon,
                IsBase = 0
            };
            string error = "";
            int res = _moduleService.TryAdd(out error, entity);
            if (res == 0)
            {
                return JsonRes.Fail(entity, error);
            }
            return JsonRes.Success(entity);
        }

        [HttpPut]
        public OkObjectResult Put(string Id, string Name, string Link, string Icon)
        {
            var exist = _moduleService.QueryByID(Id);
            if(exist == null)
            {
                return JsonRes.Fail("模块不存在");
            }
            exist.Name = Name;
            exist.Link = Link;
            exist.Icon = Icon;
            string error = "";
            int res = _moduleService.TryUpdate(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }

        [HttpDelete("{id}")]
        public OkObjectResult Delete(string id)
        {
            var exist = _moduleService.QueryByID(id);
            if (exist == null)
            {
                return JsonRes.Fail("模块不存在");
            }

            string error = "";
            int res = _moduleService.TryDelete(out error, exist);
            if (res == 0)
            {
                return JsonRes.Fail(exist, error);
            }
            return JsonRes.Success(exist);
        }
    }
}