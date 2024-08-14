
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Auth.Models;

namespace TechStore.Auth.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository{ get; }        
        void Save();
    }

}
