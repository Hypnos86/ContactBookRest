using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookRest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController : ControllerBase
    {

		[HttpGet("book")]
		public string MyController()
		{
			string myprop = "kamil";


			return myprop;

		
		}
	}
}

