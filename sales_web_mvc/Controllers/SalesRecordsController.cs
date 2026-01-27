using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Services;
using System.Threading.Tasks;

namespace sales_web_mvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View();
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
