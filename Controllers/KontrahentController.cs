using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1;

[Route("api/[controller]")]
[ApiController]
public class KontrahentController : ControllerBase
{
    private readonly AppDbContext dbcontext;

    // Konstruktor kontrolera, wstrzykujący kontekst bazy danych przez Dependency Injection
    public KontrahentController(AppDbContext context)
    {
        dbcontext = context;
    }

    // Endpoint do pobierania wszystkich kontrahentów
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Kontrahenci>>> GetKontrahenci()
    {
        return await dbcontext.Kontrahent.ToListAsync();
    }

    // Endpoint do dodawania nowego kontrahenta
    [HttpPost]
    public async Task<ActionResult<Kontrahenci>> AddKontrahent(Kontrahenci kontrahent)
    {
        // Dodaj kontrahenta do bazy danych
        dbcontext.Kontrahent.Add(kontrahent);
        await dbcontext.SaveChangesAsync();

        // Zwróć odpowiedź HTTP 201 Created z informacją o nowo utworzonym zasobie
        return CreatedAtAction(nameof(GetKontrahenci), new { id = kontrahent.Id }, kontrahent);
    }

    // Endpoint do aktualizacji danych istniejącego kontrahenta
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateKontrahent(int id, Kontrahenci kontrahent)
    {
        // Sprawdź, czy przekazane ID jest zgodne z ID kontrahenta w obiekcie
        if (id != kontrahent.Id)
        {
            return BadRequest();
        }

        // Ustaw stan obiektu na modyfikowany i zapisz zmiany w bazie danych
        dbcontext.Entry(kontrahent).State = EntityState.Modified;
        await dbcontext.SaveChangesAsync();

        // Zwróć odpowiedź - No Content
        return NoContent();
    }

    // Endpoint do usuwania kontrahenta
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKontrahent(int id)
    {
        // Znajdź kontrahenta o podanym ID
        var kontrahent = await dbcontext.Kontrahent.FindAsync(id);

        // Jeżeli kontrahent nie istnieje, zwróć odpowiedź HTTP 404 Not Found
        if (kontrahent == null)
        {
            return NotFound();
        }

        // Usuń kontrahenta z bazy danych i zapisz zmiany
        dbcontext.Kontrahent.Remove(kontrahent);
        await dbcontext.SaveChangesAsync();

        // Zwróć odpowiedź HTTP 204 No Content
        return NoContent();
    }
}
