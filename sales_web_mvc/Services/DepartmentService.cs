using sales_web_mvc.Data;
using sales_web_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace sales_web_mvc.Services
{
    public class DepartmentService
    {
        private readonly sales_web_mvcContext _context;
        public DepartmentService(sales_web_mvcContext context)
        {
            _context = context;
        }

        // Funcao assincrona
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Nome).ToListAsync(); // Ordernar por nome
        }
    }
}
