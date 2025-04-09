using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IVilleRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllVilleAsync();
    }
}
