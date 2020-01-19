using MongoDB.Driver;
using MongoDBAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBAccess.Services
{
    public class BlogService :ServiceBase, IService<Blog>
    {
        private IMongoCollection<Blog> _blogs;
        public BlogService()
        {
            _blogs = Database.GetCollection<Blog>("Blogs");
        }

        public Blog Add(Blog blog)
        {
            _blogs.InsertOne(blog);
            return blog;

        }

        public void Delete(string Id)
        {
            _blogs.DeleteOne(x => x.Id == Id);
        }

        public Blog Get(string Id)
        {
            return _blogs.Find<Blog>(x => x.Id == Id).FirstOrDefault();
        }

        public List<Blog> GetAll()
        {
            return _blogs.Find<Blog>(x=> true).ToList();
        }

        public void Update(string Id, Blog blog)
        {
            blog.Id = Id;
            _blogs.ReplaceOne(x => x.Id == Id, blog);
        }
    }
}
