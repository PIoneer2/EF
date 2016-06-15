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
    public class SizesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Sizes> sizesRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public SizesController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            sizesRepository = unitOfWork.Repository<Sizes>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/Sizes
        [ActionName("DefaultAction")]
        public async Task<IQueryable<Sizes>> Get()//my
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(sizesRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Sizes/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        public IQueryable<Sizes> All()    //all
        {
            try
            {
                return logic.Index<Sizes>(this.sizesRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Sizes/5
        public JsonResult<Sizes> Get(long id)
        {
            try
            {
                return Json(logic.Details(sizesRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/Sizes
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]SizesDTO sizes)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Sizes blankSizes = logic.CreateBlankModel(sizesRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(sizes, blankSizes, false);
                logic.EditInPost(blankSizes, sizesRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/Sizes/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Put([FromBody]SizesDTO sizes)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Sizes typicalSizes = EFServiceLocator.GetService<Sizes>();
                logic.FromDTOtoBaseClass(sizes, typicalSizes, true);
                logic.EditInPost(typicalSizes, sizesRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/Sizes/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(sizesRepository, id);
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
