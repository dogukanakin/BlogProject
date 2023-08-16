namespace CrsSoftBlogProject.Models.Domain
{
    public class ContactFormDomain
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
    }
}
