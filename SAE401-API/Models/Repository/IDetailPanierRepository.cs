using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IDetailPanierRepository<TEntity>
    {

        Task<ActionResult<TEntity?>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient);

        Task AddDetailPanierAsync(TEntity entity);
        Task UpdateDetailPanierAsync(Detailpanier detailpanier, TEntity entity);
        Task DeleteDetailPanierAsync(Detailpanier detailpanier);
    }
}
