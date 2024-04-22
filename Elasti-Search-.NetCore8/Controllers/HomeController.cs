using Domain.Models;
using Elasti_Search_.NetCore8.Models;
using Elastic;
using Elastic.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Rep;
using System.Diagnostics;

namespace Elasti_Search_.NetCore8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerRepository customerRepository;

        public HomeController(ILogger<HomeController> logger,IElasticService<Customer> elastic)
        {
            _logger = logger;
            customerRepository = new CustomerRepository(elastic);

        }
        [HttpGet("Test")]
        public async Task<IActionResult> Test([FromQuery] GridQueryModel gridQueryModel)
        {
            gridQueryModel.Page = 1;
            gridQueryModel.Limit = 5;
            var (totalRecords, documents) = await customerRepository.GetCustomersAsync(gridQueryModel);

            return StatusCode(200,new { documents, tol = totalRecords });
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
