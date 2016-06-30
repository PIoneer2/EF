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
    public class TypeOfStorageController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<TypeOfStorage> typeOfStorageRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public TypeOfStorageController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            typeOfStorageRepository = unitOfWork.Repository<TypeOfStorage>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/TypeOfStorage
        public IQueryable<TypeOfStorage> Get()//my
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(typeOfStorageRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/TypeOfStorage/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        [Route("take/All")]
        public IQueryable<TypeOfStorage> All()    //all
        {
            try
            {
                return logic.Index<TypeOfStorage>(this.typeOfStorageRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/TypeOfStorage/5
        public JsonResult<TypeOfStorage> Get(long id)
        {
            try
            {
                return Json(logic.Details(typeOfStorageRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/TypeOfStorage
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]TypeOfStorageDTO typeOfStorage)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                TypeOfStorage blankTypeOfStorage = logic.CreateBlankModel(typeOfStorageRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(typeOfStorage, blankTypeOfStorage, false);
                logic.EditInPost(blankTypeOfStorage, typeOfStorageRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/TypeOfStorage/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Put([FromBody]TypeOfStorageDTO typeOfStorage)
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                TypeOfStorage typicalTypeOfStorage = EFServiceLocator.GetService<TypeOfStorage>();
                logic.FromDTOtoBaseClass(typeOfStorage, typicalTypeOfStorage, true);
                logic.EditInPost(typicalTypeOfStorage, typeOfStorageRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/TypeOfStorage/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(typeOfStorageRepository, id);
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
