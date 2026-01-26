using sales_web_mvc.Data;
using sales_web_mvc.Models; // Importar os modelos
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace sales_web_mvc.Services
{
    public class SellerService
    {
        private readonly sales_web_mvcContext _context;
        public SellerService(sales_web_mvcContext context)
        {
            _context = context;
        }

        // Funcao assincrona
        public async Task<List<Seller>> FindAllAsync()
        {
            // Retornar todos os vendedores
            return await _context.Seller.ToListAsync();
        }

        // Inserir um novo vendedor no bd, assincrona
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Encontrar o seller por id, ASSINCRONA
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Remover o seller
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool temAlgum = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!temAlgum)
            {
                throw new DirectoryNotFoundException("Id nao encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
    }
}
