using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.Repository
{
    public interface IVilleRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllVilleAsync();
    }
}
