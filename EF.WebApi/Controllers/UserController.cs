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
    [System.Web.Http.Authorize]
    public class UserController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRepository<User> userRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public UserController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            userRepository = unitOfWork.Repository<User>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET: api/User
        [System.Web.Http.HttpGet]
        public IQueryable<User> Get()
        {
            try
            {
                return logic.Index(userRepository, 0).AsQueryable();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // GET: api/User/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize(Roles = "Admin")]
        public JsonResult<User> Get(long id)
        {
            try
            {
                return Json(logic.Details(userRepository, id));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // POST: api/User
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Post([FromBody]UserDTO user)
        {
            try
            {
                var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                User blankUser = logic.CreateBlankModel(userRepository, currentUser.Id);
                logic.FromDTOtoBaseClass(user, blankUser, false);
                logic.EditInPost(blankUser, userRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // PUT: api/User/5
        [System.Web.Http.HttpPut]
        [System.Web.Http.Authorize(Roles = "Admin")]
        public IHttpActionResult Put([FromBody]UserDTO user)
        {
            try
            {
                //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId<long>());
                User typicalUser = EFServiceLocator.GetService<User>();
                logic.FromDTOtoBaseClass(user, typicalUser, true);
                logic.EditInPost(typicalUser, userRepository);
                return Ok();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        // DELETE: api/User/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                logic.ConfirmDelete(userRepository, id);
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
