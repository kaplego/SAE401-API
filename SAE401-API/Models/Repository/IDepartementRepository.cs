using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IDepartementRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllDepartementAsync();
    }
}
