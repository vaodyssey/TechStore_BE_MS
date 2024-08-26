namespace TechStore.User.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Models.User> UserRepository { get; }
        void Save();
    }

}
