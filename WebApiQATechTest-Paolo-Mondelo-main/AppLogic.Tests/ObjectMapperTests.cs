using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DataModel;
using NUnit.Framework;
using ServiceModel;
using Shouldly;

namespace AppLogic.Tests
{
    [TestFixture]
    public class ObjectMapperTests
    {
        private readonly IObjectMapper mapper = new ObjectMapper();

        [Test]
        public void Map_unknown_throws() => Should.Throw<NotSupportedException>(() => mapper.Map<int, string>(2));

        [Test]
        public void Map_nullObject_returnsNull_doesNotThrow()
        {
            var result = Should.NotThrow(() => mapper.Map<LegalMatter, DbLegalMatter>((LegalMatter)null));
            result.ShouldBeNull();
        }

        [Test]
        public void Map_nullCollection_returnsNull_doesNotThrow()
        {
            var result = Should.NotThrow(() => mapper.Map<LegalMatter, DbLegalMatter>((IEnumerable<LegalMatter>)null));
            result.ShouldBeNull();
        }

        [Test]
        public void Map_Collection_ReturnsMappedLawyerCollection()
        {
            var values = new List<Lawyer>
            {
                new (Guid.NewGuid(), "first1", "last1", "company1"),
                new (Guid.NewGuid(), "first2", "last2", "company2"),
            };

            var result = mapper.Map<Lawyer, DbLawyer>(values);

            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(2);
            result.ShouldBeAssignableTo<IEnumerable<DbLawyer>>();
        }

        [Test]
        public void Map_Collection_ReturnsMappedLegalMatterCollection()
        {
            var values = new List<LegalMatter>
            {
                new (Guid.NewGuid(), "name1", Guid.NewGuid(), null),
                new (Guid.NewGuid(), "name2", null, null)
            };

            var result = mapper.Map<LegalMatter, DbLegalMatter>(values);

            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(2);
            result.ShouldBeAssignableTo<IEnumerable<DbLegalMatter>>();
        }

        [Test]
        public void Map_Lawyer_ReturnsCorrectlyMappedDbLawyer()
        {
            var source = new Lawyer(Guid.NewGuid(), "first1", "last1", "company1");
            var result = mapper.Map<Lawyer, DbLawyer>(source);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<DbLawyer>();
            result.Id.ShouldBe(source.Id);
            result.FirstName.ShouldBe(source.FirstName);
            result.LastName.ShouldBe(source.LastName);
            result.CompanyName.ShouldBe(source.CompanyName);
        }

        [Test]
        public void Map_LegalMatter_ReturnsCorrectlyMappedDbLegalMatter()
        {
            var source = new LegalMatter(Guid.NewGuid(), "name1", Guid.NewGuid(), null);
            var result = mapper.Map<LegalMatter, DbLegalMatter>(source);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<DbLegalMatter>();
            result.Id.ShouldBe(source.Id);
            result.MatterName.ShouldBe(source.MatterName);
            result.LawyerId.ShouldBe(source.LawyerId);
        }

        [Test]
        public void Map_DbLawyer_ReturnsCorrectlyMappedLawyer()
        {
            var source = new DbLawyer(Guid.NewGuid(), "first1", "last1", "company1");
            var result = mapper.Map<DbLawyer, Lawyer>(source);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<Lawyer>();
            result.Id.ShouldBe(source.Id);
            result.FirstName.ShouldBe(source.FirstName);
            result.LastName.ShouldBe(source.LastName);
            result.CompanyName.ShouldBe(source.CompanyName);
        }

        [TestCase(null, null)]
        [TestCase("3FA85F64-5717-4562-B3FC-2C963F66AFA6", "company1")]
        public void Map_DbLegalMatter_ReturnsCorrectlyMappedLegalMatter(string lawyerIdString, string lawyerCompanyName)
        {
            var lawyerId = lawyerIdString == null ? (Guid?)null : Guid.Parse(lawyerIdString);
            var source = new DbLegalMatter(Guid.NewGuid(), "name1", lawyerId)
            {
                Lawyer = lawyerId.HasValue
                    ? new DbLawyer { Id = lawyerId.Value, CompanyName = lawyerCompanyName }
                    : null
            };
            var result = mapper.Map<DbLegalMatter, LegalMatter>(source);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<LegalMatter>();
            result.Id.ShouldBe(source.Id);
            result.MatterName.ShouldBe(source.MatterName);
            result.LawyerId.ShouldBe(source.LawyerId);
            result.LawyerCompanyName.ShouldBe(source.Lawyer?.CompanyName);
        }
    }
}
