using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using EF.Core;
using EF.Core.Data;
using EF.Web.SLocator;
using EF.Web.BusinessLogic;


namespace EF.WebApi.Controllers
{
    [Authorize]
    [EnableQuery]
    public class ValuesController : ApiController
    {

        private IUnitOfWork unitOfWork;
        private IRepository<Transactions> transactionsRepository;
        private IUserManager manager;
        private IBusinessLogic logic;

        public ValuesController()
        {
            unitOfWork = EFServiceLocator.GetService<IUnitOfWork>();
            transactionsRepository = unitOfWork.Repository<Transactions>();
            manager = EFServiceLocator.GetService<IUserManager>();
            logic = EFServiceLocator.GetService<IBusinessLogic>();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}
