using SAE401_API.Models.Repository;
using SAE401_API.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DataManager
{
    public class HistoriqueconsultationManager<TEntity> : IHistoriqueconsultationRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public HistoriqueconsultationManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetHistoriqueconsultationByIdAsync(int idproduit, int idclient)
        {
            var historiqueConsultation = await _milibooContext.Historiqueconsultations
                .Include(h => h.ClientNavigation)  // Inclure les informations sur le client
                .Include(h => h.ProduitNavigation)  // Inclure les informations sur le produit
                .FirstOrDefaultAsync(h => h.Idproduit == idproduit && h.Idclient == idclient);

            return historiqueConsultation != null ? new ActionResult<TEntity>((TEntity)(object)historiqueConsultation) : new NotFoundResult();
        }

        public async Task AddHistoriqueconsultationAsync(TEntity entity)
        {
            if (entity is Historiqueconsultation historiqueconsultation)
            {
                _milibooContext.Historiqueconsultations.Add(historiqueconsultation);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task DeleteHistoriqueconsultationAsync(Historiqueconsultation historiqueconsultation)
        {
            _milibooContext.Historiqueconsultations.Remove(historiqueconsultation);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
