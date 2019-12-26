using Inventory.Attributes;
using Inventory.Controllers;
using Inventory.Models;
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
        //[AccessControl("Add")]
        public ActionResult Add()
        {
            var model = CreateModel(0);
            return View("Form",model);
        }
        private DistTicket_VM CreateModel(int id)
        {
            var _Ticket = db.DistributionTicketPC5.Where(t=>t.Id == id).SingleOrDefault();


            return new DistTicket_VM
            {
                Ticket = _Ticket ?? new DistributionTicketPC5(),
                TicketItemData = db.TicketItem.Where(i=>i.TicketID == id).ToList()
            };
        }

        [HttpPost]
        [AccessControl("Add")]
        public JsonResult Insert(DistTicket_VM model)
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
                    var Ticket = new DistributionTicketPC5
                    {
                        TicketNumber = model.Ticket.TicketNumber,
                        TicketIssuedDate = model.Ticket.TicketIssuedDateForm.ToDate(Language),
                        Warehouse = model.Ticket.Warehouse,
                        RequestNumber = model.Ticket.RequestNumber,
                        RequestingDate = model.Ticket.RequestingDateForm.ToDate(Language),
                        Requester = model.Ticket.Requester,
                        Details = model.Ticket.Details,
                        InsertedBy = AppUser.Id,
                        InsertedDate = DateTime.Now,
                        IsActive = true,
                    };
                 
                    db.DistributionTicketPC5.Add(Ticket);
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "DistributionTicketPC5",
                        ModfiedId = Ticket.Id,
                        Action = "Insert",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(Ticket),
                    });
                    db.SaveChanges();

                    if (model.TicketItemData != null)
                    {
                        foreach (var row in model.TicketItemData)
                        {
                            if (row != null)
                            {
                                row.TicketID = Ticket.Id;
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
                    return Json(Ticket.Id);
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
        public ActionResult Search(TicketSearch search)
        {
         
            ViewBag.search = search;
            return View();
        }

        public JsonResult ListPartial(TicketSearch search)
        {
            Response.Cookies.Add(new HttpCookie("prc_search", JsonConvert.SerializeObject(search)));
            var DateFrom = search.DateFrom.ToDate(Language);
            var DateTo = search.DateTo.ToDate(Language);
            var data = db.DistributionTicketPC5.ToList();
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
            var model = CreateModel(id);
            return View(model);
        }

        [HttpPost]
        [AccessControl("Edit")]
        public JsonResult Update(DistTicket_VM model)
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
                    var Ticket = db.DistributionTicketPC5.Find(model.Ticket.Id);
                    if (Ticket == null) return Json(false);
                    Ticket.TicketNumber = model.Ticket.TicketNumber;
                    Ticket.TicketIssuedDate = model.Ticket.TicketIssuedDateForm.ToDate(Language);
                    Ticket.Warehouse = model.Ticket.Warehouse;
                    Ticket.RequestNumber = model.Ticket.RequestNumber;
                    Ticket.RequestingDate = model.Ticket.RequestingDateForm.ToDate(Language);
                    Ticket.Requester = model.Ticket.Requester;
                    Ticket.Details = model.Ticket.Details;
                    Ticket.IsActive = true;
                    Ticket.LastUpdatedBy = AppUser.Id;
                    Ticket.LastUpdatedDate = DateTime.Now;
                   
                    db.SaveChanges();

                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "DistributionTicketPC5",
                        ModfiedId = Ticket.Id,
                        Action = "Update",
                        UserId = AppUser.Id,
                        ModifiedTime = DateTime.Now,
                        ModifiedData = JsonConvert.SerializeObject(Ticket),
                    });
                    db.SaveChanges();

                    db.TicketItem.Where(x => x.TicketID == Ticket.Id).ToList().ForEach(x => x.IsActive = false);
                    db.SaveChanges();
                    if (model.TicketItemData != null)
                    {
                        foreach (var row in model.TicketItemData)
                        {
                            if (row != null)
                            {
                                string action = row.ID == 0 ? "Insert" : "Update";
                                if (row.ID == 0)
                                {
                                    row.TicketID = Ticket.Id;
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
                    return Json(Ticket.Id);
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
                var obj = db.DistributionTicketPC5.Find(id);
                if (obj != null)
                {
                    obj.IsActive = false;
                    db.ActivityLogs.Add(new ActivityLog
                    {
                        ModifiedTable = "DistributionTicketPC5",
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
            var model = CreateModel(id);

            return View("Form", model);
        }
    }
}