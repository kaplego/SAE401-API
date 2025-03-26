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

        public async Task AddProfessionelAsync(Professionel pro)
        {
            milibooContext.Professionels.Add(pro);
            await milibooContext.SaveChangesAsync();
        }

        public async Task UpdateProfessionelAsync(Professionel entityToUpdate, ProfessionelDTO entity)
        {
            entityToUpdate.Idactivitepro = entity.Idactivitepro;
            entityToUpdate.Nomsociete = entity.Nomsociete;
            entityToUpdate.Numtva = entity.Numtva;

            milibooContext.Entry(entityToUpdate).State = EntityState.Modified;
            await milibooContext.SaveChangesAsync();
        }
    }
}
