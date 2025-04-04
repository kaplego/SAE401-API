using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IProduitsimilaireRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetProduitsimilaireByIdAsync(int idproduitRef, int idproduitSim);
        Task<Produitsimilaire> AddProduitsimilaireAsync(TEntity entity);
        Task DeleteProduitsimilaireAsync(TEntity entity);
    }
}
