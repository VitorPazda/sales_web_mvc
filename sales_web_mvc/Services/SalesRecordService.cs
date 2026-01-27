using sales_web_mvc.Data;
using sales_web_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace sales_web_mvc.Services
{
    public class SalesRecordService
    {
        private readonly sales_web_mvcContext _context;
        public SalesRecordService(sales_web_mvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller) // Join das tabelas
                .Include(x => x.Seller.Department) // Join com a tabela de departamentos
                .OrderByDescending(x => x.Date) // Ordenar por data
                .ToListAsync();
        }
    }
}
