using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IAttributRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAttributByTypeAsync(int id);

    }
}
