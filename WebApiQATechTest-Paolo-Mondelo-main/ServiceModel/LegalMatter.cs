using System;

namespace ServiceModel
{
    public record LegalMatter(Guid Id, string MatterName, Guid? LawyerId, string LawyerCompanyName);
}
