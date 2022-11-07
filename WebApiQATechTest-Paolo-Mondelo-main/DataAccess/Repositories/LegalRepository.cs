using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class LegalRepository : ILegalRepository
    {
        private readonly TechTestDbContext techTestDbContext;

        public LegalRepository(TechTestDbContext techTestDbContext)
        {
            this.techTestDbContext = techTestDbContext;
        }

        public async Task<IReadOnlyList<DbLegalMatter>> AssignLawyerAsync(IEnumerable<Guid> ids, Guid lawyerId)
        {
            using var transaction = techTestDbContext.Database.BeginTransaction();
            try
            {
                var lawyer = await techTestDbContext.Lawyer.SingleAsync(d => d.Id == lawyerId);
                var legalMatters = await techTestDbContext.Matter.Where(m => ids.Contains(m.Id)).ToListAsync();
                foreach (var legalMatter in legalMatters)
                {
                    legalMatter.Lawyer = lawyer;
                }
                await techTestDbContext.SaveChangesAsync();
                transaction.Commit();
                return legalMatters;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<DbLegalMatter> CreateLegalMatterAsync(DbLegalMatter newMatter)
        {
            var id = await techTestDbContext.AddAsync(newMatter).ConfigureAwait(false);
            await techTestDbContext.SaveChangesAsync();

            return id.Entity;
        }

        public async Task<IReadOnlyList<DbLegalMatter>> GetLegalMattersAsync(int skip = 0, int take = 100) => 
            await techTestDbContext.Matter
                .Include(i => i.Lawyer)
                .OrderBy(d => d.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

        public Task<DbLegalMatter> GetLegalMatterAsync(Guid id) => techTestDbContext.Matter
            .Include(i => i.Lawyer)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
