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
    [Route("api/Comment")]
    public class CommentController : Controller
    {
		IDataLayer dataLayer;

		public CommentController(IDataLayer dataLayer)
		{
			this.dataLayer = dataLayer;
		}

        // GET: api/Comment
        [HttpGet]
        public async Task<IList<Comment>> Get()
        {
			return await dataLayer.CommentReadAll();
        }

		// GET: api/Comment/5
		//[HttpGet("{id}", Name = "Get")]
		[HttpGet("{id}")]
        public async Task<IList<Comment>> Get(int id)
        {
			return await dataLayer.CommentRead(id);
        }
        
        // POST: api/Comment
        [HttpPost]
        public async Task<int> Post([FromBody]int topicId, [FromBody]int threadId, [FromBody]int userId, [FromBody]string comment, [FromBody]int? replyToCommentId)
        {
			return await dataLayer.CommentCreate(topicId, threadId, userId, comment, replyToCommentId);
        }
        
        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]int topicId, [FromBody]int threadId, [FromBody]int userId, [FromBody]string comment, [FromBody]int? replyToCommentId)
        {
			return await dataLayer.CommentUpdate(id, topicId, threadId, userId, comment, replyToCommentId);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
			dataLayer.CommentDelete(id);
        }
    }
}
