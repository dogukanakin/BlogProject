namespace CrsSoftBlogProject.Models.ViewModels

{
    public class EditUserViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string CurrentRole { get; set; }
        public string NewRole { get; set; }

    }

}
