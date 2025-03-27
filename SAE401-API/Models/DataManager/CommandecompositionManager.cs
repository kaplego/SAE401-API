using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class CommandecompositionManager<TEntity> : ICommandecompositionRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public CommandecompositionManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }



        public async Task AddCommandecompositionAsync(TEntity entity)
        {
            if (entity is Commandecomposition commandecomposition)
            {
                _milibooContext.Commandecompositions.Add(commandecomposition);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }




    }
}
