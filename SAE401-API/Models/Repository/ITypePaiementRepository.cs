using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface ITypePaiementRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllTypePaiementAsync();

    }
}
