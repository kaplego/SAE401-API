using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IAvisRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetAvisByIdAsync(int idavis);
        Task<Avisproduit> AddAvisAsync(TEntity entity);
        Task DeleteAvisAsync(TEntity entity);
    }
}
