using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class CommandeManager<TEntity> : ICommandeRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public CommandeManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetCommandeByIdAsync(int idcommande)
        {
            var commande = await _milibooContext.Commandes
                .Include(c => c.ClientNavigation)
                .Include(c => c.AdresseLivrNavigation)
                .Include(c => c.AdresseFactNavigation)
                .Include(c => c.StatutNavigation)
                .Include(c => c.TransporteurNavigation)
                .Include(c => c.DetailsProduitNavigation).ThenInclude(p => p.ColorationNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(c => c.DetailsProduitNavigation).ThenInclude(p => p.ColorationNavigation).ThenInclude(p => p.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(c => c.DetailsCompositionNavigation).ThenInclude(d => d.CompositionNavigation).ThenInclude(c => c.DetailsNavigation)
                .ThenInclude(p => p.ColorationNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(c => c.DetailsCompositionNavigation).ThenInclude(d => d.CompositionNavigation).ThenInclude(c => c.DetailsNavigation)
                .ThenInclude(p => p.ColorationNavigation).ThenInclude(p => p.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .FirstOrDefaultAsync(c => c.Idcommande == idcommande);

            return commande != null ? new ActionResult<TEntity>((TEntity)(object)commande) : new NotFoundResult();
        }

        public async Task<Commande> AddCommandeAsync(TEntity entity)
        {
            if (entity is Commande commande)
            {
                await _milibooContext.Commandes.AddAsync(commande);
                await _milibooContext.SaveChangesAsync();
                return commande;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }


    }
}
