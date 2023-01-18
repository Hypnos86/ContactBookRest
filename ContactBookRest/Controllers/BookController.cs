using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookRest.Models;
using ContactBookRest.Repository;

namespace ContactBookRest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController : Controller
    {

		[HttpGet("allcontact")]
		public ActionResult GetAllContact()
		{
			BaseRepository baseRepository = new BaseRepository(); 
			List<Contact> result = baseRepository.GetAllContact();
			return Json(result);
		}

		[HttpGet("test")]
		public ActionResult Test()
		{
			BaseRepository baseRepository = new BaseRepository();
			string result = baseRepository.GetTest();
			return Json(result);
		}
	}
}

