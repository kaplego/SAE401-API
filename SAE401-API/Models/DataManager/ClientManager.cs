using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataMethods;
using System.Security.Claims;
using System.Text;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SAE401_API.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SAE401_API.Models.DataManager
{
    public class ClientManager : IClientRepository<Client>
    {
        readonly _DBMilibooContext milibooContext;

        public ClientManager() { }

        public ClientManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }


        public async Task<Client?> GetClientByLoginAsync(string email, string password)
        {
            Client? client = await milibooContext.Clients
                .Include(c => c.HistoriquesNavigation).ThenInclude(h => h.ProduitNavigation)
                .ThenInclude(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(c => c.AimesNavigation).ThenInclude(a => a.ProduitNavigation)
                .ThenInclude(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(c => c.MessagesNavigation).Include(c => c.ProfessionelNavigation)
                .Include(c => c.AdressesNavigation).ThenInclude(a => a.VilleNavigation)
                .Include(c => c.AdressesNavigation).ThenInclude(a => a.DepartementNavigation)
                .Include(c => c.AdressesNavigation).ThenInclude(a => a.PayNavigation)
                .Include(c => c.CommandesNavigation)
                .Include(c => c.PaniersProduitNavigation).ThenInclude(p => p.ColorationNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(c => c.PaniersCompositionNavigation).ThenInclude(p => p.CompositionNavigation).ThenInclude(c => c.DetailsNavigation)
                .ThenInclude(p => p.ColorationNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .FirstOrDefaultAsync(c => c.Emailclient.ToLower() == email.ToLower());
            if (client == null) return null;
            string hash = client.Hashmdp.Replace(" ", "");
            if (BCrypt.Net.BCrypt.Verify(password, hash))
            {
                return client;
            }
            return null;
        }

        public async Task<ActionResult<Client?>> GetClientByIdAsync(int id)
        {
            return await milibooContext.Clients
                .Include(c => c.HistoriquesNavigation).ThenInclude(h => h.ProduitNavigation)
                .ThenInclude(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(c => c.AimesNavigation).ThenInclude(a => a.ProduitNavigation)
                .ThenInclude(p => p.ColorationsNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(c => c.MessagesNavigation).Include(c => c.ProfessionelNavigation)
                .Include(c => c.AdressesNavigation).ThenInclude(a => a.VilleNavigation)
                .Include(c => c.AdressesNavigation).ThenInclude(a => a.DepartementNavigation)
                .Include(c => c.AdressesNavigation).ThenInclude(a => a.PayNavigation)
                .Include(c => c.CommandesNavigation)
                .Include(c => c.PaniersProduitNavigation).ThenInclude(p => p.ColorationNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .Include(c => c.PaniersCompositionNavigation).ThenInclude(p => p.CompositionNavigation).ThenInclude(c => c.DetailsNavigation)
                .ThenInclude(p => p.ColorationNavigation).ThenInclude(c => c.PhotocolsNavigation).ThenInclude(p => p.PhotoNavigation)
                .FirstOrDefaultAsync(c => c.Idclient == id);
        }

        public async Task AddClientAsync(Client client)
        {
            milibooContext.Clients.Add(client);
            await milibooContext.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client, ClientDTO entity)
        {
            client.Nomclient = entity.Nomclient;
            client.Prenomclient = entity.Prenomclient;
            client.Civiliteclient = entity.Civiliteclient;
            client.Emailclient = entity.Emailclient;
            client.Telfixeclient = entity.Telfixeclient;
            client.Telportableclient = entity.Telportableclient;
            client.Datecreationcompte = entity.Datecreationcompte;
            if (entity.Hashmdp != null) client.Hashmdp = entity.Hashmdp;
            client.Pointfideliteclient = entity.Pointfideliteclient;
            client.Newslettermiliboo = entity.Newslettermiliboo;
            client.Newsletterpartenaires = entity.Newsletterpartenaires;

            milibooContext.Entry(client).State = EntityState.Modified;
            await milibooContext.SaveChangesAsync();
        }
    }
}
