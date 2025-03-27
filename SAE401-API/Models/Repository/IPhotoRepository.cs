using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IPhotoRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetPhotoByIdAsync(int id);
        Task AddPhotoAsync(TEntity entity);
        Task DeletePhotoAsync(Photo photo);
    }
}
