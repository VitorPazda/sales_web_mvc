using sales_web_mvc.Data;
using sales_web_mvc.Models; // Importar os modelos

namespace sales_web_mvc.Services
{
    public class SellerService
    {
        private readonly sales_web_mvcContext _context;
        public SellerService(sales_web_mvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            // Retornar todos os vendedores
            return _context.Seller.ToList();
        }
        // Inserir um novo vendedor no bd
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
