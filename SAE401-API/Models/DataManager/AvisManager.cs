using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataMethods;
using System.Security.Claims;
using System.Text;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.DataManager
{
    public class AvisManager : IAvisRepository<Avisproduit>
    {
        readonly _DBMilibooContext milibooContext;

        public AvisManager() { }

        public AvisManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<Avisproduit?>> GetAvisByIdAsync(int id)
        {
            return await milibooContext.Avisproduits
                .Include(a => a.PhotoavisNavigation)
                .Include(a => a.SignalementsNavigation)
                .FirstOrDefaultAsync(c => c.Idavis == id);
        }

        public async Task AddAvisAsync(Avisproduit avis)
        {
            milibooContext.Avisproduits.Add(avis);
            await milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAvisAsync(Avisproduit avis)
        {
            foreach (Photoavi pa in avis.PhotoavisNavigation)
            {
                milibooContext.Photoavis.Remove(pa);
            }
            foreach (Signalementavi sga  in avis.SignalementsNavigation)
            {
                milibooContext.Signalementavis.Remove(sga);
            }
            milibooContext.Avisproduits.Remove(avis);
            await milibooContext.SaveChangesAsync();
        }
    }
}
