using System;
using System.Collections.Generic;

namespace DataAccess.DataModel
{
    public class DbLawyer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public ICollection<DbLegalMatter> LegalMatters { get; set; }

        public DbLawyer() { }

        public DbLawyer(Guid id, string firstName, string lastName, string companyName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
        }
    };
}
