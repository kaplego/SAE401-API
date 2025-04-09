using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
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

        public async Task<Adresse> AddAdresseAsync(TEntity entity)
        {
            if (entity is Adresse adresse)
            {
                await _milibooContext.Adresses.AddAsync(adresse);
                await _milibooContext.SaveChangesAsync();
                return adresse;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task<Adresse> UpdateAdresseAsync(Adresse entity, AdresseDTO adresseDTO)
        {
            if (entity is Adresse adr)
            {
                adr.Nomadresse = adresseDTO.Nomadresse;
                adr.Numerorue = adresseDTO.Numerorue;
                adr.Nomrue = adresseDTO.Nomrue;
                adr.Codepostaladresse = adresseDTO.Codepostaladresse;
                adr.Idpays = adresseDTO.Idpays;
                adr.Iddepartement = adresseDTO.Iddepartement;
                adr.Codeinsee = adresseDTO.Codeinsee;
            }

            _milibooContext.Entry(entity).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAdresseAsync(Adresse adresse)
        {
            _milibooContext.Adresses.Remove(adresse);
            await _milibooContext.SaveChangesAsync();
        }
    }
}
