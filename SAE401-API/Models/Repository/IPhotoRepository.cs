using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IPhotoRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetPhotoByIdAsync(int id);
        Task<Photo> AddPhotoAsync(TEntity entity);
        Task DeletePhotoAsync(Photo photo);
    }
}
