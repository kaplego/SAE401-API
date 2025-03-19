using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IRegroupementRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllRegroupementAsync();

    }
}
