using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.Repository
{
    public interface IProfessionelRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetProfessionelByIdAsync(int id);

        Task AddProfessionelAsync(TEntity entity);
        Task UpdateProfessionelAsync(TEntity entityToUpdate, ProfessionelDTO entity);
    }
}
