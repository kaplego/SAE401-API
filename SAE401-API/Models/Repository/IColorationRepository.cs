using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IColorationRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetColorationByIdAsync(int idproduit, int idcouleur);
        Task<Coloration> AddColorationAsync(TEntity entity);
        Task<Coloration> UpdateColorationAsync(Coloration coloration, TEntity entity);
    }
}
