using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RLStats.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public async Task OnPostAsync()
        {

            using var client = new HttpClient();

            var content = await client.GetStringAsync("https://api.tracker.gg/api/v2/rocket-league/standard/profile/xbl/canihaveacheeto");


            var inGameName = Request.Form["ingamename"];
            
            System.Diagnostics.Debug.WriteLine(content);
            
        }

        public void OnGet()
        {
        }
    }
}
