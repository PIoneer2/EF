using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EF.Core;
using EF.Core.Data;
using EF.Web.SLocator;
using System.Web.Http.Results;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EF.WebApi.Controllers
{
    [Authorize]
    public class TransactionsController : ApiController
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

        // GET: api/Transactions
        [ActionName("DefaultAction")]
        public async Task<IQueryable<Transactions>> Get()//my transactions
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(transactionsRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Transactions/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        public IQueryable<Transactions> All()    //all transactions
        {
            try
            {
                return logic.Index<Transactions>(this.transactionsRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Transactions/5
        public JsonResult<Transactions> Get(long id)
        {
            try
            {
                return Json(logic.Details(transactionsRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/Transactions
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> PostTransaction([FromBody]TransactionDTO transaction)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Transactions blankTransaction = logic.CreateBlankModel(transactionsRepository, currentUser.Id);
                logic.Transform(blankTransaction, transaction, false);
                logic.CreateEditInPost(blankTransaction, transactionsRepository, currentUser.Id);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/Transactions/5
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]TransactionDTO transaction)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Transactions typicalTransaction = new Transactions();
                logic.Transform(typicalTransaction, transaction, true);
                logic.CreateEditInPost(typicalTransaction, transactionsRepository, currentUser.Id);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/Transactions/5
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(transactionsRepository, id);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
