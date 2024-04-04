using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories;

namespace Pagyeonja.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _transactionRepository.GetTransactions();
        }
    }
}