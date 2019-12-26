using MediatR;
using Moq;
using sl.application.Commands.CreateLog;
using sl.domain.Exceptions;
using sl.domain.Models;
using System;
using System.Threading;
using Xunit;

namespace sl.application.unitTests.Commands
{
    public class CreateLogTest
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldCreateLog()
        {
            // arrange
            var repositoryMock = new Mock<ILogRepository>();

            var handler = new CreateLogCommand.Handler(repositoryMock.Object);
            var command = new CreateLogCommand
            {
                Id = Guid.NewGuid(),
                Message = "Exception 123QWE",
                Level = "Warn",
                SystemId = "sys32",
                Labels = new string[] { "sys32", "pron" }
            };

            // act
            var result = handler.Handle(command, CancellationToken.None).Result;

            // assert
            Assert.Equal(Unit.Value, result);
            repositoryMock.Verify(p => p.AddLog(It.IsAny<Log>()), Times.Once);
        }

        [Fact]
        public void Handle_GivenInvalidRequest_ShouldThrowLogDomainException()
        {
            // arrange
            var repositoryMock = new Mock<ILogRepository>();

            var handler = new CreateLogCommand.Handler(repositoryMock.Object);
            var command = new CreateLogCommand
            {
                Id = Guid.NewGuid(),
                Message = "Exception 123QWE",
                Level = "warn",
                SystemId = "sys32",
                Labels = new string[] { "sys32", "pron" }
            };

            // act
            var ex = Assert.Throws<LogDomainException>(() => handler.Handle(command, CancellationToken.None).Wait());


            // assert
            Assert.NotNull(ex);
            Assert.IsType<LogDomainException>(ex);
            repositoryMock.Verify(p => p.AddLog(It.IsAny<Log>()), Times.Never);
            Assert.Equal("Log's level is not right!!! Ex.: Trace, Debug, Info, Warn, Error, Fatal.", ex.Message);
        }
    
    }
}
