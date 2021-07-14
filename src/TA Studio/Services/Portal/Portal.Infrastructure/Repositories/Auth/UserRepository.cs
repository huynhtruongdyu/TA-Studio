using Portal.Domain.AggregatesModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.Auth
{
    public interface IUserRepository: IRepository<User>
    {

    }
    public class UserRepository:EfRepository<User>, IUserRepository
    {

        public UserRepository(PortalContext context):base(context)
        {

        }
    }
}
