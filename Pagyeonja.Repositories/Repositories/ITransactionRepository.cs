using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<Transaction> GetTransaction(Guid id);
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<bool> TransactionExists(Guid id);
        Task<bool> DeleteTransaction(Guid id);
    }
}