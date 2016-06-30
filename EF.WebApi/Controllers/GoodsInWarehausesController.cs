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
    public class GoodsInWarehausesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<GoodsInWarehauses> goodsInWarehausesRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public GoodsInWarehausesController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            goodsInWarehausesRepository = unitOfWork.Repository<GoodsInWarehauses>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/GoodsInWarehauses
        public IQueryable<GoodsInWarehauses> Get()//my
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(goodsInWarehausesRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/GoodsInWarehauses/All
        [Route("take/All")]
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        public IQueryable<GoodsInWarehauses> All()    //all
        {
            try
            {
                return logic.Index<GoodsInWarehauses>(this.goodsInWarehausesRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/GoodsInWarehauses/5
        public JsonResult<GoodsInWarehauses> Get(long id)
        {
            try
            {
                return Json(logic.Details(goodsInWarehausesRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/GoodsInWarehauses
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]GoodsInWarehausesDTO goodsInWarehauses)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                GoodsInWarehauses blankGoodsInWarehauses = logic.CreateBlankModel(goodsInWarehausesRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(goodsInWarehauses, blankGoodsInWarehauses, false);
                logic.EditInPost(blankGoodsInWarehauses, goodsInWarehausesRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/GoodsInWarehauses/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Put([FromBody]GoodsInWarehausesDTO goodsInWarehauses)
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                GoodsInWarehauses typicalGoodsInWarehausess = EFServiceLocator.GetService<GoodsInWarehauses>();
                logic.FromDTOtoBaseClass(goodsInWarehauses, typicalGoodsInWarehausess, true);
                logic.EditInPost(typicalGoodsInWarehausess, goodsInWarehausesRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/GoodsInWarehauses/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(goodsInWarehausesRepository, id);
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
