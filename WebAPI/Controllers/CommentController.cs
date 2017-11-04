using WebAPI.Filters;
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
	[AuthenticatedUser]
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
        public async Task<int> Post([FromBody]Comment comment)
        {
			return await dataLayer.CommentCreate(comment.TopicId, comment.ThreadId, comment.UserId, comment.CommentStr, comment.ReplyToCommentId);
        }
        
        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]Comment comment)
        {
			return await dataLayer.CommentUpdate(id, comment.Version, comment.TopicId, comment.ThreadId, comment.UserId, comment.CommentStr, comment.ReplyToCommentId);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
			dataLayer.CommentDelete(id);
        }
    }
}
