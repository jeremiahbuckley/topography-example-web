using WebAPI.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
		private IDataLayer dataLayer;

		public UserController(IDataLayer dataLayer)
		{
			this.dataLayer = dataLayer;
		}
        // GET: api/User
        [HttpGet]
        public async Task<IList<User>> Get()
        {
			return await dataLayer.UserReadAll();
        }

        // GET: api/User/5
        //[HttpGet("{id}", Name = "Get")]
		[HttpGet("{id}")]
		public async Task<IList<User>> Get(int id)
        {
			return await dataLayer.UserRead(id);
        }
        
        // POST: api/User
        [HttpPost]
        public async Task<int> Post([FromBody]string name, [FromBody]bool enabled)
        {
			return await dataLayer.UserCreate(name, enabled);
		}
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]string name, [FromBody]bool enabled)
		{
			return await dataLayer.UserUpdate(id, name, enabled);
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
        public void Delete(int id)
        {
			dataLayer.UserDelete(id);
        }
    }
}
