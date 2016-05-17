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
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Goods> goodsRepository;

        public GoodsController()
        {
            goodsRepository = unitOfWork.Repository<Goods>();
        }

        public ActionResult Index()
        {
            IEnumerable<Goods> goods = goodsRepository.Table.ToList();
            return View(goods);
        }

        // GET: Goods/Details/5
        public ActionResult Details(int id)
        {
            Goods model = goodsRepository.GetById(id);
            return View(model);
        }

        // GET: Goods/CreateEditGood
        public ActionResult CreateEditGood(int? id)
        {
            Goods model = new Goods();
            if (id.HasValue)
            {
                model = goodsRepository.GetById(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEditGood(Goods model)
        {
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

        // GET: Goods/Delete/5
        public ActionResult Delete(int id)
        {
            Goods model = goodsRepository.GetById(id);
            return View(model);
        }

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Goods model = goodsRepository.GetById(id);
            goodsRepository.Delete(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        /*
        // GET: Goods
        public ActionResult Index()
        {
            return View(db.Goods.ToList());
        }

        // GET: Goods/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // GET: Goods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Goods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Quantity,Info")] Goods goods)
        {
            if (ModelState.IsValid)
            {
                db.Goods.Add(goods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goods);
        }

        // GET: Goods/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // POST: Goods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Quantity,Info")] Goods goods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goods);
        }

        // GET: Goods/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Goods goods = db.Goods.Find(id);
            db.Goods.Remove(goods);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
