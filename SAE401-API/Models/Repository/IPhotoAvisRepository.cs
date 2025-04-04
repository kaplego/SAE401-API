using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IPhotoAvisRepository<TEntity>
    {
        Task<ActionResult<Avisproduit?>> GetAvisByIdAsync(int idavis);
        Task<Photoavi> AddPhotoAvisAsync(TEntity entity);
    }
}
