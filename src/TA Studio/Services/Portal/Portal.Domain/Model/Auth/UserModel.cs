using Portal.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Model.Auth
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserTypeEnum Type { get; set; }
    }

    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserTypeEnum Type { get; set; }
    }
}
