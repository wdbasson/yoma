namespace Yoma.Core.Domain.Core.Interfaces
{
    public interface IRepositoryBatchedWithValueContains<T> : IRepositoryBatched<T>, IRepositoryValueContains<T>
        where T : class
    {
    }
}
