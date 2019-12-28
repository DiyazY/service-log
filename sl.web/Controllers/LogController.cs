using MediatR;
using Microsoft.AspNetCore.Mvc;
using sl.application.Commands.CreateLog;

namespace sl.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public ActionResult AddLog([FromBody]CreateLogCommand command)
        {
            _mediator
                .Send(command)
                .Wait();
            return Ok();
        }
    }
}