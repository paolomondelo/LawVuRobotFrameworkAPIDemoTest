using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataModel;

namespace DataAccess.Repositories
{
    public interface ILegalRepository
    {
        public Task<IReadOnlyList<DbLegalMatter>> AssignLawyerAsync(IEnumerable<Guid> ids, Guid lawyerId);
        public Task<DbLegalMatter> CreateLegalMatterAsync(DbLegalMatter newMatter);
        public Task<DbLegalMatter> GetLegalMatterAsync(Guid id);
        public Task<IReadOnlyList<DbLegalMatter>> GetLegalMattersAsync(int skip, int take);
    }
}