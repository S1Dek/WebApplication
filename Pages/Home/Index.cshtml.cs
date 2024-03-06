using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApplication1
{

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Kontrahenci> Kontrahent { get; set; }

    public async Task OnGetAsync()
    {
        Kontrahent = await _context.Kontrahent.ToListAsync();
    }
}
}