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
        private readonly IRideHistoryRepository _rideHistoryRepository;

        private readonly HitchContext _context;
        public TransactionService(ITransactionRepository transactionRepository, HitchContext context, IRideHistoryRepository rideHistoryRepository)
        {
            _transactionRepository = transactionRepository;
            _context = context;
            _rideHistoryRepository = rideHistoryRepository;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _transactionRepository.GetTransactions();
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            await _transactionRepository.AddTransaction(transaction);

            //add the data to be added to the ride history
            RideHistory rideHistory = new RideHistory
            {
                TransactionId = transaction.TransactionId,
                RiderId = transaction.RiderId,
                CommuterId = transaction.CommuterId
            };

            //call the ridehistory to be added
            await _rideHistoryRepository.AddRideHistory(rideHistory);

            return transaction;
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

        public async Task<Transaction> UpdateTransaction(Transaction transaction)
        {
            return await _transactionRepository.UpdateTransaction(transaction);
        }
    }
}