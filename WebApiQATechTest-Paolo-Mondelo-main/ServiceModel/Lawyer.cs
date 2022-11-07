using System;

namespace ServiceModel
{
    public record Lawyer
    (
        Guid Id,
        string FirstName,
        string LastName,
        string CompanyName
    );
}
