using Portal.Domain.Enumerations;

namespace Portal.Domain.AggregatesModel.Auth
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserTypeEnum Type { get; set; }

        public User() : base()
        {
        }
    }
}