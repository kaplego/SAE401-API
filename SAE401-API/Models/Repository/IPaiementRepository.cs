using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IPaiementRepository<TEntity>
    {
        Task<ActionResult<Commande?>> GetCommandeByIdAsync(int idcommande);
        Task AddPaiementAsync(TEntity entity);
    }
}
