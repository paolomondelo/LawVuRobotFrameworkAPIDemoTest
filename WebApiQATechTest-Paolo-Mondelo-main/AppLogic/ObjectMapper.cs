using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DataModel;
using ServiceModel;

namespace AppLogic
{
    public class ObjectMapper : IObjectMapper
    {
        public TDest Map<TSource, TDest>(TSource item)
        {
            object result = item switch
            {
                null => null,
                Lawyer lawyer => Map(lawyer),
                LegalMatter matter => Map(matter),
                DbLawyer dbLawyer => Map(dbLawyer),
                DbLegalMatter dbMatter => Map(dbMatter),
                _ => throw new NotSupportedException()
            };

            return (TDest) result;
        }

        public IEnumerable<TDest> Map<TSource, TDest>(IEnumerable<TSource> sourceCollection) => sourceCollection?.Select(Map<TSource, TDest>);
        private DbLawyer Map(Lawyer lawyer) => new(lawyer.Id, lawyer.FirstName, lawyer.LastName, lawyer.CompanyName);
        private DbLegalMatter Map(LegalMatter matter) => new (matter.Id, matter.MatterName, matter.LawyerId);
        private Lawyer Map(DbLawyer lawyer) => new(lawyer.Id, lawyer.FirstName, lawyer.LastName, lawyer.CompanyName);
        private LegalMatter Map(DbLegalMatter matter) => new(matter.Id, matter.MatterName, matter.LawyerId, matter.Lawyer?.CompanyName);
    }
}
