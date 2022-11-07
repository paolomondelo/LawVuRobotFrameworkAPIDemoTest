using DataAccess.DataModel;
using DataAccess.Repositories;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLogic.Tests
{
    [TestFixture]
    public class LegalLogicTests
    {
        private Mock<ILawyerRepository> mockLawyerRepository;
        private Mock<ILegalRepository> mockLegalRepository;
        private ILegalLogic legalLogic;
        private IObjectMapper mapper = new ObjectMapper();

        [SetUp]
        public void SetUp()
        {
            mockLawyerRepository = new Mock<ILawyerRepository>(MockBehavior.Strict);
            mockLegalRepository = new Mock<ILegalRepository>(MockBehavior.Strict);
            mapper = new ObjectMapper();
            legalLogic = new LegalLogic(mockLawyerRepository.Object, mockLegalRepository.Object, mapper);
        }

        [Test]
        public async Task AssignMatters_InvalidLawyer()
        {
            var lawyerId = Guid.NewGuid();
            mockLawyerRepository.Setup(m => m.GetLawyerAsync(lawyerId)).ReturnsAsync((DbLawyer)null);
            var result = await legalLogic.AssignMattersAsync(new List<Guid>(), lawyerId);
            result.ShouldBeNull();
        }

        [Test]
        public async Task AssignMatters_ValidLawyer()
        {
            var lawyerId = Guid.NewGuid();
            var lawyer = new DbLawyer { Id = lawyerId };
            mockLawyerRepository.Setup(m => m.GetLawyerAsync(lawyerId)).ReturnsAsync(lawyer);
            var legalMatterId = Guid.NewGuid();
            mockLegalRepository.Setup(m => m.AssignLawyerAsync(It.Is<IEnumerable<Guid>>((IEnumerable<Guid> l) => l.Any(l => l == legalMatterId)), lawyerId))
                .ReturnsAsync(new List<DbLegalMatter> { new DbLegalMatter { LawyerId = lawyerId, Lawyer = lawyer } });
            var result = await legalLogic.AssignMattersAsync(new List<Guid> { legalMatterId }, lawyerId);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(1);
            result.First().LawyerId.ShouldBe(lawyerId);
            mockLawyerRepository.VerifyAll();
            mockLegalRepository.VerifyAll();
        }

        // Etc...
    }
}
