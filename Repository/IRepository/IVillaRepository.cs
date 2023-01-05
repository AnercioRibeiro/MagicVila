using MagicVila_VilaAPI.Models;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null);
        Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked=true);
        Task CreateAsync(Villa entity);
        Task RemoveAsync(Villa entity);
        Task SaveAsync();
        Task UpdateAsync(Villa entity); 
    }
}
