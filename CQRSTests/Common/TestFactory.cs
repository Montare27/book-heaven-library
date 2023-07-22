namespace BusinessTests.Common
{
    using AutoMapper;
    using business.Common.AutoMapper;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using persistence;

    public class TestFactory : IDisposable
    {
        public readonly static Guid Id = Guid.NewGuid();
        public readonly static DateTime DateTime = DateTime.Now;
        
        protected readonly Mock<ICurrentUserService> UserService = new Mock<ICurrentUserService>();

        protected readonly IMapper Mapper;
        protected readonly BookDbContext Context;
        
        public TestFactory()
        {
            Mapper = new Mapper(new MapperConfiguration(expression => 
                expression.AddProfile(new AutoMapperProfiles())));
            
            UserService.Setup(x => x.Id).Returns(Id);
            DbContextOptions<BookDbContext> options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            Context = new BookDbContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
