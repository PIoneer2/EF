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
    public class RestrictionsSetController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<RestrictionsSet> restrictionsSetRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public RestrictionsSetController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            restrictionsSetRepository = unitOfWork.Repository<RestrictionsSet>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/RestrictionsSet
        [ActionName("DefaultAction")]
        public async Task<IQueryable<RestrictionsSet>> Get()//my
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(restrictionsSetRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/RestrictionsSet/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        public IQueryable<RestrictionsSet> All()    //all
        {
            try
            {
                return logic.Index<RestrictionsSet>(this.restrictionsSetRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/RestrictionsSet/5
        public JsonResult<RestrictionsSet> Get(long id)
        {
            try
            {
                return Json(logic.Details(restrictionsSetRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/RestrictionsSet
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]RestrictionsSetDTO restrictionsSet)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                RestrictionsSet blankRestrictionsSet = logic.CreateBlankModel(restrictionsSetRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(restrictionsSet, blankRestrictionsSet, false);
                logic.EditInPost(blankRestrictionsSet, restrictionsSetRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/RestrictionsSet/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Put([FromBody]RestrictionsSetDTO restrictionsSet)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                RestrictionsSet typicalRestrictionsSets = EFServiceLocator.GetService<RestrictionsSet>();
                logic.FromDTOtoBaseClass(restrictionsSet, typicalRestrictionsSets, true);
                logic.EditInPost(typicalRestrictionsSets, restrictionsSetRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/RestrictionsSet/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(restrictionsSetRepository, id);
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
