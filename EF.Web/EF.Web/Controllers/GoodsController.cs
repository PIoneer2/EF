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
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using EF.Core;
using EF.Web.SLocator;

namespace EF.Web.Controllers
{
    public class GoodsController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Goods> goodsRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public GoodsController(EFUnitOfWork tmpUnit)
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            goodsRepository = unitOfWork.Repository<Goods>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        public ActionResult Index()
        {
            return View(/*BL.Index<Goods>(this.goodsRepository)*/);
        }

        // GET: Goods/Details/5
        public ActionResult Details(object id)
        {
            if (id != null)
            {

                if (id is long)
                {
                    return View(/*BL.Details<Goods>((long)id, this.goodsRepository)*/);
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
                    return View(/*BL.Details<Goods>((long)id, this.goodsRepository)*/);
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
        public async Task<ActionResult> CreateEditGoodInPost(object mdl)
        {
            if (mdl != null)
            {
                if (mdl is Goods)
                {
                    var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                    return View(/*BL.CreateEditInPost<Goods>(mdl, this.goodsRepository, currentUser.Id)*/);
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
                    return View(/*BL.Details<Goods>((long)id, this.goodsRepository)*/);
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
                    //BL.ConfirmDelete<Goods>((long)id, this.goodsRepository);
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
