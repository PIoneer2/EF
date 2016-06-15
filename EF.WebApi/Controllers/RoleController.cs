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
    public class RoleController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Role> roleRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public RoleController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            roleRepository = unitOfWork.Repository<Role>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/Role
        [ActionName("DefaultAction")]
        public async Task<IQueryable<Role>> Get()//my
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                return logic.Index(roleRepository, currentUser.Id).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Role/All
        [System.Web.Http.HttpGet]
        public IQueryable<Role> All()    //all
        {
            try
            {
                return logic.Index<Role>(this.roleRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/Role/5
        public JsonResult<Role> Get(long id)
        {
            try
            {
                return Json(logic.Details(roleRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/Role
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]RoleDTO role)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Role blankRole = logic.CreateBlankModel(roleRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(role, blankRole, false);
                logic.EditInPost(blankRole, roleRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/Role/5
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]RoleDTO role)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                Role typicalRoles = EFServiceLocator.GetService<Role>();
                logic.FromDTOtoBaseClass(role, typicalRoles, true);
                logic.EditInPost(typicalRoles, roleRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/Role/5
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(roleRepository, id);
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
