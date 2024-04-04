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

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            return await _transactionRepository.AddTransaction(transaction);
        }

        public async Task<bool> TransactionExists(Guid id)
        {
            return await _transactionRepository.TransactionExists(id);
        }

        public async Task<Transaction> GetTransaction(Guid id)
        {
            return await _transactionRepository.GetTransaction(id);
        }
    }
}