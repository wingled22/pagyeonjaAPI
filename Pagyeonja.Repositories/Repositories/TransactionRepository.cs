using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HitchContext _context;
        public TransactionRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            do
            {
                transaction.TransactionId = Guid.NewGuid();
            } while (await TransactionExists(transaction.TransactionId));

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> GetTransaction(Guid id)
        {
            return await _context.Transactions.Where(t => t.TransactionId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _context.Transactions.OrderByDescending(t => t.TransactionId).ToListAsync();
        }

        public async Task<bool> TransactionExists(Guid id)
        {
            return await _context.Transactions.AnyAsync(t => t.TransactionId == id);
        }
    }
}