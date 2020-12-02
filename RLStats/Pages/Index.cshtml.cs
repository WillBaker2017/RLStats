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
        string connStr = "Server=tcp:rlstats.database.windows.net,1433;Initial Catalog=RLStats;Persist Security Info=False;User ID=rladmin;Password=RLpassword2020!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=10;";

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public string password { get; set; }

        public string Msg { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string usererror { get; set; }

		public string ServerValue = String.Empty;

        private readonly ILogger<IndexModel> _logger;


		


        public IActionResult OnPost(string btn)
        { 
            //Determine if the user is logging in or signing up

            if(btn == "Login")
			{
                //user is logging in
                string sqlReturnString;
                //Start SQL Connection
                using (var con = new SqlConnection(connStr))//when we connect to sql database
                {
                    var sql = "select USERNAME from USER_ACCOUNTS WHERE USERNAME = @username AND PASSWORD = @password";//our qury to exec a login test
                    using (var cmd = new SqlCommand(sql, con))//build the commmand
                    {
                        cmd.Parameters.AddWithValue("@username", username);//send username entered
                        cmd.Parameters.AddWithValue("@password", password);//send password entered
                        con.Open();//open the connection
                        sqlReturnString = (string)cmd.ExecuteScalar();//get the request from the server and set the varable from the query
                        con.Close();//close the connection to sql database
                    }
                }//after using the sql database we know if the username and password was entered correctly
                 //Check for returned username
                if (sqlReturnString == username.ToString())
                {
                    //Log in Was Valid Send to Profile
                    System.Diagnostics.Debug.WriteLine("Log in succfulll");
                    return RedirectToPage("/Privacy");

                }
                else
                {
                    //User entered incorrect information
                    System.Diagnostics.Debug.WriteLine("Log in Failed");
                    return Page();
                }
			}
			else
			{
                //User is signing up
                string sqlReturnString;
                //First Get the users input and check its not null
                if (CheckNewAccount(username) == 0)
                {
                    return Page();
                }

                //Start SQL Connection
                using (var con = new SqlConnection(connStr))//when we connect to sql database
                {
                    var sql = "INSERT INTO USER_ACCOUNTS (username,password) VALUES (@username, @password);";//our qury to exec a login test
                    using (var cmd = new SqlCommand(sql, con))//build the commmand
                    {
                        cmd.Parameters.AddWithValue("@username", username);//send username entered
                        cmd.Parameters.AddWithValue("@password", password);//send password entered
                        con.Open();//open the connection
                        sqlReturnString = (string)cmd.ExecuteScalar();//get the request from the server and set the varable from the query
                        con.Close();//close the connection to sql database
                    }
                }//after using the sql database we know if the username and password was entered correctly
                 //Check for returned username
                return RedirectToPage("/Privacy");
            }
        }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public int CheckNewAccount(String username)
		{
            //connect to sql and check for username existing 
            string sqlReturnString;
            //First Get the users input and check its not null
            //Start SQL Connection
            using (var con = new SqlConnection(connStr))//when we connect to sql database
            {
                var sql = "select USERNAME from USER_ACCOUNTS WHERE USERNAME = @username";//our qury to exec a login test
                using (var cmd = new SqlCommand(sql, con))//build the commmand
                {
                    cmd.Parameters.AddWithValue("@username", username.ToString());//send username entered
                   
                    con.Open();//open the connection
                    sqlReturnString = (string)cmd.ExecuteScalar();//get the request from the server and set the varable from the query
                    con.Close();//close the connection to sql database
                }
            }//after using the sql database we know if the username and password was entered correctly
            if (sqlReturnString == username.ToString())
            {
                //Username Already exists
                System.Diagnostics.Debug.WriteLine("Duplicate Username Found");
                usererror = "Duplicate Username Found";
                return 0;
            }
              System.Diagnostics.Debug.WriteLine("User Dosent Exist, Create Account");
              return 1;
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
