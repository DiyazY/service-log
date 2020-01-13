using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sl.application.Models;
using sl.application.Queries.GetLogsByTerm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sl.web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public string Term { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        [BindProperty(SupportsGet = true)]
        public string SystemId { get; set; }
        public IEnumerable<LogViewModel> Data { get; set; } = new List<LogViewModel>();


        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(SystemId))
            {
                var response = await _mediator.Send(new GetLogsByTermQuery
                {
                    Count = PageSize,
                    Page = CurrentPage,
                    SystemId = SystemId,
                    Term = Term
                });
                Data = response.Logs;
                Count = response.Count;
            }
        }
    }
}
