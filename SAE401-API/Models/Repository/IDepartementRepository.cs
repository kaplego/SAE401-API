using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.Repository
{
    public interface IDepartementRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllDepartementAsync();
    }
}
