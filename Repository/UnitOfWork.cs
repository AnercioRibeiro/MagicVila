using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Repository.IRepository;

namespace MagicVila_VilaAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            VillaRepository = new VillaRepository(db);
        } 
        public IVillaRepository VillaRepository { get; private set; }
    }
}
