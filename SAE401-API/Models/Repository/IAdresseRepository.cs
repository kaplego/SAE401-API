using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IAdresseRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetAdresseByIdAsync(int idadresse);
        Task<Adresse> AddAdresseAsync(TEntity entity);
        Task<Adresse> UpdateAdresseAsync(Adresse adresse, AdresseDTO entity);
        Task DeleteAdresseAsync(Adresse adresse);
    }
}
