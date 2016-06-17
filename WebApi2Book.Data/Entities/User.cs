
namespace WebApi2Book.Data.Entities
{
    public class User
    {
        public virtual long UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserName { get; set; }
        public virtual byte[] Version { get; set; }
    }
}
