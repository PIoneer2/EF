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

namespace EF.Web.Controllers
{
    public class GoodsController : Controller
    {
        private UnitOfWork unitOfWork;
        private Repository<Goods> goodsRepository;

        public GoodsController(UnitOfWork tmpUnit)
        {
            unitOfWork = tmpUnit;
            goodsRepository = unitOfWork.Repository<Goods>();
        }

        public ActionResult Index()
        {
            IEnumerable<Goods> goods = goodsRepository.Table.ToList();
            return View(goods);
        }

        // GET: Goods/Details/5
        public ActionResult Details(object id)
        {
            if (id != null)
            {

                if (id is int)
                {
                    Goods model = goodsRepository.GetById(id);
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

        // GET: Goods/CreateEditGood
        public ActionResult CreateEditGood(object id)
        {
            if (id != null)
            {
                if (id is int)
                {
                    Goods model = new Goods();
                    model = goodsRepository.GetById(id);
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

        [HttpPost, ActionName("CreateEditGood")]
        public ActionResult CreateEditGoodInPost(object mdl)//Goods
        {
            if (mdl != null)
            {
                if (mdl is Goods)
                {
                    Goods model = (Goods)mdl;

                    if (model.ID == 0)
            {
                model.Name = "";
                model.Quantity = 1;
                model.Info = "";
                goodsRepository.Insert(model);
            }
            else
            {
                var editModel = goodsRepository.GetById(model.ID);
                editModel.Name = model.Name; ;
                editModel.Quantity = model.Quantity;
                editModel.Info = model.Info;
                goodsRepository.Update(editModel);
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
                if (id is int)
                {
                    Goods model = goodsRepository.GetById(id);
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

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(object id)
        {
            if (id != null)
            {
                if (id is int)
                {
                    Goods model = goodsRepository.GetById(id);
                    goodsRepository.Delete(model);
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
