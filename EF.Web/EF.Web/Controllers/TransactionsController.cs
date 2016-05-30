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

        public TransactionsController(EFUnitOfWork tmpUnit)
        {
            unitOfWork = tmpUnit;
            transactionsRepository = unitOfWork.Repository<Transactions>();
            
            var uStore = new CustomUserStore(EFDbContext.Create());
            manager = new UserManager<User, long>(uStore);
            
        }

        // GET: Transactions
        
        public ActionResult Index() //my
        {
            var currentUser = manager.FindById(User.Identity.GetUserId<long>());
            return View(BL.Index<Transactions>(this.transactionsRepository, currentUser.Id));
        }

        //GET: Transactions/My
        [Authorize(Roles = "Admin")]
        public ActionResult all()    //only my transactions
        {
            
            return View(BL.Index<Transactions>(this.transactionsRepository));
        }

        // GET: Transactions/Details/5
        public ActionResult Details(object id)
        {
            if (id != null)
            {
                if (id is int)
                {
                    return View(BL.Details<Transactions>((int)id, this.transactionsRepository));
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

        // GET: Transactions/CreateEditTransaction
        public ActionResult CreateEditTransaction(object id)//должно работать после рефакторинга
        {
            if (id != null)
            {
                int outInt;
                if (int.TryParse(id.ToString(), out outInt))
                {
                    return View(BL.Details<Transactions>(outInt, this.transactionsRepository));
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
        public async Task<ActionResult> CreateEditTransactionInPost([Bind(Include = "Id,Description,TranactionTypeId,AspNetUsersId,Date")] Transactions mdl) //не работает после рефакторинга
        {
                    var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                    mdl.UserId = currentUser.Id;
                    return View(BL.CreateEditInPost<Transactions>(mdl, this.transactionsRepository));
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(object id)//не работает после рефакторинга
        {
            if (id != null)
            {
                if (id is int)
                {
                    return View(BL.Details<Transactions>((int)id, this.transactionsRepository));
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

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(object id)//не работает после рефакторинга
        {
            if (id != null)
            {
                if (id is int)
                {
                    BL.ConfirmDelete<Transactions>((int)id, this.transactionsRepository);
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
