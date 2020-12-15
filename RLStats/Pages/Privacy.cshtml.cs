using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		[BindProperty]
		public string platformusername { get; set; }

		public string Msg { get; set; }


		public string result { get; set; }
		public string message { get; set; }
		public string playerId { get; set; }
		public string Standard3MMR { get; set; }
		public string Standard2MMR { get; set; }
		public string GameLabel;

		private readonly ILogger<PrivacyModel> _logger;

		public PrivacyModel(ILogger<PrivacyModel> logger)
		{
			_logger = logger;
		}


		public async Task<IActionResult> OnPostAsync(string btn)
		{

			var Platform = "";

			if (btn == "Xbox")
			{
				Platform = "xbl";
			}
			else
			{
				Platform = "psn";
			}

			//http://localhost:28015/api/RocketLeague?platform=xbl&name=canihaveacheeto
			//Error Check 
			if (platformusername == null)
			{
				return Page();
			}
			var URL = "http://20.42.107.40:28015/api/RocketLeague?platform=" + Platform + "&name=" + platformusername.ToString();
			using var client = new HttpClient();
			var content = await client.GetStringAsync(URL);
			RLMMR TempTest = Newtonsoft.Json.JsonConvert.DeserializeObject<RLMMR>(content);
			//Check if Result was Valid
			if (TempTest.result)
			{
				System.Diagnostics.Debug.WriteLine("result:" + TempTest.result.ToString());
				System.Diagnostics.Debug.WriteLine("message:" + TempTest.message.ToString());
				System.Diagnostics.Debug.WriteLine("playerId:" + TempTest.playerId.ToString());
				System.Diagnostics.Debug.WriteLine("Standard3MMR:" + TempTest.Standard3MMR.ToString());
				System.Diagnostics.Debug.WriteLine("Standard2MMR:" + TempTest.Standard2MMR.ToString());

				result = "Fetched Profile";
				//message = TempTest.message.ToString();
				playerId = "PlayerID: "+TempTest.playerId.ToString();
				Standard3MMR = "3V3 MMR: " + TempTest.Standard3MMR.ToString();
				Standard2MMR = "2V2 MMR: " + TempTest.Standard2MMR.ToString();
				GameLabel = "3V3 MMR: 2V2 MMR:";
			}
			else
			{
				result = "Error In Fetching Profile";
				message = TempTest.message.ToString();
			}
			


			return Page();
		}
	}


	public class RLMMR
	{
		public bool result { get; set; }
		public string message { get; set; }
		public int playerId { get; set; }
		public int Standard3MMR { get; set; }
		public int Standard2MMR { get; set; }
	}


}