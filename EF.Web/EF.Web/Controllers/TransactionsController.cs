using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF.Core.Data;
using EF.Data;
using EF.Core;

namespace EF.Web.Controllers
{
    public class TransactionsController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Transactions> transactionsRepository;

        public TransactionsController ()
            {
            transactionsRepository = unitOfWork.Repository<Transactions>();
            }

    // GET: Transactions
    public ActionResult Index()
        {
            IEnumerable<Transactions> transactions = transactionsRepository.Table.ToList();
            return View(transactions);
        }

        // GET: Transactions/Details/5

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Transactions model = transactionsRepository.GetById(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Transactions/CreateEditTransaction
        public ActionResult CreateEditTransaction(int? id)
        {
            Transactions model = new Transactions();
            if (id.HasValue)
            {
                model = transactionsRepository.GetById(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEditTransaction(Transactions model)
        {
            if (model.ID == 0)
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

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Transactions model = transactionsRepository.GetById(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Transactions model = transactionsRepository.GetById(id);
            transactionsRepository.Delete(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
