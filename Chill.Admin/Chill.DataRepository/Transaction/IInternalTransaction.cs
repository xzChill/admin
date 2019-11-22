using System.Data;

namespace Chill.DataRepository
{
    internal interface IInternalTransaction : ITransaction
    {
        void BeginTransaction(IsolationLevel isolationLevel);

        void CommitTransaction();

        void RollbackTransaction();
    }
}
