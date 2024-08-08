using Xunit;
using Moq;
using GlassLewis.Company.Api.Repository;
using GlassLewis.Company.Api.Repository.Context;
using GlassLewis.Company.Api.Repository.Entities;
using GlassLewis.Company.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace GlassLewis.Company.Api.UnitTest.Repository
{
    public class CompanyRepositoryTests
    {
        private readonly CompanyRepository _companyRepository;
        private readonly Mock<CompanyContext> _mockContext;
        private readonly Mock<DbSet<Companies>> _mockSet;
        private readonly Mock<ILogger<CompanyRepository>> _mockLogger;

        public CompanyRepositoryTests()
        {
            _mockContext = new Mock<CompanyContext>();
            _mockSet = new Mock<DbSet<Companies>>();
            _mockLogger = new Mock<ILogger<CompanyRepository>>();
            _companyRepository = new CompanyRepository(_mockContext.Object, _mockLogger.Object);
        }

    }
}
