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

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _context.Transactions.OrderByDescending(a => a.TransactionId).ToListAsync();
        }
    }
}