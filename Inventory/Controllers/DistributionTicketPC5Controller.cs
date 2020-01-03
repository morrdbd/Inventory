using Inventory.Attributes;
using Inventory.Controllers;
using Inventory.Models;
using Inventory.Models.Procedures;
using Inventory.Models.Repositories;
using Inventory.Models.Tables;
using Inventory.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [AccessControl]
    public class DistributionTicketPC5Controller : BaseController
    {
        InventoryDBContext db = new InventoryDBContext();
        AdminRepository AdminRepo = new AdminRepository();
        [AccessControl("Add")]
        public ActionResult Add()
        {
            CreateDropDown();
            return View("Form", new Ticket_VM());
        }

        private void CreateDropDown()
        {
            var unitList = db.LookupValues.Where(l => l.LookupCode == "UNIT" && l.IsActive == true).OrderBy(l=>l.ForOrdering).Select(l =>
             new { l.ValueId, UnitName = Language == "prs" ? l.DrName : Language == "ps" ? l.PaName : l.EnName }).ToList();
            ViewBag.UnitDrp = new SelectList(unitList, "ValueId", "UnitName");

            var branchList =  db.Branches.Where(d=>d.IsActive == true).OrderBy(d => d.ForOrdering).Select(d =>
               new { d.BranchID, BranchName = Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName }).ToList();
            ViewBag.BranchDrp = new SelectList(branchList, "BranchID", "BranchName");
        }

        private Ticket_VM CreateModel(Ticket _ticket)
        {
            return new Ticket_VM
            {
                TicketID = _ticket.TicketID,
                TicketNumber = _ticket.TicketNumber,
                TicketIssuedDateVM = _ticket.TicketIssuedDate.ToDateString(Language),
                Warehouse = _ticket.Warehouse,
                RequestNumber = _ticket.RequestNumber,
                RequestDateVM = _ticket.RequestDate.ToDateString(Language),
                BranchID = _ticket.BranchID,
                BranchName = db.Branches.Where(r => r.IsActive == true && r.BranchID == _ticket.BranchID).Select(d =>
                Language == "prs" ? d.DrName : Language == "ps" ? d.PaName : d.EnName).FirstOrDefault(),
                Details = _ticket.Details,
                TicketItems = db.Database.SqlQuery<Ticket_Item>("Ticket_Items @p0, @p1", _ticket.TicketID, Language).ToList(),
            };
        }

        [HttpPost]
        [AccessControl("Add")]
        public JsonResult Insert(Ticket_Form_VM model)
        {
            if (ModelState.IsValid == false)
            {
                LogModelErrors();
                return Json(false);
            }
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var _Ticket = new Ticket
                    {
                        TicketNumber = model.TicketNumber,
                        TicketIssuedDate = model.TicketIssuedDateVM.ToDate(Language),
                        Warehouse = model.Warehouse,
                        RequestNumber = model.RequestNumber,
                        RequestDate = model.RequestDateVM.ToDate(Language),
                        BranchID = model.BranchID,
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.Tickets.Add(_Ticket);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Ticket",
                        ModfiedId = _Ticket.TicketID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(_Ticket),
                    });
                    db.SaveChanges();

                    if (model.TicketItems != null)
                    {
                        foreach (var row in model.TicketItems)
                        {
                            if (row != null)
                            {
                                row.TicketID = _Ticket.TicketID;
                                row.InsertedBy = AppUser.Id;
                                row.InsertedDate = DateTime.Now;
                                row.IsActive = true;
                                db.TicketItem.Add(row);
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "TicketItem",
                                    ModfiedId = row.ID,
                                    Action = "Insert",
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                            }
                        }
                    }

                    trans.Commit();
                    return Json(_Ticket.TicketID);
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }
            return Json(false);
        }

        [AccessControl("Search")]
        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.search = new TicketSearch();
            CreateDropDown();
            return View("Search");
        }

        [AccessControl("Search")]
        public JsonResult ListPartial(TicketSearch model)
        {
            var TicketIssuedDateFrom = model.DateFrom.ToDate(Language);
            var TicketIssuedDateTo = model.DateTo.ToDate(Language);

            var query = db.Tickets.Where(t=>t.IsActive == true);

            if (model.RequestNumber != null && model.RequestNumber != 0)
            {
                query = query.Where(c => c.RequestNumber == model.RequestNumber);
            }
            if (model.TicketNumber != null && model.TicketNumber != 0)
            {
                query = query.Where(c => c.TicketNumber == model.TicketNumber);
            }
            if (model.BranchID != 0)
            {
                query = query.Where(c => c.BranchID == model.BranchID);
            }
            if (TicketIssuedDateFrom != null)
            {
                query = query.Where(c => c.TicketIssuedDate >= TicketIssuedDateFrom);
            }
            if (TicketIssuedDateTo != null)
            {
                query = query.Where(c => c.TicketIssuedDate <= TicketIssuedDateTo);
            }
            ViewBag.search = model;
            var records = query.OrderBy(t=>t.TicketIssuedDate).ToList();
            var data = records.Select(t => new
            {
                t.TicketID,
                t.TicketNumber,
                TicketIssuedDate = t.TicketIssuedDate.ToDateString(Language),
                t.Warehouse,
                t.RequestNumber,
                RequestDate = t.RequestDate.ToDateString(Language),
                BranchName = db.Branches.Where(r => r.IsActive == true && r.BranchID == t.BranchID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault()
            }).ToList();
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                draw = model.draw,
            });
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var ticket = db.Tickets.Where(t=>t.IsActive == true).SingleOrDefault(t => t.TicketID == id);
            if(ticket == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(ticket);
            return View(model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(Ticket_Form_VM model)
        {
            if (ModelState.IsValid == false)
            {
                LogModelErrors();
                return Json(false);
            }
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var Ticket = db.Tickets.Find(model.TicketID);
                    if (Ticket == null) return Json(false);
                    Ticket.TicketNumber = model.TicketNumber;
                    Ticket.TicketIssuedDate = model.TicketIssuedDateVM.ToDate(Language);
                    Ticket.Warehouse = model.Warehouse;
                    Ticket.RequestNumber = model.RequestNumber;
                    Ticket.RequestDate = model.RequestDateVM.ToDate(Language);
                    Ticket.BranchID = model.BranchID;
                    Ticket.Details = model.Details;
                    Ticket.IsActive = true;
                    Ticket.LastUpdatedBy = AppUser.Id;
                    Ticket.LastUpdatedDate = DateTime.Now;

                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Ticket",
                        ModfiedId = Ticket.TicketID,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(Ticket),
                    });
                    db.SaveChanges();

                    db.TicketItem.Where(x => x.TicketID == Ticket.TicketID).ToList().ForEach(x => x.IsActive = false);
                    db.SaveChanges();
                    if (model.TicketItems != null)
                    {
                        foreach (var row in model.TicketItems)
                        {
                            if (row != null)
                            {
                                string action = row.ID == 0 ? "Insert" : "Update";
                                if (row.ID == 0)
                                {
                                    row.TicketID = Ticket.TicketID;
                                    row.InsertedBy = AppUser.Id;
                                    row.InsertedDate = DateTime.Now;
                                    row.IsActive = true;
                                    db.TicketItem.Add(row);
                                }
                                else
                                {
                                    var obj = db.TicketItem.Find(row.ID);
                                    if (obj != null)
                                    {
                                        obj.Quantity = row.Quantity;
                                        obj.UnitID = row.UnitID;
                                        obj.ItemDetails = row.ItemDetails;
                                        obj.UnitPrice = row.UnitPrice;
                                        obj.DealWithAccount = row.DealWithAccount;
                                        obj.LastUpdatedBy = AppUser.Id;
                                        obj.LastUpdatedDate = DateTime.Now;
                                        obj.IsActive = true;
                                    }
                                }
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "TicketItem",
                                    ModfiedId = row.ID,
                                    Action = action,
                                    UserId = AppUser.Id,
                                    ModifiedTime = DateTime.Now,
                                    ModifiedData = JsonConvert.SerializeObject(row),
                                });
                                db.SaveChanges();
                            }
                        }
                    }
            trans.Commit();
            return Json(Ticket.TicketID);
        }
                catch (Exception e)
                {
                    trans.Rollback();
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
    }
}
            return Json(false);
        }

        [AccessControl("Delete")]
        public JsonResult Delete(int id = 0)
        {
            try
            {
                var obj = db.Tickets.Find(id);
                if (obj != null)
                {
                    obj.IsActive = false;
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Ticket",
                        ModfiedId = id,
                        Action = "Delete",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                    });
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AccessControl("Edit")]
        public ActionResult Edit(int id = 0)
        {
            CreateDropDown();
            var ticket = db.Tickets.SingleOrDefault(t => t.TicketID == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(ticket);
            return View("Form", model);
        }

        [AccessControl("Search")]
        public ActionResult ItemInUse()
        {
            CreateDropDown();
            ViewBag.search = new ItemInUseSearch();
            return View("ItemInUse");
        }

        [AccessControl("Search")]
        public JsonResult ListItemInUse(ItemInUseSearch model)
        {
            var TicketIssuedDateFrom = model.DateFrom.ToDate(Language);
            var TicketIssuedDateTo = model.DateTo.ToDate(Language);

            var query = db.Tickets.Where(t => t.IsActive == true);

            if (model.TicketNumber != null && model.TicketNumber != 0)
            {
                query = query.Where(c => c.TicketNumber == model.TicketNumber);
            }
            if (model.BranchID != 0)
            {
                query = query.Where(c => c.BranchID == model.BranchID);
            }
            if (TicketIssuedDateFrom != null)
            {
                query = query.Where(c => c.TicketIssuedDate >= TicketIssuedDateFrom);
            }
            if (TicketIssuedDateTo != null)
            {
                query = query.Where(c => c.TicketIssuedDate <= TicketIssuedDateTo);
            }
            ViewBag.search = model;
            var records = (from t in query
                           join i in db.TicketItem
                           on t.TicketID equals i.TicketID
                           select new {
                               t.TicketID,
                               t.TicketNumber,
                               t.TicketIssuedDate,
                               t.BranchID,
                               t.RequestDate,
                               BranchName = db.Branches.Where(r => r.IsActive == true && r.BranchID == t.BranchID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
                               i.ItemDetails,
                               UnitName = db.LookupValues.Where(l=>l.IsActive == true && l.ValueId == i.UnitID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault(),
                               i.UnitPrice
                           }).OrderBy(t => t.TicketIssuedDate).ToList();
            //query.OrderBy(t => t.TicketIssuedDate).ToList();
            var data = records.Select(t => new
            {
                t.TicketID,
                t.TicketNumber,
                TicketIssuedDate = t.TicketIssuedDate.ToDateString(Language),
                RequestDate = t.RequestDate.ToDateString(Language),
                BranchName = db.Branches.Where(r => r.IsActive == true && r.BranchID == t.BranchID).Select(r => Language == "prs" ? r.DrName : Language == "ps" ? r.PaName : r.EnName).FirstOrDefault()
            }).ToList();
            return Json(new
            {
                data = data.Skip(model.start).Take(model.length).ToList(),
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                draw = model.draw,
            });
        }

    }
}