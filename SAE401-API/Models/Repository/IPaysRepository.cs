using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IPaysRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllPaysAsync();
    }
}
