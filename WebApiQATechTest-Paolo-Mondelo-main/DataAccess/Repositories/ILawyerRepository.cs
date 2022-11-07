using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataModel;

namespace DataAccess.Repositories
{
    public interface ILawyerRepository
    {
        public Task<DbLawyer> CreateLawyerAsync(DbLawyer newLawyer);
        public Task<DbLawyer> GetLawyerAsync(Guid id);
        public Task<IReadOnlyList<DbLawyer>> GetLawyersAsync(int skip, int take);
    }
}
