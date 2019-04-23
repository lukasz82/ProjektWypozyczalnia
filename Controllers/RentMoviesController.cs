using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class RentMoviesController : Controller
    {
        private readonly DBContext _context;

        public RentMoviesController(DBContext context)
        {
            _context = context;
        }

        // GET: RentMovies
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: RentMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentMovie = await _context.RentMovie
                .Include(r => r.Movie)
                .SingleOrDefaultAsync(m => m.RentMovieId == id);
            if (rentMovie == null)
            {
                return NotFound();
            }

            return View(rentMovie);
        }

        // GET: RentMovies/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Title");
            return View();
        }

        // POST: RentMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentMovieId,RentDate,RentDays,DayRentPrice,MovieId")] RentMovie rentMovie)
        {

            if (ModelState.IsValid)
            {
                rentMovie.IsRent = true;
                _context.Add(rentMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowRentedMovies));
            }
            //ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Name", rentMovie.MovieId);
            return View("ShowRentedMovies");
        }

        // GET: RentMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentMovie = await _context.RentMovie.SingleOrDefaultAsync(m => m.RentMovieId == id);
            if (rentMovie == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "MovieId", rentMovie.MovieId);
            return View(rentMovie);
        }

        // POST: RentMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentMovieId,IsRent,RentDate,RentDays,DayRentPrice,MovieId")] RentMovie rentMovie)
        {
            if (id != rentMovie.RentMovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentMovieExists(rentMovie.RentMovieId))
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
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "MovieId", rentMovie.MovieId);
            return View(rentMovie);
        }

        // GET: RentMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentMovie = await _context.RentMovie
                .Include(r => r.Movie)
                .SingleOrDefaultAsync(m => m.RentMovieId == id);
            if (rentMovie == null)
            {
                return NotFound();
            }

            return View(rentMovie);
        }

        // POST: RentMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentMovie = await _context.RentMovie.SingleOrDefaultAsync(m => m.RentMovieId == id);
            _context.RentMovie.Remove(rentMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentMovieExists(int id)
        {
            return _context.RentMovie.Any(e => e.RentMovieId == id);
        }

        public async Task<IActionResult> ShowRentedMovies()
        {
            var dBContext = _context.RentMovie.Include(r => r.Movie);
            return View(await dBContext.ToListAsync());
        }
    }
}
