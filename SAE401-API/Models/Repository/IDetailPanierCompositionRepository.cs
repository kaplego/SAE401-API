using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IDetailPanierCompositionRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetDetailPanierCompositionByIdAsync(int idcomposition, int idclient);

        Task<Detailpaniercomposition> AddDetailPanierCompositionAsync(TEntity entity);
        Task<Detailpaniercomposition> UpdateDetailPanierCompositionAsync(Detailpaniercomposition detailpaniercomposition, TEntity entity);
        Task DeleteDetailPanierCompositionAsync(Detailpaniercomposition detailpaniercomposition);
    }
}
