using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.DTOs;

namespace BeFit.Controllers
{
    public class PerformedExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PerformedExercisesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PerformedExercises
        public async Task<IActionResult> Index()
        {
            var list = await _context.Set<PerformedExercise>()
                .Include(p => p.ExerciseType)
                .Include(p => p.TrainingSession)
                .AsNoTracking()
                .ToListAsync();
            return View(list);
        }

        // GET: PerformedExercises/Create
        public IActionResult Create()
        {
            PopulateSelectLists();
            return View(new PerformedExerciseCreateDto());
        }

        // POST: PerformedExercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PerformedExerciseCreateDto dto)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Challenge(); // wymaga logowania
            }

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                var model = new PerformedExercise
                {
                    TrainingSessionId = dto.TrainingSessionId,
                    ExerciseTypeId = dto.ExerciseTypeId,
                    Sets = dto.Sets,
                    RepsPerSet = dto.RepsPerSet,
                    LoadKg = dto.LoadKg,
                    UserId = userId
                };

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateSelectLists();
            return View(dto);
        }

        // GET: PerformedExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var performed = await _context.Set<PerformedExercise>().FindAsync(id);
            if (performed == null) return NotFound();

            // tutaj możesz mapować na DTO jeśli chcesz edytować przez DTO, ale zostawmy to na później
            PopulateSelectLists(performed);
            return View(performed); // zakładamy że masz widok Edytuj dla modelu PerformedExercise
        }

        // POST: PerformedExercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PerformedExercise performedExercise)
        {
            if (id != performedExercise.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performedExercise);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Set<PerformedExercise>().Any(e => e.Id == performedExercise.Id))
                        return NotFound();
                    throw;
                }
            }

            PopulateSelectLists(performedExercise);
            return View(performedExercise);
        }

        private void PopulateSelectLists(PerformedExercise? model = null)
        {
            ViewData["ExerciseTypeId"] = new SelectList(
                _context.Set<ExerciseType>().AsNoTracking().OrderBy(t => t.Name),
                "Id",
                "Name",
                model?.ExerciseTypeId);

            var sessions = _context.Set<TrainingSession>()
                .AsNoTracking()
                .OrderByDescending(s => s.StartedAt)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StartedAt.ToString("yyyy-MM-dd HH:mm") + " - " + s.EndedAt.ToString("HH:mm")
                })
                .ToList();

            if (model != null)
            {
                foreach (var si in sessions)
                {
                    if (int.TryParse(si.Value, out var id) && id == model.TrainingSessionId)
                        si.Selected = true;
                }
            }

            ViewData["TrainingSessionId"] = sessions;
        }
    }
}