using EM.Data;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Performer> AddPerformerImageAsync(Performer performer);
        Task<IEnumerable<Performer>> GetAllPerformersAsync(); 

    }
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext dbcontext)
        {
            context = dbcontext;
        }
        public async Task<Performer> AddPerformerImageAsync(Performer performer)
        {
            context.Performers.Add(performer);
            await context.SaveChangesAsync();
            return performer;
        }

        public Task<IEnumerable<Performer>> GetAllPerformersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
