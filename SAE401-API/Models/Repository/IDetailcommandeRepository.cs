using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IDetailcommandeRepository<TEntity>
    {
        Task<Detailcommande> AddDetailcommandeAsync(TEntity entity);

    }
}
