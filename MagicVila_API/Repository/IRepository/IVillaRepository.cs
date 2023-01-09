using MagicVila_VilaAPI.Models;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa>UpdateAsync(Villa entity);
    }
}
