using Moq;
using sl.application.Queries;
using sl.application.Queries.GetLogsByTerm;
using sl.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace sl.application.unitTests.Queries
{
    public class GetLogsByTermTest
    {
        [Theory, MemberData(nameof(Ids))] // look for "8d67d5a6-b37a-4a13-83a9-1088e4aa16b6"
        public void Handle_GivenValidRequest_ShouldReturnLogViewModels(string[] idStrings)
        {
            // arrange
            var repositoryMock = new Mock<IQueryRepository>();

            var logs = new List<Log>();
            foreach (var id in idStrings)
            {
                var log = Log.CreateLog(
                    Guid.Parse(id),
                    $"message={id}",
                    $"system1",
                    LogLevel.Debug,
                    $"stackTrace={id}",
                    new string[] { $"l1={id}", $"l2={id}" }
                    );
                logs.Add(log);
            }

            repositoryMock
                .Setup(p => p.GetLogsByTerm(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns((string systemId, int page, int count, string term) =>
            {
                return logs.Where(p => p.StackTrace.Contains(term) || p.Message.Contains(term) || p.SystemId.Contains(term)
                    || p.Level.ToString().Contains(term) || p.Labels.Any(c => c.Value.Contains(term)) || p.RegisteredAt.ToString().Contains(term)
                ).Select(p => new Models.LogViewModel
                {
                    Id = p.Id,
                    RegisteredAt = p.RegisteredAt,
                    Labels = p.Labels.Select(c => c.Value).ToArray(),
                    Level = p.Level.ToString(),
                    Message = p.Message,
                    SystemId = p.SystemId
                });
            });

            var handler = new GetLogsByTermQuery.Handler(repositoryMock.Object);
            var command = new GetLogsByTermQuery
            {
                SystemId = $"system1",
                Page = 1,
                Count = 2,
                Term = "1088e4aa16b6" // part of id
            };

            // act
            var result = handler.Handle(command, CancellationToken.None).Result;

            // assert
            Assert.NotNull(result);
            Assert.Single(result.Logs);
            repositoryMock.Verify(p => p.GetLogsByTerm(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            Assert.Single(result.Logs, p => p.Id == Guid.Parse("8d67d5a6-b37a-4a13-83a9-1088e4aa16b6"));
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
