using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface ICommandecompositionRepository<TEntity>
    {

        Task<Commande?> GetCommandeByIdAsync(int id);
        Task<Commandecomposition> AddCommandecompositionAsync(TEntity entity);


    }
}
