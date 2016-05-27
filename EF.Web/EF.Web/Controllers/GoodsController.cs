using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF.Core.Data;
using EF.Data;
using EF.Web.Models;

namespace EF.Web.Controllers
{
    public class GoodsController : Controller
    {
        private EFUnitOfWork unitOfWork;
        private EFRepository<Goods> goodsRepository;

        public GoodsController(EFUnitOfWork tmpUnit)
        {
            unitOfWork = tmpUnit;
            goodsRepository = unitOfWork.Repository<Goods>();
        }

        public ActionResult Index()
        {
            return View(BL.Index<Goods>(this.goodsRepository));
        }

        // GET: Goods/Details/5
        public ActionResult Details(object id)
        {
            if (id != null)
            {

                if (id is long)
                {
                    return View(BL.Details<Goods>((long)id, this.goodsRepository));
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

        // GET: Goods/CreateEditGood
        public ActionResult CreateEditGood(object id)
        {
            if (id != null)
            {
                if (id is long)
                {
                    return View(BL.Details<Goods>((long)id, this.goodsRepository));
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

        [HttpPost, ActionName("CreateEditGood")]
        public ActionResult CreateEditGoodInPost(object mdl)
        {
            if (mdl != null)
            {
                if (mdl is Goods)
                {
                    return View(BL.CreateEditInPost<Goods>(mdl, this.goodsRepository));
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

        // GET: Goods/Delete/5
        public ActionResult Delete(object id)
        {
            if (id != null)
            {
                if (id is long)
                {
                    return View(BL.Details<Goods>((long)id, this.goodsRepository));
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

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(object id)
        {
            if (id != null)
            {
                if (id is long)
                {
                    BL.ConfirmDelete<Goods>((long)id, this.goodsRepository);
                    return RedirectToAction("Index");
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

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
