using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;


namespace SAE401_API.Models.DataManager
{
    public class ProduitManager : IProduitRepository<Produit>
    {
        readonly _DBMilibooContext milibooContext;

        public ProduitManager() { }

        public ProduitManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Produit>>>GetAllProduitAsync()
        {
            return await milibooContext.Produits
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(p => p.ValeursNavigation).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRegroupementAsync(int id)
        {
            List<int> idproduits = await milibooContext.Detailregroupements
                .Where(dr => dr.Idregroupement == id).Select(dr => dr.Idproduit).Distinct().ToListAsync();

            return await milibooContext.Produits
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(p => p.ValeursNavigation).Where(p => idproduits.Contains(p.Idproduit)).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByCategorieAsync(int id)
        {

            Categorieproduit categorie = await milibooContext.Categorieproduits
                .Include(c => c.CategorieEnfanteNavigation)
                .FirstAsync(c => c.Idcategorie == id);
            List<Produit> produits = await milibooContext.Produits
                .Include(p => p.TypeNavigation).Include(p => p.ColorationsNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(p => p.ValeursNavigation).Where(p => p.TypeNavigation.Idcategorie == id).ToListAsync();
            foreach (Categorieproduit cat in categorie.CategorieEnfanteNavigation)
            {
                produits.AddRange(GetAllProduitByCategorieAsync(cat.Idcategorie).Result.Value);
            }
            foreach (Produit p in produits) { p.TypeNavigation = null; }
            return produits;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByTypeAsync(int id)
        {
            Typeproduit type = await milibooContext.Typeproduits
                .Include(t => t.ProduitsNavigation).ThenInclude(p => p.ColorationsNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(t => t.ProduitsNavigation).ThenInclude(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(t => t.ProduitsNavigation).ThenInclude(p => p.ValeursNavigation).FirstAsync(t => t.Idtypeproduit == id);
            return type.ProduitsNavigation.ToList();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllProduitByRechercheAsync(string recherche, int seuil)
        {
            var produits = await milibooContext.Produits
                .Include(p => p.ValeursNavigation)
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .ToListAsync();


            return produits
               .Where(u => ContainsApproximateMatch(u.Nomproduit, recherche, seuil))  // Filtrer les produits
               .OrderBy(u => GetMinLevenshteinDistance(u.Nomproduit, recherche))       // Trier par la plus petite distance de Levenshtein
               .ToList();  // Effectuer la conversion finale en liste

        }


        private bool ContainsApproximateMatch(string nomProduit, string recherche, int seuil)
        {
            // Vérifier chaque sous-chaîne du nom du produit
            for (int i = 0; i <= nomProduit.Length - recherche.Length; i++)
            {
                string substring = nomProduit.Substring(i, recherche.Length);
                if (MethodProduitManager.LevenshteinDistance(substring.ToUpper(), recherche.ToUpper()) <= seuil)
                {
                    return true;  // Trouvé une correspondance approximative
                }
            }
            return false;  // Aucune sous-chaîne ne correspond
        }

        private int GetMinLevenshteinDistance(string nomProduit, string recherche)
        {
            int minDistance = int.MaxValue;

            // Vérifier chaque sous-chaîne du nom du produit
            for (int i = 0; i <= nomProduit.Length - recherche.Length; i++)
            {
                string substring = nomProduit.Substring(i, recherche.Length);
                int distance = MethodProduitManager.LevenshteinDistance(substring.ToUpper(), recherche.ToUpper());
                minDistance = Math.Min(minDistance, distance);  // Trouver la plus petite distance
            }

            return minDistance;
        }

        public async Task<ActionResult<Produit>> GetProduitByIdAsync(int id)
        {
            return await milibooContext.Produits
                .Include(p => p.ValeursNavigation)
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.CouleurNavigation)
                .Include(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(p => p.AvisNavigation).ThenInclude(a => a.PhotoavisNavigation).ThenInclude(p =>p.PhotoNavigation)
                .FirstOrDefaultAsync(p => p.Idproduit == id);
        }

        public async Task AddProduitAsync(Produit entity)
        {
            await milibooContext.Produits.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }

        
        public async Task UpdateProduitAsync(Produit produit, ProduitDTO entity)
        {
            milibooContext.Entry(produit).State = EntityState.Modified;
            produit.Idproduit = produit.Idproduit;
            produit.Idtypeproduit = entity.Idtypeproduit;
            produit.Idpays = entity.Idpays;
            produit.Nomproduit = entity.Nomproduit;
            produit.Notice = entity.Sourcenotice;
            produit.Aspecttechnique = entity.Sourceaspecttechnique;
            produit.Delailivraison = entity.Delailivraison;
            produit.Coutlivraison = entity.Coutlivraison;
            produit.Nbpaiementmax = entity.Nbpaiementmax;
            await milibooContext.SaveChangesAsync();
        }

        public async Task  DeleteProduitAsync(Produit produit)
        {
            milibooContext.Produits.Remove(produit);
            await milibooContext.SaveChangesAsync();
        }
        
    }
}
