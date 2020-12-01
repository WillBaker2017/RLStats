using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Data.SqlClient;
using System.Data.Entity;

namespace RLStats.Pages
{
    public class IndexModel : PageModel
    {
        public string ServerValue = String.Empty;

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


            string connStr = "Server=tcp:rlstats.database.windows.net,1433;Initial Catalog=RLStats;Persist Security Info=False;User ID=rladmin;Password=RLpassword2020!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=10;";
            string usernamereturn;//the username from sql
            using (var con = new SqlConnection(connStr))//when we connect to sql database
            {
                var sql = "select USERNAME from USER_ACCOUNTS WHERE USERNAME = @username AND PASSWORD = @password";//our qury to exec a login test
                using (var cmd = new SqlCommand(sql, con))//build the commmand
                {
                    cmd.Parameters.AddWithValue("@username", uName.ToString());//send username entered
                    cmd.Parameters.AddWithValue("@password", pWord.ToString());//send password entered
                    con.Open();//open the connection
                    usernamereturn = (string)cmd.ExecuteScalar();//get the request from the server and set the varable from the query
                    con.Close();//close the connection to sql database
                }
            }//after using the sql database we know if the username and password was entered correctly
            if (usernamereturn == uName.ToString())
            {
                System.Diagnostics.Debug.WriteLine("Log in succfulll");

            }
            else {
                System.Diagnostics.Debug.WriteLine("bruh");
            }

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
