using CrsSoftBlogProject.Models.Domain;
using System.Collections.Generic;

namespace CrsSoftBlogProject.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<BlogPostDomain> BlogPosts { get; set; }
        public List<TagDomain> Tags { get; set; }
    }
}


