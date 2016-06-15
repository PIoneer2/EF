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
    public class TranactionTypeController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<TranactionType> tranactionTypeRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public TranactionTypeController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            tranactionTypeRepository = unitOfWork.Repository<TranactionType>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/TranactionType
        [ActionName("DefaultAction")]
        public async Task<IQueryable<TranactionType>> Get()//my
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(tranactionTypeRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/TranactionType/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        public IQueryable<TranactionType> All()    //all
        {
            try
            {
                return logic.Index<TranactionType>(this.tranactionTypeRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/TranactionType/5
        public JsonResult<TranactionType> Get(long id)
        {
            try
            {
                return Json(logic.Details(tranactionTypeRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/TranactionType
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]TranactionTypeDTO tranactionType)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                TranactionType blankTranactionType = logic.CreateBlankModel(tranactionTypeRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(tranactionType, blankTranactionType, false);
                logic.EditInPost(blankTranactionType, tranactionTypeRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/TranactionType/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Put([FromBody]TranactionTypeDTO tranactionType)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                TranactionType typicalTranactionType = EFServiceLocator.GetService<TranactionType>();
                logic.FromDTOtoBaseClass(tranactionType, typicalTranactionType, true);
                logic.EditInPost(typicalTranactionType, tranactionTypeRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/TranactionType/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(tranactionTypeRepository, id);
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
