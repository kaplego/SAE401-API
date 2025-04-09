using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IDetailPanierRepository<TEntity>
    {

        Task<ActionResult<TEntity?>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient);

        Task<Detailpanier> AddDetailPanierAsync(TEntity entity);
        Task<Detailpanier> UpdateDetailPanierAsync(Detailpanier detailpanier, DetailpanierDTO entity);
        Task DeleteDetailPanierAsync(Detailpanier detailpanier);
    }
}
