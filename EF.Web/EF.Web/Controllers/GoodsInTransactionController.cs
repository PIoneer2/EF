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
    public class GoodsInTransactionController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GoodsInTransaction> goodsInTransactionsRepository;

        public GoodsInTransactionController()
        {
            goodsInTransactionsRepository = unitOfWork.Repository<GoodsInTransaction>();
        }

        // GET: GoodsInTransaction
        public ActionResult Index()
        {
            IEnumerable<GoodsInTransaction> goodsInTransactions = goodsInTransactionsRepository.Table.ToList();
            return View(goodsInTransactions);
        }

        // GET: GoodsInTransaction/Details/5
        public ActionResult Details(int id)
        {
            GoodsInTransaction model = goodsInTransactionsRepository.GetById(id);
            return View(model);
        }

        // GET: GoodsInTransaction/CreateEditGoodsInTransaction
        public ActionResult CreateEditGoodsInTransaction(int? id)
        {
            GoodsInTransaction model = new GoodsInTransaction();
            if (id.HasValue)
            {
                model = goodsInTransactionsRepository.GetById(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEditGoodsInTransaction(GoodsInTransaction model)
        {
            if (model.ID == 0)
            {
                model.TransactionsId = 1;
                model.GoodsId = 1;
                model.Quantity = 1;
                goodsInTransactionsRepository.Insert(model);
            }
            else
            {
                var editModel = goodsInTransactionsRepository.GetById(model.ID);
                editModel.TransactionsId = model.TransactionsId;
                editModel.GoodsId = model.GoodsId;
                editModel.Quantity = model.Quantity;
                goodsInTransactionsRepository.Update(editModel);
            }

            if (model.ID > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: GoodsInTransaction/Delete/5
        public ActionResult Delete(int id)
        {
            GoodsInTransaction model = goodsInTransactionsRepository.GetById(id);
            return View(model);
        }

        // POST: GoodsInTransaction/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            GoodsInTransaction model = goodsInTransactionsRepository.GetById(id);
            goodsInTransactionsRepository.Delete(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
