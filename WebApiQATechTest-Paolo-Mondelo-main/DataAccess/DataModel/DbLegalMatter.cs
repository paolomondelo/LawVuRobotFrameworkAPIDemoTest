using System;

namespace DataAccess.DataModel
{
    public class DbLegalMatter
    {
        public Guid Id { get; set; }
        public string MatterName { get; set; }
        public Guid? LawyerId { get; set; }

        public DbLawyer Lawyer { get; set; }

        public DbLegalMatter() { }

        public DbLegalMatter(Guid id, string matterName, Guid? lawyerId)
        {
            Id = id;
            MatterName = matterName;
            LawyerId = lawyerId;
        }
    }
}
