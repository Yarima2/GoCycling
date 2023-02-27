using GoCycling.Models;
using GoCycling.Queries;
using GoCycling.StravaApiRequests;
using GoCycling.StravaModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace GoCycling.Pages
{
	public class LoginModel : PageModel
	{
        public bool hasCode { get; set; }
        public bool hostedLocally { get; set; }
		public string code { get; set; }

        public async void OnGet(string code)
		{
			hasCode = code != null;
            //this environment variable is set in azure but not typically on private machines
            hostedLocally = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));

            if (hasCode)
			{
				this.code = code;
			}
		}
	}
}
