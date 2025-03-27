using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.Repository
{
    public interface IPaysRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllPaysAsync();
    }
}
