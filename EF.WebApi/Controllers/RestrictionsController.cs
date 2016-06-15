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
    public class RestrictionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Restrictions> restrictionsRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public RestrictionsController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            restrictionsRepository = unitOfWork.Repository<Restrictions>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/Restrictions
        [ActionName("DefaultAction")]
        public async Task<IQueryable<Restrictions>> Get()//my
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(restrictionsRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Restrictions/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        public IQueryable<Restrictions> All()    //all
        {
            try
            {
                return logic.Index<Restrictions>(this.restrictionsRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Restrictions/5
        public JsonResult<Restrictions> Get(long id)
        {
            try
            {
                return Json(logic.Details(restrictionsRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/Restrictions
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]RestrictionsDTO restrictions)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Restrictions blankRestrictions = logic.CreateBlankModel(restrictionsRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(restrictions, blankRestrictions, false);
                logic.EditInPost(blankRestrictions, restrictionsRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/Restrictions/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Put([FromBody]RestrictionsDTO restrictions)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Restrictions typicalRestrictionss = EFServiceLocator.GetService<Restrictions>();
                logic.FromDTOtoBaseClass(restrictions, typicalRestrictionss, true);
                logic.EditInPost(typicalRestrictionss, restrictionsRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/Restrictions/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(restrictionsRepository, id);
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
