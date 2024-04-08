using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Repositories.Repositories
{

    public class TopupHistoryRepository : ITopupHistoryRepository
    {
        private readonly HitchContext _context;

        public TopupHistoryRepository(HitchContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<RideHistory>> GetTopupHistories()
        {
            throw new NotImplementedException();
        }
    }
}