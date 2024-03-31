using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Pagyeonja.Repositories.Repositories
{
    public interface IDatabaseTransactionRepository
    {
        Task<IDbContextTransaction> StartTransaction();
        Task RevertTransaction(IDbContextTransaction dbContextTransaction);
    }
}