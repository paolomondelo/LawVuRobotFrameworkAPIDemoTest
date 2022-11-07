using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class LawyerRepository : ILawyerRepository
    {
        private readonly TechTestDbContext techTestDbContext;

        public LawyerRepository(TechTestDbContext techTestDbContext)
        {
            this.techTestDbContext = techTestDbContext;
        }

        public async Task<DbLawyer> CreateLawyerAsync(DbLawyer newLawyer)
        {
            var id = await techTestDbContext.AddAsync(newLawyer);
            await techTestDbContext.SaveChangesAsync();
            return id.Entity;
        }

        public async Task<DbLawyer> GetLawyerAsync(Guid id) => await techTestDbContext.Lawyer.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<DbLawyer>> GetLawyersAsync(int skip, int take) =>
            await techTestDbContext.Lawyer
                .OrderBy(d => d.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
    }
}
