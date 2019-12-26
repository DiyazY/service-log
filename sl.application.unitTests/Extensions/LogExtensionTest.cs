using sl.application.Extensions;
using sl.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace sl.application.unitTests.Extensions
{
    public class LogExtensionTest
    {
        [Fact]
        public void Test_ToDetailedViewModel()
        {
            // arrange
            var id = Guid.NewGuid();
            var systemId = $"{Guid.NewGuid()}";//some unique value
            var stackTrace = "ipsum lorem";
            var message = "IO exception";
            var logLevel = LogLevel.Error;
            var labels = new string[] { "CustomClass", "v12.0" };

            var log = Log.CreateLog(id, message, systemId, logLevel, stackTrace, labels);

            // act
            var viewModel = log.ToDetailedViewModel();

            // assert
            Assert.NotNull(viewModel);
            Assert.Equal(viewModel.Id, log.Id);
            Assert.Equal(viewModel.Labels, log.Labels.Select(p=>p.Value));
            Assert.Equal(viewModel.Level, log.Level.ToString());
            Assert.Equal(viewModel.SystemId, log.SystemId);
            Assert.Equal(viewModel.StackTrace, log.StackTrace);
            Assert.Equal(viewModel.RegisteredAt, log.RegisteredAt);
        }
    }
}
