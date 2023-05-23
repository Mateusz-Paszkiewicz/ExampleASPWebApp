using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Beers
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public IList<Beer> Beer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var beers = from s in _context.Beer
                         select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                beers = beers.Where(s => s.Type.Contains(SearchString));
            }

            if(bottleChoice != 0)
            {
                if (bottleChoice == 1)
                    beers = beers.Where(s => s.czyButelka == true);
                else
                {
                    beers = beers.Where(s => s.czyButelka == false);
                }
            }

            Beer = await beers.ToListAsync();
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int bottleChoice { get; set; }

        public SelectList? Types { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BeerType { get; set; }
    }
}
