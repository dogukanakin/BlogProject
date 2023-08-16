namespace CrsSoftBlogProject.Models.ViewModels
{
    public class AddLikeRequestViewModel
    {
        public Guid BlogPostId { get; set; }
        public Guid UsersId { get; set; }
    }
}
