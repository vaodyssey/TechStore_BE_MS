using TechStore.User.Migrations;

namespace TechStore.User.Repositories
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UserDbContext? _dbContext;
        private bool disposed = false;
        private IGenericRepository<Models.User> _UserRepository { get; set; }
        private IGenericRepository<Models.Order> _OrderRepository { get; set; }
        private IGenericRepository<Models.OrderDetail> _OrderDetailRepository { get; set; }

        public UnitOfWork(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<Models.User> UserRepository
        {
            get
            {
                return _UserRepository ??= new GenericRepository<Models.User>(_dbContext!);
            }
        }
        public IGenericRepository<Models.Order> OrderRepository
        {
            get
            {
                return _OrderRepository ??= new GenericRepository<Models.Order>(_dbContext!);
            }
        }
        public IGenericRepository<Models.OrderDetail> OrderDetailRepository
        {
            get
            {
                return _OrderDetailRepository ??= new GenericRepository<Models.OrderDetail>(_dbContext!);
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
