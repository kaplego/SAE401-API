using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class CommandecompositionManager<TEntity> : ICommandecompositionRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public CommandecompositionManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetCommandecompositionByIdsAsync(int idcomposition, int idcommande)
        {
            var commandecomposition = await _milibooContext.Commandecompositions
                .Include(cc => cc.CommandeNavigation)  // Include related entities as needed
                .Include(cc => cc.CompositionNavigation)
                .FirstOrDefaultAsync(cc => cc.Idcomposition == idcomposition && cc.Idcommande == idcommande);

            return commandecomposition != null ? new ActionResult<TEntity>((TEntity)(object)commandecomposition) : new NotFoundResult();
        }

        public async Task AddCommandecompositionAsync(TEntity entity)
        {
            if (entity is Commandecomposition commandecomposition)
            {
                _milibooContext.Commandecompositions.Add(commandecomposition);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task UpdateCommandecompositionAsync(Commandecomposition commandecomposition, TEntity entity)
        {
            if (entity is Commandecomposition updatedCommandecomposition)
            {
                commandecomposition.Quantitecompositioncommande = updatedCommandecomposition.Quantitecompositioncommande;
            }

            _milibooContext.Entry(commandecomposition).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
        }

        public async Task DeleteCommandecompositionAsync(Commandecomposition commandecomposition)
        {
            _milibooContext.Commandecompositions.Remove(commandecomposition);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
