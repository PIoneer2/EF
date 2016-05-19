using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF.Core.Data;
using EF.Data;
using EF.Core;
using System.Runtime.CompilerServices;

namespace EF.Web.Controllers
{
    public class TransactionsController : Controller
    {

        private UnitOfWork unitOfWork;
        private Repository<Transactions> transactionsRepository;

        public TransactionsController (UnitOfWork tmpUnit)
            {
            unitOfWork = tmpUnit;
            transactionsRepository = unitOfWork.Repository<Transactions>();
            }

    // GET: Transactions
    public ActionResult Index()
        {
            IEnumerable<Transactions> transactions = transactionsRepository.Table.ToList();
            return View(transactions);
        }

        // GET: Transactions/Details/5

        public ActionResult Details(object id)
        {
            if (id != null) {
                
                if (id is int)
                {
                    Transactions model = transactionsRepository.GetById(id);
                    return View(model);
                }

                //if (id is ...)
                //{
                //    ...
                //}

                else
                {
                    return RedirectToAction("Index");
                }
            }
            else {
                return RedirectToAction("Index");
            }
        }

        // GET: Transactions/CreateEditTransaction
        public ActionResult CreateEditTransaction(object id)
        {
            if (id != null)
            {
                if (id is int )
                { 
                Transactions model = new Transactions();
                model = transactionsRepository.GetById(id);
                return View(model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("CreateEditTransaction")]
        public ActionResult CreateEditTransactionInPost(object mdl) //Transactions model
        {
                if (mdl != null)
                {
                    if (mdl is Transactions)
                    {
                        Transactions model = (Transactions)mdl;

                    if (0 == model.ID)
                    {
                        model.Date = System.DateTime.Now;
                        model.Description = "";
                        model.TranactionTypeId = 1;
                        model.UsersId = 1;
                        transactionsRepository.Insert(model);
                    }
                    else
                    {
                        var editModel = transactionsRepository.GetById(model.ID);
                        editModel.Description = model.Description; ;
                        editModel.TranactionTypeId = model.TranactionTypeId;
                        editModel.UsersId = model.UsersId;
                        editModel.Date = model.Date;
                        transactionsRepository.Update(editModel);
                    }

                    if (model.ID > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(model);
                }

                //if (id is ...)
                //{
                //    ...
                //}

                else
                {
                    return RedirectToAction("Index");
                }
            }
            else {
                return RedirectToAction("Index");
            }
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(object id)
        {
            if (id != null)
            {
                if (id is int)
                {
                    Transactions model = transactionsRepository.GetById(id);
                    return View(model);
                }

                //if (id is ...)
                //{
                //    ...
                //}

                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(object id)
        {
            if (id != null)
            {
                if (id is int)
                {
                    Transactions model = transactionsRepository.GetById(id);
                    transactionsRepository.Delete(model);
                    return RedirectToAction("Index");
                }

                //if (id is ...)
                //{
                //    ...
                //}

                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
