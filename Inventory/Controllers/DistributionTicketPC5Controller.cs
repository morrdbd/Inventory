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
            return View("Form", new Ticket_VM());
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
                Requester = _ticket.Requester,
                Details = _ticket.Details,
                TicketItems = db.Database.SqlQuery<Ticket_Items>("Ticket_Items @p0, @p1", _ticket.TicketID, Language).ToList(),
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
                    var Ticket = new Ticket
                    {
                        TicketNumber = model.TicketNumber,
                        TicketIssuedDate = model.TicketIssuedDateVM.ToDate(Language),
                        Warehouse = model.Warehouse,
                        RequestNumber = model.RequestNumber,
                        RequestDate = model.RequestDateVM.ToDate(Language),
                        Requester = model.Requester,
                        Details = model.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };

                    db.Tickets.Add(Ticket);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "Ticket",
                        ModfiedId = Ticket.TicketID,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(Ticket),
                    });
                    db.SaveChanges();

                    if (model.TicketItems != null)
                    {
                        foreach (var row in model.TicketItems)
                        {
                            if (row != null)
                            {
                                row.TicketID = Ticket.TicketID;
                                row.InsertedBy = AppUser.Id;
                                row.InsertedDate = DateTime.Now;
                                row.IsActive = true;
                                db.TicketItem.Add(row);
                                db.SaveChanges();

                                db.ActivityLogs.Add(new ActivityLog
                                {
                                    ModifiedTable = "TicketItemData",
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

        [AccessControl("Search")]
        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.search = new TicketSearch();
            return View("Search");
        }

        public JsonResult ListPartial(TicketSearch search)
        {
            var DateFrom = search.DateFrom.ToDate(Language);
            var DateTo = search.DateTo.ToDate(Language);
            var data = db.Tickets.ToList();
            return Json(new
            {
                data = data.Skip(search.start).Take(search.length).ToList(),
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                draw = search.draw,
            });
        }

        [AccessControl("View")]
        public ActionResult View(int id = 0)
        {
            var ticket = db.Tickets.SingleOrDefault(t => t.TicketID == id);
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
                    Ticket.Requester = model.Requester;
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
            var ticket = db.Tickets.SingleOrDefault(t => t.TicketID == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(ticket);
            return View("Form", model);
        }
    }
}