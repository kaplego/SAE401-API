using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface ITransporteurRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllTransporteurAsync();
    }
}

