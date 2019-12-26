using Moq;
using sl.application.Queries.GetLogById;
using sl.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace sl.application.unitTests.Queries
{
    public class GetLogByIdTest
    {

        [Theory, MemberData(nameof(Ids))] // look for "8d67d5a6-b37a-4a13-83a9-1088e4aa16b6"
        public void Handle_GivenValidRequest_ShouldReturnLogDetailedModel(string[] idStrings)
        {
            // arrange
            var repositoryMock = new Mock<ILogRepository>();

            var logs = new List<Log>();
            foreach (var id in idStrings)
            {
                var log = Log.CreateLog(
                    Guid.Parse(id),
                    $"message={id}",
                    $"system={id}",
                    LogLevel.Debug,
                    $"stackTrace={id}",
                    new string[] { $"l1={id}", $"l2={id}" }
                    );
                logs.Add(log);
            }

            repositoryMock.Setup(p => p.GetLogById(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                var log = logs.FirstOrDefault(p => p.Id == id);
                return log;
            });

            var handler = new GetLogByIdQuery.Handler(repositoryMock.Object);
            var command = new GetLogByIdQuery
            {
                Id = Guid.Parse("8d67d5a6-b37a-4a13-83a9-1088e4aa16b6")
            };

            // act
            var result = handler.Handle(command, CancellationToken.None).Result;

            // assert
            Assert.NotNull(result);
            repositoryMock.Verify(p => p.GetLogById(It.IsAny<Guid>()), Times.Once);
            Assert.Equal(result.Id, Guid.Parse("8d67d5a6-b37a-4a13-83a9-1088e4aa16b6"));
        }

        public static IEnumerable<object[]> Ids
        {
            get
            {
                yield return new object[] { new string[] { "879455a0-caef-4abb-a4db-7d3c107db175", "8d67d5a6-b37a-4a13-83a9-1088e4aa16b6", "7449662c-65a3-4a0c-8732-5799446d2ac5" } };
            }
        }
    }
}
