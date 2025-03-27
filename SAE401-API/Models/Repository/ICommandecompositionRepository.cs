using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface ICommandecompositionRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetCommandecompositionByIdsAsync(int idcomposition, int idcommande);
        Task AddCommandecompositionAsync(TEntity entity);
        Task UpdateCommandecompositionAsync(Commandecomposition commandecomposition, TEntity entity);
        Task DeleteCommandecompositionAsync(Commandecomposition commandecomposition);
    }
}
