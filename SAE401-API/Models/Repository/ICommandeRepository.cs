using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface ICommandeRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetCommandeByIdAsync(int idcommande);
        Task<Commande> AddCommandeAsync(TEntity entity);

    }

}
