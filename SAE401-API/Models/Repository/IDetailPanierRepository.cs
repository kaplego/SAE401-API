using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IDetailPanierRepository<TEntity>
    {

        Task<ActionResult<TEntity?>> GetDetailPanierByIdAsync(int idproduit, int idcouleur, int idclient);

        Task AddDetailPanierAsync(TEntity entity);
        Task UpdateDetailPanierAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteDetailPanierAsync(TEntity entity);
    }
}
