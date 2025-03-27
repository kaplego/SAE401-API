using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IDetailcommandeRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetDetailcommandeByIdsAsync(int idproduit, int idcouleur, int idcommande);
        Task AddDetailcommandeAsync(TEntity entity);
        Task UpdateDetailcommandeAsync(Detailcommande detailcommande, TEntity entity);
        Task DeleteDetailcommandeAsync(Detailcommande detailcommande);
    }
}
