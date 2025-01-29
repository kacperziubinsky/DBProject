using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly DBProjectContext _context;

    public ClientController(DBProjectContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        return _context.Clients.ToList();
    }

    [HttpGet]
    [Route("{clientID}")]
    public async Task<ActionResult<Client>> GetClient(string clientID)
    {
        var client = await _context.Clients.FindAsync(clientID);
        if(client == null) return NotFound( new {Message = "Client not found"});
        return client;
    }

    [HttpPost]
    public async Task<ActionResult<Client>> PostClient(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetClient), new { clientID = client.ClientID }, client);
    }



    [HttpDelete]
    [Route("{clientID}")]
    public async Task<ActionResult<Client>> DeleteClient(string clientID)
    {
        var client = await _context.Clients.FindAsync(clientID);
        
        if(client == null) return NotFound(new {Message = "Client not found"});
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        
        return Ok(new {Message = "Client deleted"});
    }
}