using TechStore.Products.Migrations;
using TechStore.Products.Models;

namespace TechStore.Products.Repositories
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ProductsDbContext? _dbContext;
        private bool disposed = false;
        private IGenericRepository<Product> _ProductRepository { get; set; }
        private IGenericRepository<Brand> _BrandRepository { get; set; }
        public UnitOfWork(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                return _ProductRepository ??= new GenericRepository<Product>(_dbContext!);
            }
        }
        public IGenericRepository<Brand> BrandRepository
        {
            get
            {
                return _BrandRepository ??= new GenericRepository<Brand>(_dbContext!);
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
