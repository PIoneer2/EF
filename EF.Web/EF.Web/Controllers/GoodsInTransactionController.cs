﻿using System;
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
        private IUnitOfWork unitOfWork;
        private IRepository<GoodsInTransaction> goodsInTransactionsRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public GoodsInTransactionController(EFUnitOfWork tmpUnit)
        {
            unitOfWork = tmpUnit;
            goodsInTransactionsRepository = unitOfWork.Repository<GoodsInTransaction>();
        }

        // GET: GoodsInTransaction
        public ActionResult Index()
        {
            IEnumerable<GoodsInTransaction> goodsInTransactions = goodsInTransactionsRepository.Table.ToList();
            return View(goodsInTransactions);
        }

        // GET: GoodsInTransaction/Details/5
        public ActionResult Details(object id)
        {
            if (id != null)
            {

                if (id is long)
                {
                    GoodsInTransaction model = goodsInTransactionsRepository.GetById((long)id);
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

        // GET: GoodsInTransaction/CreateEditGoodsInTransaction
        public ActionResult CreateEditGoodsInTransaction(object id)
        {
            if (id != null)
            {
                if (id is long)
                {
                GoodsInTransaction model = new GoodsInTransaction();
                model = goodsInTransactionsRepository.GetById((long)id);
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

        [HttpPost,ActionName("CreateEditGoodsInTransaction")]
        public ActionResult CreateEditGoodsInTransactionInPost(object mdl)//GoodsInTransaction
        {
            if (mdl != null)
            {
                if (mdl is GoodsInTransaction)
                {
                    GoodsInTransaction model = (GoodsInTransaction)mdl;

                    if (model.Id == 0)
            {
                model.TransactionsId = 1;
                model.GoodsId = 1;
                model.Quantity = 1;
                goodsInTransactionsRepository.Insert(model);
            }
            else
            {
                var editModel = goodsInTransactionsRepository.GetById(model.Id);
                editModel.TransactionsId = model.TransactionsId;
                editModel.GoodsId = model.GoodsId;
                editModel.Quantity = model.Quantity;
                goodsInTransactionsRepository.Update(editModel);
            }
                    
                    if (model.Id > 0)
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
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: GoodsInTransaction/Delete/5
        public ActionResult Delete(object id)
        {
            if (id != null)
            {
                if (id is long)
                {
                    GoodsInTransaction model = goodsInTransactionsRepository.GetById((long)id);
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

        // POST: GoodsInTransaction/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(object id)
        {
            if (id != null)
            {
                if (id is long)
                {
                    GoodsInTransaction model = goodsInTransactionsRepository.GetById((long)id);
                    goodsInTransactionsRepository.Delete(model);
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
