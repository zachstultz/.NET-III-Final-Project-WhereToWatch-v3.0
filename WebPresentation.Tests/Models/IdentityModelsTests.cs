namespace WebPresentation.Tests.Models
{
    using WebPresentation.Models;
    using System;
    using NUnit.Framework;
    using NSubstitute;
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;

    [TestFixture]
    public class ApplicationUserTests
    {
        private ApplicationUser _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ApplicationUser();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ApplicationUser();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public async Task CanCallGenerateUserIdentityAsync()
        {
            var manager = new UserManager<ApplicationUser>(Substitute.For<IUserStore<ApplicationUser>>());
            var result = await _testClass.GenerateUserIdentityAsync(manager);
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallGenerateUserIdentityAsyncWithNullManager()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.GenerateUserIdentityAsync(default(UserManager<ApplicationUser>)));
        }
    }

    [TestFixture]
    public class ApplicationDbContextTests
    {
        private ApplicationDbContext _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ApplicationDbContext();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ApplicationDbContext();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanCallCreate()
        {
            var result = ApplicationDbContext.Create();
            Assert.Fail("Create or modify test");
        }
    }
}