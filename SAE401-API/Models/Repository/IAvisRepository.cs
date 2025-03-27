using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IAvisRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetAvisByIdAsync(int idavis);
        Task AddAvisAsync(TEntity entity);
        Task DeleteAvisAsync(TEntity entity);
    }  
}
