namespace BlogMainStructure.Infrastructure.DataAccess.Interfaces
{
    // Defines a basic repository with synchronous operations.
    public interface IRepository
    {
        int SaveChanges();
    }
}
