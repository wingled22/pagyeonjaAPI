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
    }
}