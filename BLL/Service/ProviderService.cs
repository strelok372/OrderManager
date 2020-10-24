using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Service
{
    public interface IProviderService
    {
        Task<Provider> Get(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Provider>> GetAll(CancellationToken cancellationToken);
    }
    
    public class ProviderService : IProviderService
    {
        private readonly ApplicationContext _context;

        public ProviderService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<Provider> Get(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Provider>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _context.Providers.ToListAsync(cancellationToken);
            return list;
        }
    }
}