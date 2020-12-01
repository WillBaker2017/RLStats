using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RLStats.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            var emailAddress = Request.Form["emailaddress"];
            var uName = Request.Form["uname"];
            var pWord = Request.Form["pword"];
            // do something with emailAddress
            System.Diagnostics.Debug.WriteLine(emailAddress);
            System.Diagnostics.Debug.WriteLine(uName);
            System.Diagnostics.Debug.WriteLine(pWord);
        }



        public void OnGet()
        {
            


            int x = 1;
            //string uName = IndexModel.ReferenceEquals;
            if (x==1)
            {

            }
        }
    }
}
