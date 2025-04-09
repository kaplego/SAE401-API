using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class ColorationManager<TEntity> : IColorationRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public ColorationManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetColorationByIdAsync(int idproduit, int idcouleur)
        {
            var coloration = await _milibooContext.Colorations
                .Include(c => c.ProduitNavigation) // Inclure les informations sur le produit
                .Include(c => c.CouleurNavigation)  // Inclure les informations sur la couleur
                .Include(c => c.PhotocolsNavigation).ThenInclude((p) => p.PhotoNavigation) // Inclure les photos de la coloration du produit
                .FirstOrDefaultAsync(c => c.Idproduit == idproduit && c.Idcouleur == idcouleur);

            return coloration != null ? new ActionResult<TEntity>((TEntity)(object)coloration) : new NotFoundResult();
        }

        public async Task<Coloration> AddColorationAsync(TEntity entity)
        {
            if (entity is Coloration coloration)
            {
                await _milibooContext.Colorations.AddAsync(coloration);
                await _milibooContext.SaveChangesAsync();
                return coloration;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task<Coloration> UpdateColorationAsync(Coloration coloration, TEntity entity)
        {
            if (entity is Coloration updatedColoration)
            {
                coloration.Prixvente = updatedColoration.Prixvente;
                coloration.Prixsolde = updatedColoration.Prixsolde;
                coloration.Quantitestock = updatedColoration.Quantitestock;
                coloration.Descriptioncoloration = updatedColoration.Descriptioncoloration;
                coloration.Estvisible = updatedColoration.Estvisible;
            }

            _milibooContext.Entry(coloration).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
            return coloration;
        }
    }
}
