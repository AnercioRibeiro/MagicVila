namespace MagicVila_VilaAPI.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IVillaRepository VillaRepository { get; }
        IVillaNumberRepository VillaNumberRepository { get; }


    }
}
