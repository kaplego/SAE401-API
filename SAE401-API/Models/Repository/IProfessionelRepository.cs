using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IProfessionelRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetProfessionelByIdAsync(int id);

        Task<Professionel> AddProfessionelAsync(TEntity entity);
        Task<Professionel> UpdateProfessionelAsync(TEntity entityToUpdate, ProfessionelDTO entity);
    }
}
