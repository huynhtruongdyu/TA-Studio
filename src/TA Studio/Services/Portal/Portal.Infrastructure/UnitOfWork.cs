using Microsoft.EntityFrameworkCore;
using Portal.Infrastructure.Repositories.Auth;
using System;
using System.Threading.Tasks;

namespace Portal.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        Task SaveChangesAsync();

        void SaveChanges();

        #region Repositories
        public IUserRepository UserRepository { get; }
        #endregion
    }

    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        #region Repositories
        public IUserRepository UserRepository { get; }
        #endregion

        public UnitOfWork(
            PortalContext portalContext,
            IUserRepository userRepository)
        {
            Context = portalContext;

            #region Repositories
            this.UserRepository = userRepository;
            #endregion
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

    }
}