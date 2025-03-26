using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class AdresseManager<TEntity> : IAdresseRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public AdresseManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        // Méthode pour récupérer une adresse par son Id
        public async Task<ActionResult<TEntity?>> GetAdresseByIdAsync(int idadresse)
        {
            var adresse = await _milibooContext.Adresses
                .Include(a => a.ClientNavigation) // Inclure le client lié
                .Include(a => a.VilleNavigation)  // Inclure la ville liée
                .FirstOrDefaultAsync(a => a.Idadresse == idadresse);

            return adresse != null ? new ActionResult<TEntity>((TEntity)(object)adresse) : new NotFoundResult();
        }

        // Méthode pour ajouter une nouvelle adresse
        public async Task AddAdresseAsync(TEntity entity)
        {
            if (entity is Adresse adresse)
            {
                _milibooContext.Adresses.Add(adresse);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        // Méthode pour mettre à jour une adresse
        public async Task UpdateAdresseAsync(Adresse adresse, TEntity entity)
        {
            if (entity is Adresse updatedAdresse)
            {
                adresse.Nomadresse = updatedAdresse.Nomadresse;
                adresse.Nomrue = updatedAdresse.Nomrue;
                adresse.Codepostaladresse = updatedAdresse.Codepostaladresse;
                // Ajouter ici d'autres propriétés à mettre à jour

                _milibooContext.Entry(adresse).State = EntityState.Modified;
                await _milibooContext.SaveChangesAsync();
            }
        }

        // Méthode pour supprimer une adresse
        public async Task DeleteAdresseAsync(Adresse adresse)
        {
            _milibooContext.Adresses.Remove(adresse);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
