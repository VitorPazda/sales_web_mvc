using sales_web_mvc.Models;
using sales_web_mvc.Models.Enums;

namespace sales_web_mvc.Data
{
    public class SeedingService
    {
        private sales_web_mvcContext _context;
        public SeedingService(sales_web_mvcContext context)
        {
            _context = context;
        }

        // Funcao para popular a base de dados
        public void Seed()
        {
            // Verificar se existe algum dado nas tabelas
            /*
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; // bd ja populado
            }
            */

            // Instanciar os dados no bd

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Filipe", "filipe@gmail.com", new DateTime(2005, 9, 6), 1000.0, d1);
            Seller s2 = new Seller(2, "Vinicius Saco", "vinicius_saco@gmail.com", new DateTime(2003, 11,27), 1000.0, d1);
            Seller s3 = new Seller(3, "Vitor", "vitor@gmail.com", new DateTime(2005, 9, 18), 1000.0, d1);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2026, 01, 18), 1100.0, SaleStatus.Billed, s2);

            // Add no banco

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3);
            _context.SalesRecord.AddRange(r1);

            _context.SaveChanges();
        }
    }
}
