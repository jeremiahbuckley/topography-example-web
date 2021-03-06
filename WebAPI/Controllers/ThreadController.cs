﻿using WebAPI.Filters;
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
    [Route("api/Thread")]
	[AuthenticatedUser]
    public class ThreadController : Controller
    {
		IDataLayer dataLayer;

		public ThreadController(IDataLayer dataLayer)
		{
			this.dataLayer = dataLayer;
		}

        // GET: api/Thread
        [HttpGet]
        public async Task<IList<Thread>> Get()
        {
			return await dataLayer.ThreadReadAll();
        }

		// GET: api/Thread/5
		//[HttpGet("{id}", Name = "Get")]
		[HttpGet("{id}")]
        public async Task<IList<Thread>> Get(int id)
        {
			return await dataLayer.ThreadRead(id);
        }
        
        // POST: api/Thread
        [HttpPost]
        public async Task<int> Post([FromBody]Thread thread)
		{
			return await dataLayer.ThreadCreate(thread.Name, thread.TopicId, thread.Enabled, thread.Pinned, thread.PinOrder);
        }
        
        // PUT: api/Thread/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]Thread thread)
		{
			return await dataLayer.ThreadUpdate(id, thread.Version, thread.Name, thread.TopicId, thread.Enabled, thread.Pinned, thread.PinOrder);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
			dataLayer.ThreadDelete(id);
        }
    }
}
