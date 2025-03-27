using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IPhotocolorationRepository<TEntity>
    {
        Task AddPhotocolorationAsync(TEntity entity);
    }
}
