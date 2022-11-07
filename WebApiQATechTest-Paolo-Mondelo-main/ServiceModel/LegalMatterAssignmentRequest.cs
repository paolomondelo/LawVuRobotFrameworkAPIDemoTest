using System;
using System.Collections.Generic;

namespace ServiceModel
{
    public class LegalMatterAssignmentRequest
    {
        public ICollection<Guid> Ids { get; set; }
        public Guid LawyerId { get; set; }
    }
}
