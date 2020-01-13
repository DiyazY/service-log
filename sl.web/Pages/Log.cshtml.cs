using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sl.application.Models;
using sl.application.Queries.GetLogById;

namespace sl.web
{
    public class LogModel : PageModel
    {
        private readonly IMediator _mediator;

        public LogModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public Guid LogId { get; set; }
        public LogDetailedViewModel Log { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["returnUrl"] = Request.Headers["Referer"].ToString();
            var query = new GetLogByIdQuery { Id = LogId };
            Log = await _mediator.Send(query);
        }
    }
}