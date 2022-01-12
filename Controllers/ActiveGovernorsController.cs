using csvTask.Data;
using csvTask.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace csvTask.Controllers
{
    public class ActiveGovernorsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private List<ActiveGovernor> ActiveGovernors { get; set; }    

        public ActiveGovernorsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<ActiveGovernor> objList = _db.ActiveGovernors;
            return View(objList);
        }


        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActiveGovernor obj)
        {
            if (ModelState.IsValid)
            {
                _db.ActiveGovernors.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        //GET-UPDATE
        public IActionResult Update(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ActiveGovernors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST-UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ActiveGovernor obj)
        {
            if (ModelState.IsValid)
            {
                _db.ActiveGovernors.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }





        //GET-DELETE
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ActiveGovernors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST-DELETE

        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ActiveGovernors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ActiveGovernors.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult CSV()
        {
            var activegov =_db.ActiveGovernors.ToList();
            var builder = new StringBuilder();
            builder.AppendLine("State,Capital,Governor");
            foreach(var gove in activegov)
            {
                builder.AppendLine($"{gove.State},{gove.Capital},{gove.Governor}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()),"text/csv","GovernorInfo.csv");
        }



    }
}
