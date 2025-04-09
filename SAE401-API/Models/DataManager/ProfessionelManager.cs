using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class ProfessionelManager : IProfessionelRepository<Professionel>
    {
        readonly _DBMilibooContext milibooContext;

        public ProfessionelManager() { }

        public ProfessionelManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<Professionel?>> GetProfessionelByIdAsync(int id)
        {
            return await milibooContext.Professionels.FirstOrDefaultAsync(p => p.Idclient == id);
        }

        public async Task<Professionel> AddProfessionelAsync(Professionel pro)
        {
            await milibooContext.Professionels.AddAsync(pro);
            await milibooContext.SaveChangesAsync();
            return pro;
        }

        public async Task<Professionel> UpdateProfessionelAsync(Professionel entityToUpdate, ProfessionelDTO entity)
        {
            entityToUpdate.Idactivitepro = entity.Idactivitepro;
            entityToUpdate.Nomsociete = entity.Nomsociete;
            entityToUpdate.Numtva = entity.Numtva;

            milibooContext.Entry(entityToUpdate).State = EntityState.Modified;
            await milibooContext.SaveChangesAsync();
            return entityToUpdate;
        }
    }
}
