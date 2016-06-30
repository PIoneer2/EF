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
    public class GoodsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Goods> goodsRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public GoodsController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            goodsRepository = unitOfWork.Repository<Goods>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/Goods
        [System.Web.Http.HttpGet]
        public IQueryable<Goods> Get()
        {
            try
            {
                return logic.Index(goodsRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Goods/All
        [System.Web.Http.HttpGet]
        [Route("take/All")]
        [System.Web.Http.Authorize(Roles = "Admin")]
        public IQueryable<Goods> All()
        {
            try
            {
                return logic.Index<Goods>(this.goodsRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Goods/5
        [System.Web.Http.HttpGet]
        public JsonResult<Goods> Get(long id)
        {
            try
            {
                return Json(logic.Details(goodsRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/Goods
        
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]GoodDTO good)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Goods blankGood = logic.CreateBlankModel(goodsRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(good, blankGood, false);
                logic.EditInPost(blankGood, goodsRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/Goods/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Put([FromBody]GoodDTO good)
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Goods typicalGood = EFServiceLocator.GetService<Goods>();
                logic.FromDTOtoBaseClass(good, typicalGood, true);
                logic.EditInPost(typicalGood, goodsRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/Goods/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(goodsRepository, id);
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
