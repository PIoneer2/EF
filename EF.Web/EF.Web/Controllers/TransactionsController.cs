using EF.Core;
using EF.Core.Data;
using EF.Web.SLocator;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EF.Web.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Transactions> transactionsRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public TransactionsController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            transactionsRepository = unitOfWork.Repository<Transactions>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: Transactions
        public async Task<ActionResult> Index() //my transactions
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>()); //вызов FindByIdAsync начинает искать юзера с правильным ИД, а возвращает
            //с неправильным ИД
            return View(logic.Index<Transactions>(this.transactionsRepository, currentUser.Id));
        }

        //GET: Transactions
        [Authorize(Roles = "Admin")]
        public ActionResult IndexAll()    //all transactions
        {
            return View(logic.Index<Transactions>(this.transactionsRepository, 0));
        }

        // GET: Transactions/Details/5
        public ActionResult Details(object id)
        {
            long outLong;
            if (long.TryParse(id.ToString(), out outLong))
            {
                return View(logic.Details<Transactions>(this.transactionsRepository, outLong));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Transactions/CreateEditTransaction
        public async Task<ActionResult> CreateEditTransaction(object id)
        {
            long outLong;
            ViewBag.UserId = new SelectList(unitOfWork.Repository<User>().Table.ToList(), "Id", "UserName");
            ViewBag.TranactionTypeId = new SelectList(unitOfWork.Repository<TranactionType>().Table.ToList(), "Id", "Name");
            //редактирование
            if (long.TryParse(id.ToString(), out outLong))
            {
                return View(logic.Details<Transactions>(this.transactionsRepository, outLong));
            }
            //создание новой записи
            else
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return View(logic.CreateBlankModel<Transactions>(this.transactionsRepository, currentUser.Id));
            }
        }

        [HttpPost, ActionName("CreateEditTransaction")]
        public async Task<ActionResult> CreateEditTransactionInPost([Bind(Include = "Id,Description,TranactionTypeId,UserId,Date")] Transactions mdl) //не работает после рефакторинга
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
            //mdl.UserId = currentUser.Id;
            logic.CreateEditInPost<Transactions>(mdl, this.transactionsRepository, currentUser.Id);
            //return View();
            return RedirectToAction("Index");
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(object id)
        {
            long outLong;
            if (long.TryParse(id.ToString(), out outLong))
            {
                return View(logic.Details<Transactions>(this.transactionsRepository, outLong));
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
            long outLong;
            if (long.TryParse(id.ToString(), out outLong))
            {
                logic.ConfirmDelete<Transactions>(this.transactionsRepository, outLong);
                return RedirectToAction("Index");
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
