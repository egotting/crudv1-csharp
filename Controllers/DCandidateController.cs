using Crudv1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crudv1.Controllers;

[Route(("v1"))] // http:localhost:8080/v1/candidates
[ApiController]
public class DCandidateController : ControllerBase
{
  private readonly DonationDBContext _context;

  public DCandidateController(DonationDBContext context)
  {
    _context = context;
  }

  // GET: api/DCandidate
  [HttpGet]
  [Route("getcandidates")]                                                                 // Note:
  public async Task<ActionResult<IEnumerable<DCandidate>>> GetDCandidates() // "Task" significa uma operacao assincrona que vai ser executada em segundo plano 
                                                                            // "ActionResult" significa que esse metodo pode retorna diferentes tipos de acões, como resposta HTTP, redirecioamento entre outros...
                                                                            // como um OkResult() 200 ou NotFoudResult 404.
                                                                            // "IEnumerable<T>" É uma sequencia de elementos no caso o T seria o seu Model aonde tem as tabelas.
                                                                            // E os tres juntos significa que vai ser uma acão assincrona que pode fazer uma busca no db de forma assincrona que vai resulta em uma busca de um o mais elementos
  {
    return await _context.DCandidates.ToListAsync(); // retornando o os elementos em uma lista assincrona
  }




  [HttpGet]
  [Route("candidates/{id}")]
  public async Task<ActionResult<DCandidate>> GetDCandidates(Guid id)
  {
    var dCandidate = await _context.DCandidates.FindAsync(id);

    if (id != dCandidate.Id)
    {
      return NotFound();
    }
    return dCandidate;
  }


  [HttpPut]
  [Route("candidate/{id}")]
  public async Task<IActionResult> PutDCandidates(Guid id, DCandidate dcandidate)
  {
    dcandidate.Id = id;

    if (id != dcandidate.Id)
    {
      return BadRequest();
    }

    _context.Entry(dcandidate).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!DCandidateExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }
    return NoContent();
  }

  [HttpDelete]
  [Route("candidate/{id}")]
  public async Task<ActionResult<DCandidate>> DeleteDCandidante(Guid id)
  {
    var dcandidate = await _context.DCandidates.FindAsync(id);

    if (dcandidate == null)
    {
      return NotFound();
    }

    _context.DCandidates.Remove(dcandidate);
    await _context.SaveChangesAsync();

    return dcandidate;
  }


  [HttpPost]
  [Route("candidate")]
  public async Task<ActionResult<DCandidate>> PostDCandidate(DCandidate dcandidate)
  {
    _context.DCandidates.Add(dcandidate);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetDCandidate", new { id = dcandidate.Id }, dcandidate);
  }



  private bool DCandidateExists(Guid id)
  {
    return _context.DCandidates.Any(e => e.Id == id);
  }
}
