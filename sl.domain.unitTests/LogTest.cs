using sl.domain.Exceptions;
using sl.domain.Models;
using System;
using Xunit;

namespace sl.domain.unitTests
{
    public class LogTest
    {
        [Fact]
        public void Log_Create_With_Labels_And_stackTrace()
        {
            // arrange
            var id = Guid.NewGuid();
            var systemId = $"{Guid.NewGuid()}";//some unique value
            var stackTrace = "ipsum lorem";
            var message = "IO exception";
            var now = DateTime.UtcNow;
            var logLevel = LogLevel.Error;
            var labels = new string[] { "CustomClass", "v12.0" };

            // act
            var log = Log.CreateLog(id, message, systemId, logLevel, stackTrace, labels);

            // assert
            Assert.NotNull(log);
            Assert.Equal(log.Id, id);
            Assert.Equal(log.Level, logLevel);
            Assert.Equal(log.SystemId, systemId);
            Assert.Equal(log.Message, message);
            Assert.Equal(log.StackTrace, stackTrace);
            Assert.True(log.Labels.Count == 2);
            Assert.True(log.RegisteredAt > now);
        }

        [Fact]
        public void Log_Create_WithOut_Labels_And_stackTrace()
        {
            // arrange
            var id = Guid.NewGuid();
            var systemId = $"{Guid.NewGuid()}";//some unique value
            var stackTrace = "";
            var message = "IO exception";
            var now = DateTime.UtcNow;
            var logLevel = LogLevel.Trace;

            // act
            var log = Log.CreateLog(id, message, systemId, logLevel, stackTrace);

            // assert
            Assert.NotNull(log);
            Assert.Equal(log.Id, id);
            Assert.Equal(log.Level, logLevel);
            Assert.Equal(log.SystemId, systemId);
            Assert.Equal(log.Message, message);
            Assert.Null(log.StackTrace);
            Assert.Null(log.Labels);
            Assert.True(log.RegisteredAt > now);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Log_Invalid_Message(string message)
        {
            // arrange
            var id = Guid.NewGuid();
            var systemId = $"{Guid.NewGuid()}";//some unique value
            var stackTrace = "ipsum lorem";
            var now = DateTime.UtcNow;
            var logLevel = LogLevel.Error;
            var labels = new string[] { "CustomClass", "v12.0" };
            Log log = null;

            // act
            var ex = Assert.Throws<LogDomainException>(() => {
                log = Log.CreateLog(id, message, systemId, logLevel, stackTrace, labels);
            });

            // assert
            Assert.Null(log);
            Assert.NotNull(ex);
            Assert.IsType<LogDomainException>(ex);
            Assert.Equal("Log's message is empty!!!", ex.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Log_Invalid_SystemId(string systemId)
        {
            // arrange
            var id = Guid.NewGuid();
            var stackTrace = "ipsum lorem";
            var message = "IO exception";
            var now = DateTime.UtcNow;
            var logLevel = LogLevel.Error;
            var labels = new string[] { "CustomClass", "v12.0" };
            Log log = null;

            // act
            var ex = Assert.Throws<LogDomainException>(() => {
                log = Log.CreateLog(id, message, systemId, logLevel, stackTrace, labels);
            });

            // assert
            Assert.Null(log);
            Assert.NotNull(ex);
            Assert.IsType<LogDomainException>(ex);
            Assert.Equal("Log's system id is empty!!!", ex.Message);
        }

        [Fact]
        public void Log_Invalid_Id()
        {
            // arrange
            var id = Guid.Empty;
            var stackTrace = "ipsum lorem";
            var message = "IO exception";
            var systemId = $"{Guid.NewGuid()}";//some unique value
            var now = DateTime.UtcNow;
            var logLevel = LogLevel.Error;
            var labels = new string[] { "CustomClass", "v12.0" };
            Log log = null;

            // act
            var ex = Assert.Throws<LogDomainException>(() => {
                log = Log.CreateLog(id, message, systemId, logLevel, stackTrace, labels);
            });

            // assert
            Assert.Null(log);
            Assert.NotNull(ex);
            Assert.IsType<LogDomainException>(ex);
            Assert.Equal("Log's id is empty!!!", ex.Message);
        }

        [Theory, MemberData(nameof(WrongLabels))]
        public void Log_Invalid_Labels(string[] labels)
        {
            // arrange
            var id = Guid.NewGuid();
            var stackTrace = "ipsum lorem";
            var message = "IO exception";
            var systemId = $"{Guid.NewGuid()}";//some unique value
            var now = DateTime.UtcNow;
            var logLevel = LogLevel.Error;
            Log log = null;

            // act
            var ex = Assert.Throws<LabelDomainException>(() => {
                log = Log.CreateLog(id, message, systemId, logLevel, stackTrace, labels);
            });

            // assert
            Assert.Null(log);
            Assert.NotNull(ex);
            Assert.IsType<LabelDomainException>(ex);
            Assert.Equal("Empty label couldn't be added!", ex.Message);
        }
        public static System.Collections.Generic.IEnumerable<object[]> WrongLabels
        {
            get
            {
                yield return new object[] { new string[] { null, "" } };
                yield return new object[] {new string[] { "", " " }};
                yield return new object[] { new string[] { null, null } };
                yield return new object[] { new string[] { "system", null } };
                yield return new object[] { new string[] { "", "t43.6" } };
            }
        }
    }
}
