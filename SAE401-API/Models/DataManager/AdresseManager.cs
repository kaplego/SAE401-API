using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class AdresseManager<TEntity> : IAdresseRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public AdresseManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetAdresseByIdAsync(int idadresse)
        {
            var adresse = await _milibooContext.Adresses
                .Include(a => a.ClientNavigation)
                .Include(a => a.DepartementNavigation)
                .Include(a => a.PayNavigation)
                .Include(a => a.VilleNavigation)
                .FirstOrDefaultAsync(a => a.Idadresse == idadresse);

            return adresse != null ? new ActionResult<TEntity>((TEntity)(object)adresse) : new NotFoundResult();
        }

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

        public async Task UpdateAdresseAsync(Adresse adresse, TEntity entity)
        {
            if (entity is Adresse updatedAdresse)
            {
                adresse.Nomadresse = updatedAdresse.Nomadresse;
                adresse.Numerorue = updatedAdresse.Numerorue;
                adresse.Nomrue = updatedAdresse.Nomrue;
                adresse.Codepostaladresse = updatedAdresse.Codepostaladresse;
                adresse.Idpays = updatedAdresse.Idpays;
                adresse.Iddepartement = updatedAdresse.Iddepartement;
                adresse.Codeinsee = updatedAdresse.Codeinsee;
            }

            _milibooContext.Entry(adresse).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
        }

        public async Task DeleteAdresseAsync(Adresse adresse)
        {
            _milibooContext.Adresses.Remove(adresse);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
