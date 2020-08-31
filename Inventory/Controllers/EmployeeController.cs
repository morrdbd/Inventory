using Inventory.Attributes;
using Inventory.Models;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class EmployeeController : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        // GET: Employee
        [HttpGet]
        [AccessControl("View")]
        public ActionResult Search()
        {
            CreateDropDown();
            ViewBag.search = new Employee_Search();
            return View("Search", new Employee());
        }
        
        //Use in some controller
        [AccessControl]
        [HttpPost]
        public JsonResult EmployeeListPartial(Employee_Search model)
        {
            var query = db.Employees.Where(c => c.IsActive == true);
            if (!string.IsNullOrWhiteSpace(model.sName))
            {
                query = query.Where(c => c.Name.Contains(model.sName));
            }
            if (!string.IsNullOrWhiteSpace(model.sFatherName))
            {
                query = query.Where(c => c.FatherName.Contains(model.sFatherName));
            }
            if (model.sDepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.sDepartmentID);
            }
            if (!string.IsNullOrWhiteSpace(model.sOccupation))
            {
                query = query.Where(c => c.Occupation.Contains(model.sOccupation));
            }
            var records = query.OrderBy(e => e.InsertedDate).ToList();
            var data = records.Select(e => new
            {
                e.EmployeeID,
                e.Name,
                e.FatherName,
                BranchName = db.Departments.Where(r => r.IsActive == true && r.DepartmentID == e.DepartmentID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
                e.Occupation,
                e.PhoneNumber,
                e.IsActive
            }).ToList();
     
            ViewBag.search = model;
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count(),
                recordsFiltered = data.Count(),
                draw = model.draw,
            });
        }

        [AccessControl("Add")]
        public JsonResult Save(Employee model)
        {
            ViewBag.search = new Employee_Search();
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (model.EmployeeID == 0)
                    {
                        model.IsActive = true;
                        model.InsertedBy = AppUser.Id;
                        model.InsertedDate = DateTime.Now;
                        db.Employees.Add(model);
                    }
                    else
                    {
                        model.LastUpdatedBy = AppUser.Id;
                        model.LastUpdatedDate = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                        db.Entry(model).Property(x => x.IsActive).IsModified = false;
                        db.Entry(model).Property(x => x.InsertedBy).IsModified = false;
                        db.Entry(model).Property(x => x.InsertedDate).IsModified = false;
                    }
                    db.SaveChanges();
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [HttpGet]
        [AccessControl("Edit")]
        public JsonResult LoadEmployeeRecord(int id)
        {
            var obj = db.Employees.Find(id);
            if (obj != null)
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Delete")]
        public JsonResult DeleteEmployee(int id)
        {
            var obj = db.Employees.Find(id);
            if (obj != null)
            {
                obj.IsActive = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult loadDepartmentEmployee(int id = 0)
        {
            var employeesList = db.Employees.Where(x => x.IsActive == true && x.DepartmentID == id)
                .Select(x=> new { employeeID = x.EmployeeID, name = (x.Name +" ولد "+x.FatherName)});

            return Json(employeesList, JsonRequestBehavior.AllowGet);
        }

        //Load search result
        [AccessControl]
        public JsonResult SearchEmployee(Employee_Search model)
        {
            var query = db.Employees.Select(c => new
            {
                c.EmployeeID,
                c.Name,
                c.FatherName,
                c.Occupation,
                c.DepartmentID,
                c.IsActive
            }).Where(c => c.IsActive == true);
            if (!string.IsNullOrWhiteSpace(model.sName))
            {
                query = query.Where(c => c.Name.Contains(model.sName));
            }
            if (!string.IsNullOrWhiteSpace(model.sFatherName))
            {
                query = query.Where(c => c.FatherName.Contains(model.sFatherName));
            }
            if (model.sDepartmentID != null)
            {
                query = query.Where(c => c.DepartmentID == model.sDepartmentID);
            }
            ViewBag.search = model;
            var tes1 = query.ToList();

            var data = query.Select(c =>
            new
            {
                c.EmployeeID,
                c.Name,
                c.FatherName,
                c.Occupation,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == c.DepartmentID).Select(d => Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault()
            }
            ).ToList();
            var tes = data.ToList();
            return Json(data);
        }

        [ChildActionOnly]
        public ActionResult SelectEmployee()
        {
            //EmployeeLoginViewModel model = new EmployeeLoginViewModel();
            CreateDropDown();

            return PartialView("_SelectEmployee", new Employee_Search());
        }

        private void CreateDropDown()
        {
            var departmentList = db.Departments.Where(d => d.IsActive == true).Select(d =>
                new { d.DepartmentID, DepartmentName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.DepartmentDrp = new SelectList(departmentList, "DepartmentID", "DepartmentName");
        }
    }
}