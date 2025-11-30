using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTOs;

namespace BeFit.Controllers
{
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /TrainingSessions
        public async Task<IActionResult> Index()
        {
            // pokaż sesje aktualnie zalogowanego użytkownika, a jeśli niezalogowany - pustą listę
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // możesz zamiast tego przekierować do logowania: return Challenge();
                return View(Enumerable.Empty<TrainingSession>());
            }

            var sessions = await _context.TrainingSessions
                .Where(ts => ts.UserId == userId)
                .OrderByDescending(ts => ts.StartedAt)
                .AsNoTracking()
                .ToListAsync();

            return View(sessions);
        }

        // GET: /TrainingSessions/Create
        public IActionResult Create()
        {
            return View(new TrainingSessionCreateDto());
        }

        // POST: /TrainingSessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingSessionCreateDto dto)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Challenge(); // wymaga logowania
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var model = new TrainingSession
                {
                    StartedAt = dto.StartedAt,
                    EndedAt = dto.EndedAt,
                    UserId = userId
                };

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        // (opcjonalnie) Edit/Details/Delete możesz dodać analogicznie z kontrolą właściciela
    }
}