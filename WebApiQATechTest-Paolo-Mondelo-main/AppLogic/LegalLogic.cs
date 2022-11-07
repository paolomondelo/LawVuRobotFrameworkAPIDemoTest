using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModel;
using DataAccess.Repositories;
using ServiceModel;

namespace AppLogic
{
    public class LegalLogic : ILegalLogic
    {
        private readonly ILawyerRepository lawyerRepo;
        private readonly ILegalRepository legalRepo;
        private readonly IObjectMapper mapper;

        public LegalLogic(ILawyerRepository lawyerRepo, ILegalRepository legalRepo, IObjectMapper mapper)
        {
            this.lawyerRepo = lawyerRepo;
            this.legalRepo = legalRepo;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<LegalMatter>> AssignMattersAsync(IEnumerable<Guid> ids, Guid lawyerId)
        {
            // Fail for an unknown lawyer id
            var lawyer = await lawyerRepo.GetLawyerAsync(lawyerId);
            if (lawyer == null)
            {
                return null;
            }
            var legalMatters = await legalRepo.AssignLawyerAsync(ids, lawyerId);
            return legalMatters.Select(m => mapper.Map<DbLegalMatter, LegalMatter>(m));
        }

        public async Task<Lawyer> CreateLawyerAsync(Lawyer lawyer)
        {
            var dbObject = mapper.Map<Lawyer, DbLawyer>(lawyer);
            var result = await lawyerRepo.CreateLawyerAsync(dbObject);
            return mapper.Map<DbLawyer, Lawyer>(result);
        }

        public async Task<LegalMatter> CreateMatterAsync(LegalMatter legalMatter)
        {
            var dbObject = mapper.Map<LegalMatter, DbLegalMatter>(legalMatter);
            var result = await legalRepo.CreateLegalMatterAsync(dbObject).ConfigureAwait(false);
            
            return mapper.Map<DbLegalMatter, LegalMatter>(result);
        }

        public async Task<Lawyer> GetLawyerAsync(Guid id)
        {
            var result = await lawyerRepo.GetLawyerAsync(id);
            return mapper.Map<DbLawyer, Lawyer>(result);
        }

        public async Task<IEnumerable<Lawyer>> GetLawyersAsync(int skip = 0, int take = 100)
        {
            var result = await lawyerRepo.GetLawyersAsync(skip, take);
            return mapper.Map<DbLawyer, Lawyer>(result);
        }

        public async Task<LegalMatter> GetMatterAsync(Guid id)
        {
            var result = await legalRepo.GetLegalMatterAsync(id).ConfigureAwait(false);
            return mapper.Map<DbLegalMatter, LegalMatter>(result);
        }

        public async Task<IEnumerable<LegalMatter>> GetMattersAsync(int skip = 0, int take = 100)
        {
            var result = await legalRepo.GetLegalMattersAsync(skip, take).ConfigureAwait(false);
            return mapper.Map<DbLegalMatter, LegalMatter>(result);
        }
    }
}
