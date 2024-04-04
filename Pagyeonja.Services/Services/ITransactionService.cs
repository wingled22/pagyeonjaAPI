using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Services.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactions();
    }
}