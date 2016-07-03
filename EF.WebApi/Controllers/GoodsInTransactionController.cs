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
    public class GoodsInTransactionController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<GoodsInTransaction> goodsInTransactionRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public GoodsInTransactionController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            goodsInTransactionRepository = unitOfWork.Repository<GoodsInTransaction>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/GoodsInTransaction
        public IQueryable<GoodsInTransaction> Get()//my transactions
        {
            try
            {
                return logic.Index(goodsInTransactionRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/GoodsInTransaction/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        [Route("take/All")]
        public IQueryable<GoodsInTransaction> All()    //all transactions
        {
            try
            {
                return logic.Index<GoodsInTransaction>(this.goodsInTransactionRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/GoodsInTransaction/5
        public JsonResult<GoodsInTransaction> Get(long id)
        {
            try
            {
                return Json(logic.Details(goodsInTransactionRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/GoodsInTransaction
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]GoodsInTransactionDTO goodsInTransaction)
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                GoodsInTransaction blankGoodsInTransaction = logic.CreateBlankModel(goodsInTransactionRepository, 0);
                logic.FromDTOtoBaseClass(goodsInTransaction, blankGoodsInTransaction, false);
                logic.EditInPost(blankGoodsInTransaction, goodsInTransactionRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/GoodsInTransaction/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Put([FromBody]GoodsInTransactionDTO goodsInTransaction)
        {
            try
            {
                GoodsInTransaction typicalGoodsInTransactions = EFServiceLocator.GetService<GoodsInTransaction>();
                logic.FromDTOtoBaseClass(goodsInTransaction, typicalGoodsInTransactions, true);
                logic.EditInPost(typicalGoodsInTransactions, goodsInTransactionRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/GoodsInTransaction/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(goodsInTransactionRepository, id);
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
