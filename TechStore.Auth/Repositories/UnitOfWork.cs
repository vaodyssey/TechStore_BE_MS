using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Auth.Migrations;
using TechStore.Auth.Models;

namespace TechStore.Auth.Repositories
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AuthDbContext? _dbContext;
        private bool disposed = false;
        private IGenericRepository<User> _UserRepository { get; set; }        
        public UnitOfWork(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<User> UserRepository
        {
            get
            {
                return _UserRepository ??= new GenericRepository<User>(_dbContext!);
            }
        }       


        public void Save()
        {
            _dbContext!.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext!.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
