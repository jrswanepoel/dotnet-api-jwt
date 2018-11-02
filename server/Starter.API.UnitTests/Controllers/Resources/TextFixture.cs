using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

using Starter.Data.Context;

namespace Starter.API.UnitTests.Controllers.Resources
{
    public class TestFixture : IDisposable
    {
        public DataContext Context { get; }
        public List<object> LogList { get; }

        public TestFixture()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UnitTest_MetricDatabase").Options;
            //Using In Memory Db instead of mocks or stubs, because it's much easier to set up
            Context = new DataContext(options);
            LogList = new List<object>();

            SeedData();
        }

        public ILogger<T> CreateMockLogger<T>()
        {
            var mock = new Mock<ILogger<T>>();

            mock.Setup(m => m.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true);

            mock.Setup(m => m.BeginScope(It.IsAny<object>()))
                .Returns<IDisposable>(null);

            //todo: Implement Log List further for fun

            return mock.Object;
        }

        private void SeedData()
        {

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
