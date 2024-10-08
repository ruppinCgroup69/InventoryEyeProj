﻿using InventoryEyeBack.Users;
using Microsoft.AspNetCore.Mvc;


namespace InventoryEyeBack.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet("{userId}")]
        public IEnumerable<PostsModel> Get(int userId)
        {
            PostsModel post = new PostsModel();
            var posts = post.ReadAllPosts(userId);

            return posts;
        }


        // GET: Read Posts by postId api/<PostsController>/5
        [HttpGet("PostId/{id}")]
        public PostsModel GetByPostId(int id)
        {
            PostsModel post = new PostsModel();
            return post.ReadPostByPostId(id);
        }


        // GET: Read Posts by category api/<PostsController>/5
        [HttpGet("Category/{category}")]
        public List<PostsModel> GetByCategory(int category)
        {
            PostsModel post = new PostsModel();
            return post.ReadPostByCategory(category);
        }

        // GET: Read Posts by userId api/<PostsController>/5
        [HttpGet("UserId/{userId}")]
        public List<PostsModel> GetByUserId(int userId)
        {
            PostsModel post = new PostsModel();
            return post.ReadPostByUserId(userId);
        }

    
        // GET: Search Posts by Search api/<PostsController>/5
        [HttpGet("Search/{search}")]
        public List<PostsModel> GetByProduct(string search)
        {
            PostsModel post = new PostsModel();
            return post.SearchPostsBySearch(search);
        }

        // POST api/<PostsController>
        [HttpPost]
        public IActionResult Post([FromBody] PostsModel post)
        {
            int status = post.InsertPost(post);
            if (status == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PostsModel post)
        {
            int status = post.UpdatePost(post);
            if (status == 1)
            {
                return Ok(post);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PostsModel post = new PostsModel();
            post.DeletePost(id);
            return Ok();
        }
    }
}
