using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface ICommandeRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetCommandeByIdAsync(int idcommande);
        Task AddCommandeAsync(TEntity entity);

    }
      
}
