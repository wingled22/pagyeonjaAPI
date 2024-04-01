using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class DatabaseTransactionRepository : IDatabaseTransactionRepository
    {
        private readonly HitchContext _context;
        public DatabaseTransactionRepository(HitchContext context)
        {
            _context = context;
        }
        public async Task RevertTransaction(IDbContextTransaction dbContextTransaction)
        {
            try
            {
                await dbContextTransaction.RollbackAsync();
                return;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task SaveTransaction(IDbContextTransaction dbContextTransaction)
        {
            try
            {
                await dbContextTransaction.CommitAsync();
                return;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<IDbContextTransaction> StartTransaction()
        {
            try
            {
                return await _context.Database.BeginTransactionAsync();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}