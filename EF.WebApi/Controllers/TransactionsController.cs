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
using System.Collections.Generic;

namespace EF.WebApi.Controllers
{
    [Authorize]
    //[RoutePrefix("api/Transactions")]
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

        /*
        [AllowAnonymous]
        //[System.Web.Http.ActionName("Options")]
        [System.Web.Http.HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage {
                StatusCode = System.Net.HttpStatusCode.OK            
        };
        }
        */

        // GET: api/Transactions
        //[ActionName("DefaultAction")]
        //[Route("Get")]
        [System.Web.Http.HttpGet]
        public async Task<IQueryable<TransactionDTO>> Get()//my transactions
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                IEnumerable <Transactions> usersFullTransactions = logic.Index<Transactions>(transactionsRepository, currentUser.Id);
                List<TransactionDTO> usersDTOTransactions = EFServiceLocator.GetService<List<TransactionDTO>>();
                foreach (Transactions transaction in usersFullTransactions)
                {
                    usersDTOTransactions.Add(logic.FromBaseToDTOTransaction(transaction));
                }
                return usersDTOTransactions.AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Transactions/take/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        [Route("take/All")]
        //[ActionName("All")]
        public IQueryable<TransactionDTO> All()    //all transactions
        {
            try
            {
                IEnumerable<Transactions> usersFullTransactions = logic.Index<Transactions>(transactionsRepository, 0);
                List<TransactionDTO> usersDTOTransactions = EFServiceLocator.GetService<List<TransactionDTO>>();
                foreach (Transactions transaction in usersFullTransactions)
                {
                    usersDTOTransactions.Add(logic.FromBaseToDTOTransaction(transaction));
                }
                return usersDTOTransactions.AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Transactions/5
        //[ActionName("Get")]
        //[Route("Get")]
        [System.Web.Http.HttpGet]
        public JsonResult<Transactions> Get(long id) //JsonResult<Transactions>
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
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]TransactionDTO transaction)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Transactions blankTransaction = logic.CreateBlankModel(transactionsRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(transaction, blankTransaction, false);
                logic.EditInPost(blankTransaction, transactionsRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/Transactions/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Put([FromBody]TransactionDTO transaction)
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Transactions typicalTransaction = EFServiceLocator.GetService<Transactions>();
                logic.FromDTOtoBaseClass(transaction, typicalTransaction, true);
                logic.EditInPost(typicalTransaction, transactionsRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/Transactions/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
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
