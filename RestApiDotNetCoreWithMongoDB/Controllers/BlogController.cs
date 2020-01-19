using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBAccess.Models;
using MongoDBAccess.Services;

namespace RestApiDotNetCoreWithMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("AuthPolicy")]
    public class BlogController : ControllerBase
    {
        private BlogService _blogService;
        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("list")]
        [HttpGet]
        public List<Blog> GetAll()
        {
            return _blogService.GetAll();
        }

        [Route("add")]
        [HttpPost]
        public Blog Post([FromBody] Blog blog)
        {
            return _blogService.Add(blog);
        }

        [Route("delete")]
        [HttpDelete]
        public void Delete(string Id)
        {
            _blogService.Delete(Id);
        }

        [Route("update")]
        [HttpPut]
        public void Update(string Id, Blog blog)
        {
            _blogService.Update(Id,blog);
        }
    }
}