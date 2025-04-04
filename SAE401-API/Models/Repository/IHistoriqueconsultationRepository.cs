using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IHistoriqueconsultationRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetHistoriqueconsultationByIdAsync(int idproduit, int idclient);
        Task<Historiqueconsultation> AddHistoriqueconsultationAsync(TEntity entity);
        Task DeleteHistoriqueconsultationAsync(TEntity entity);
    }
}
