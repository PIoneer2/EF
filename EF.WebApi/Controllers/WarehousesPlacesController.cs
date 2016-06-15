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
    public class WarehousesPlacesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<WarehousesPlaces> warehousesPlacesRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public WarehousesPlacesController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            warehousesPlacesRepository = unitOfWork.Repository<WarehousesPlaces>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/WarehousesPlaces
        [ActionName("DefaultAction")]
        public async Task<IQueryable<WarehousesPlaces>> Get()//my
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(warehousesPlacesRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/WarehousesPlaces/All
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        public IQueryable<WarehousesPlaces> All()    //all
        {
            try
            {
                return logic.Index<WarehousesPlaces>(this.warehousesPlacesRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/WarehousesPlaces/5
        public JsonResult<WarehousesPlaces> Get(long id)
        {
            try
            {
                return Json(logic.Details(warehousesPlacesRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/WarehousesPlaces
        [System.Web.Http.HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Post([FromBody]WarehousesPlacesDTO warehousesPlaces)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                WarehousesPlaces blankWarehousesPlaces = logic.CreateBlankModel(warehousesPlacesRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(warehousesPlaces, blankWarehousesPlaces, false);
                logic.EditInPost(blankWarehousesPlaces, warehousesPlacesRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/WarehousesPlaces/5
        [System.Web.Http.HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IHttpActionResult> Put([FromBody]WarehousesPlacesDTO warehousesPlaces)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                WarehousesPlaces typicalWarehousesPlaces = EFServiceLocator.GetService<WarehousesPlaces>();
                logic.FromDTOtoBaseClass(warehousesPlaces, typicalWarehousesPlaces, true);
                logic.EditInPost(typicalWarehousesPlaces, warehousesPlacesRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/WarehousesPlaces/5
        [System.Web.Http.HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(warehousesPlacesRepository, id);
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
