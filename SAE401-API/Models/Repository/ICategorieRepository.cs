using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface ICategorieRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllCategorieAsync();
    
    }
}
