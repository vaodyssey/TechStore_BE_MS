namespace TechStore.User.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Models.User> UserRepository { get; }
        IGenericRepository<Models.Order> OrderRepository { get; }
        IGenericRepository<Models.OrderDetail> OrderDetailRepository { get; }
        void Save();
    }

}
