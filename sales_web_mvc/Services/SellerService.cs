using sales_web_mvc.Data;
using sales_web_mvc.Models; // Importar os modelos
using Microsoft.EntityFrameworkCore;

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

        // Encontrar o seller por id
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        
        // Remover o seller
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
