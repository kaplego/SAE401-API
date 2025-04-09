using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IDetailcommandeRepository<TEntity>
    {
        Task<Commande?> GetCommandeByIdAsync(int id);
        Task<Detailcommande> AddDetailcommandeAsync(TEntity entity);

    }
}
