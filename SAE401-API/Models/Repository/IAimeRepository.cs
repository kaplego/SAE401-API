using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IAimeRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetAimeByIdAsync(int idclient, int idproduit);
        Task<Aime> AddAimeAsync(TEntity entity);
        Task DeleteAimeAsync(TEntity entity);
    }
}
