using SAE401_API.Models.Repository;
using SAE401_API.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DataManager
{
    public class ProduitsimilaireManager : IProduitsimilaireRepository<Produitsimilaire>
    {
        private readonly _DBMilibooContext _milibooContext;

        public ProduitsimilaireManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        // Méthode pour récupérer un produit similaire par ID
        public async Task<ActionResult<Produitsimilaire?>> GetProduitsimilaireByIdAsync(int idproduitRef, int idproduitSim)
        {
            var produitSimilaire = await _milibooContext.Produitsimilaires
                .Include(p => p.ProduitRefNavigation)  // Inclure les informations sur le produit de référence
                .Include(p => p.ProduitSimNavigation)  // Inclure les informations sur le produit similaire
                .FirstOrDefaultAsync(p => p.IdproduitRef == idproduitRef && p.IdproduitSim == idproduitSim);

            return produitSimilaire != null ? new ActionResult<Produitsimilaire>(produitSimilaire) : new NotFoundResult();
        }

        // Méthode pour ajouter un produit similaire
        public async Task AddProduitsimilaireAsync(Produitsimilaire produitsimilaire)
        {
            _milibooContext.Produitsimilaires.Add(produitsimilaire);
            await _milibooContext.SaveChangesAsync();
        }

        // Méthode pour supprimer un produit similaire
        public async Task DeleteProduitsimilaireAsync(Produitsimilaire produitsimilaire)
        {
            _milibooContext.Produitsimilaires.Remove(produitsimilaire);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
