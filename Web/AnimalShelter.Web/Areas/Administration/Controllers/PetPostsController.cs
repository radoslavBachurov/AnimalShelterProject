namespace AnimalShelter.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Common;
    using AnimalShelter.Data;
    using AnimalShelter.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class PetPostsController : AdministrationController
    {
        private readonly ApplicationDbContext _context;

        public PetPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/PetPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PetPosts.Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/PetPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petPost = await _context.PetPosts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petPost == null)
            {
                return NotFound();
            }

            return View(petPost);
        }

        // GET: Administration/PetPosts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Administration/PetPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Name,UserId,Likes,Location,Sex,Type,PetStatus,IsApproved,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] PetPost petPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", petPost.UserId);
            return View(petPost);
        }

        // GET: Administration/PetPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petPost = await _context.PetPosts.FindAsync(id);
            if (petPost == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", petPost.UserId);
            return View(petPost);
        }

        // POST: Administration/PetPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,Name,UserId,Likes,Location,Sex,Type,PetStatus,IsApproved,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] PetPost petPost)
        {
            if (id != petPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetPostExists(petPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", petPost.UserId);
            return View(petPost);
        }

        // GET: Administration/PetPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petPost = await _context.PetPosts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petPost == null)
            {
                return NotFound();
            }

            return View(petPost);
        }

        // POST: Administration/PetPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petPost = await _context.PetPosts.FindAsync(id);
            _context.PetPosts.Remove(petPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetPostExists(int id)
        {
            return _context.PetPosts.Any(e => e.Id == id);
        }
    }
}
