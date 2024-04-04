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
        private readonly HitchContext _context;
        public TransactionService(ITransactionRepository transactionRepository, HitchContext context)
        {
            _transactionRepository = transactionRepository;
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _transactionRepository.GetTransactions();
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            return await _transactionRepository.AddTransaction(transaction);

            //add the data then to the ride history
            RideHistory rideHistory = new RideHistory();
        }

        public async Task<bool> TransactionExists(Guid id)
        {
            return await _transactionRepository.TransactionExists(id);
        }

        public async Task<Transaction> GetTransaction(Guid id)
        {
            return await _transactionRepository.GetTransaction(id);
        }

        public async Task<bool> DeleteTransaction(Guid id)
        {
            return await _transactionRepository.DeleteTransaction(id);
        }
    }
}