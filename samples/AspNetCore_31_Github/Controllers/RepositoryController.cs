using AspNetCore_31_Github.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApi.Client;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AspNetCore_31_Github.Controllers
{
	public class RepositoryController : Controller
	{
		private readonly ILogger<RepositoryController> _logger;
		private readonly IRestClient _restClient;

		public RepositoryController(ILogger<RepositoryController> logger, IRestClient restClient)
		{
			_logger = logger;
			_restClient = restClient;
		}

		public async Task<IActionResult> Index()
		{
			using var repositories = await _restClient.GetAsync<List<Repository>>("user/repos");
			return View(repositories.Content);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
