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
    [Route("api/Topic")]
    public class TopicController : Controller
    {
		private IDataLayer dataLayer;
		public TopicController(IDataLayer dataLayer)
		{
			this.dataLayer = dataLayer;
		}

        // GET: api/Topic
        [HttpGet]
        public async Task<IList<Topic>> Get()
        {
			return await dataLayer.TopicReadAll();
        }

		// GET: api/Topic/5
		//[HttpGet("{id}", Name = "Get")]
		[HttpGet("{id}")]
        public async Task<IList<Topic>> Get(int id)
        {
            return await dataLayer.TopicRead(id);
        }
        
        // POST: api/Topic
        [HttpPost]
        public async Task<int> Post([FromBody]string name, [FromBody]bool enabled)
        {
			return await dataLayer.TopicCreate(name, enabled);
        }
        
        // PUT: api/Topic/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]string name, [FromBody]bool enabled)
        {
			return await dataLayer.TopicUpdate(id, name, enabled);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
			dataLayer.TopicDelete(id);
        }
    }
}
