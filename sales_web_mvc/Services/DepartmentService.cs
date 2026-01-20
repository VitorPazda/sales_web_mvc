using sales_web_mvc.Data;
using sales_web_mvc.Models;

namespace sales_web_mvc.Services
{
    public class DepartmentService
    {
        private readonly sales_web_mvcContext _context;
        public DepartmentService(sales_web_mvcContext context)
        {
            _context = context;
        }

        // Retornar os departamentos
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Nome).ToList(); // Ordernar por nome
        }
    }
}
