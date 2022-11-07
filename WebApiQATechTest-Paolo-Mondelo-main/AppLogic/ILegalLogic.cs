using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceModel;

namespace AppLogic
{
    public interface ILegalLogic
    {
        Task<IEnumerable<LegalMatter>> AssignMattersAsync(IEnumerable<Guid> ids, Guid lawyerId);
        public Task<Lawyer> CreateLawyerAsync(Lawyer legalMatter);
        public Task<LegalMatter> CreateMatterAsync(LegalMatter legalMatter);
        public Task<Lawyer> GetLawyerAsync(Guid id);
        Task<IEnumerable<Lawyer>> GetLawyersAsync(int skip, int take);
        public Task<LegalMatter> GetMatterAsync(Guid id);
        Task<IEnumerable<LegalMatter>> GetMattersAsync(int skip, int take);
    }
}