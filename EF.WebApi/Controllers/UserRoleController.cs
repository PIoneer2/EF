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
    [System.Web.Http.Authorize(Roles = "Admin")]
    public class UserRoleController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<UserRole> userRoleRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public UserRoleController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            userRoleRepository = unitOfWork.Repository<UserRole>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/UserRole
        [ActionName("DefaultAction")]
        public async Task<IQueryable<UserRole>> Get()//my
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(userRoleRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/UserRole/5
        public JsonResult<UserRole> Get(long id)
        {
            try
            {
                return Json(logic.Details(userRoleRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/UserRole
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]UserRoleDTO userRole)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                UserRole blankUserRole = logic.CreateBlankModel(userRoleRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(userRole, blankUserRole, false);
                logic.EditInPost(blankUserRole, userRoleRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/UserRole/5
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]UserRoleDTO userRole)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                UserRole typicalUserRole = EFServiceLocator.GetService<UserRole>();
                logic.FromDTOtoBaseClass(userRole, typicalUserRole, true);
                logic.EditInPost(typicalUserRole, userRoleRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/UserRole/5
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(userRoleRepository, id);
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
