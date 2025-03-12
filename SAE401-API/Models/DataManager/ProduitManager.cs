using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;





namespace SAE401_API.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        readonly _DBMilibooContext milibooContext;

        public ProduitManager() { }

        public ProduitManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Produit>>>GetAllAsync()
        {
            return await milibooContext.Produits.ToListAsync();
        }

        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            return await milibooContext.Produits.FirstOrDefaultAsync(p => p.Idproduit == id);
        }

        public async Task AddAsync(Produit entity)
        {
            await milibooContext.Produits.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Produit produit,Produit entity)
        {
            milibooContext.Entry(produit).State = EntityState.Modified;
            produit.Idproduit = entity.Idproduit;
            produit.Idtypeproduit = entity.Idtypeproduit;
            produit.Idpays = entity.Idpays;
            produit.Nomproduit = entity.Nomproduit;
            produit.Sourcenotice = entity.Sourcenotice;
            produit.Sourceaspecttechnique = entity.Sourceaspecttechnique;
            produit.Delailivraison = entity.Delailivraison;
            produit.Coutlivraison = entity.Coutlivraison;
            produit.Nbpaiementmax = entity.Nbpaiementmax;
            await milibooContext.SaveChangesAsync();
        }

        public async Task  DeleteAsync(Produit produit)
        {
            milibooContext.Produits.Remove(produit);
            await milibooContext.SaveChangesAsync();
        }




    }
}
