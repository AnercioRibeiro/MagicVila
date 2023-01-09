using MagicVila_VilaAPI.Models;
using System.Linq.Expressions;

namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber>UpdateAsync(VillaNumber entity);
    }
}
