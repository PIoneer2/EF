using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF.Core.Data;
using EF.Data;
using EF.Core;
using System.Runtime.CompilerServices;
using EF.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace EF.Web.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private EFUnitOfWork unitOfWork;
        private EFRepository<Transactions> transactionsRepository;
        private UserManager<User, long> manager;

        public TransactionsController(EFUnitOfWork tmpUnit, UserManager<EF.Core.Data.User, long> tmpUserManager)
        {
            unitOfWork = tmpUnit;
            transactionsRepository = unitOfWork.Repository<Transactions>();
            manager = tmpUserManager;
        }

        // GET: Transactions
        public ActionResult Index() //my transactions
        {
            var currentUser = manager.FindById(User.Identity.GetUserId<long>());
            //if (currentUser.Roles.Contains())
            return View(BL.Index<Transactions>(this.transactionsRepository, currentUser.Id));
        }

        //GET: Transactions
        [Authorize(Roles = "Admin")]
        public ActionResult IndexAll()    //all transactions
        {
            return View(BL.Index<Transactions>(this.transactionsRepository));
        }

        // GET: Transactions/Details/5
        public ActionResult Details(object id)
        {
            long outLong;
            if (long.TryParse(id.ToString(), out outLong))
            {
                return View(BL.Details<Transactions>(outLong, this.transactionsRepository));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Transactions/CreateEditTransaction
        public ActionResult CreateEditTransaction(object id)
        {
            long outLong;
            if (long.TryParse(id.ToString(), out outLong))
            { //редактирование
                return View(BL.Details<Transactions>(outLong, this.transactionsRepository));
            }
            else
            {//создание новой записи
             //вставить стартовые данные в пустые данные
                var currentUser = manager.FindById(User.Identity.GetUserId<long>());
                return View(BL.CreateBlankModel<Transactions>(this.transactionsRepository, currentUser.Id));
            }
        }

        [HttpPost, ActionName("CreateEditTransaction")]
        public async Task<ActionResult> CreateEditTransactionInPost([Bind(Include = "Id,Description,TranactionTypeId,UserId,Date")] Transactions mdl) //не работает после рефакторинга
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
            //mdl.UserId = currentUser.Id;
            BL.CreateEditInPost<Transactions>(mdl, this.transactionsRepository, currentUser.Id);
            //return View();
            return RedirectToAction("Index");
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(object id)//не работает после рефакторинга
        {
            long outLong;
            if (long.TryParse(id.ToString(), out outLong))
            {
                return View(BL.Details<Transactions>(outLong, this.transactionsRepository));
            }

            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(object id)//не работает после рефакторинга
        {
            long outLong;
            if (long.TryParse(id.ToString(), out outLong)) { 
            BL.ConfirmDelete<Transactions>(outLong, this.transactionsRepository);
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
