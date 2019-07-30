using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    public class ModuleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult OperateIndex(string moduleId)
        {
            ViewBag.moduleId = moduleId;
            return View();
        }
        public IActionResult OperateCreate(string moduleId)
        {
            ViewBag.moduleId = moduleId;
            return View();
        }
        public IActionResult OperateEdit(string id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}