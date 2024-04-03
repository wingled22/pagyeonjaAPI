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
    
    public class UpdateSuspensionRepository : IUpdateSuspensionRepository
    {
        private readonly HitchContext _context;
        public UpdateSuspensionRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task UpdateSuspensionDue(HitchContext context)
        {
            
        }
    }
}