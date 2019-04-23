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
    public class MoviesController : Controller
    {
        private readonly DBContext _context;

        public MoviesController(DBContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchMovieName, string searchGenreName, string searchAuthorName, bool productionDate, int? page)
        {
            int pages;
            const int maxElementsOnAPage = 5;

            if (searchAuthorName != null || searchGenreName != null || searchMovieName != null)
            {
            } else
            {
                if (productionDate == true)
                {
                    var movies = (from m in _context.Movie.Include(m => m.Author).Include(m => m.Genres) orderby m.ProductionDate select m).ToList();
                    ViewBag.productionDate = false;
                    return View(movies);
                } else if (productionDate == false)
                {
                    var movies = (from m in _context.Movie.Include(m => m.Author).Include(m => m.Genres) orderby m.ProductionDate descending select m).ToList();
                    ViewBag.productionDate = true;
                    return View(movies);
                }
            }
            
            if (searchMovieName != null)
            {
                var movies = (from m in _context.Movie.Include(m => m.Author).Include(m => m.Genres) where m.Title.Contains(searchMovieName) select m).ToList();
                return View(movies);
            }

            if (searchGenreName != null)
            {
                int genreId = (from g in _context.Genres where g.Name.Contains(searchGenreName) select g.GenreId).SingleOrDefault();

                var genres = (from m in _context.Movie.Include(m => m.Author).Include(m => m.Genres) where m.GenreId.Equals(genreId) select m).ToList();
                return View(genres);
            }

            if (searchAuthorName != null)
            {
                if (searchAuthorName.Split(' ').Length > 1)
                {
                    var auth = searchAuthorName.Split(' ');
                    int[] authorsId = (from a in _context.Author
                                       where
                                       a.Name.ToLower().Contains(auth[0].ToLower()) &&
                                       a.Surname.ToLower().Contains(auth[1].ToLower()) ||
                                       a.Name.ToLower().Contains(auth[1].ToLower()) &&
                                       a.Surname.ToLower().Contains(auth[0].ToLower())
                                       select a.AuthorId
                        ).ToArray();

                    List<Movie> m = new List<Movie>();

                    for (int i = 0; i < authorsId.Length; i++)
                    {
                        var authors = _context.Movie.Include
                                         (a => a.Author)
                                         .Include(a => a.Genres)
                                         .Where(a => a.AuthorId.Equals(authorsId[i]))
                                         .Select(a => a).ToList();

                        foreach (var item in authors)
                        {
                            if (item != null) { m.Add(item); }
                        }
                        authors = null;
                    }

                    return View(m.ToList());
                }
                else if (searchAuthorName.Split(' ').Length == 1)
                {
                    var movieAuthors = _context.Movie.Include
                    (a => a.Author)
                    .Include(a => a.Genres)
                    .Where(a => a.Author.Name.ToLower().Equals(searchAuthorName.ToLower()) || a.Author.Surname.ToLower().Equals(searchAuthorName.ToLower()))
                    .Select(a => a).ToList();
                    return View(movieAuthors);
                }
            }

            var dBContext = _context.Movie.Include(m => m.Author).Include(m => m.Genres);
            pages = dBContext.Count() / maxElementsOnAPage;
            ViewBag.pages = pages;
            return View(await dBContext.ToListAsync());
            //return View(await movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Author)
                .Include(m => m.Genres)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId","FullName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,ProductionDate,MovieDescription,GenreId,AuthorId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", movie.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", movie.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,ProductionDate,MovieDescription,GenreId,AuthorId")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "AuthorId", "AuthorId", movie.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Author)
                .Include(m => m.Genres)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }

        public async Task<IActionResult> SearchByDate(DateTime searchByDateFrom, DateTime searchByDateTo)
        {
            List<Movie> dBContext = new List<Movie>();

            if (searchByDateFrom != null && searchByDateTo.Year == 0001)
            {
                 dBContext = await _context.Movie.Include(m => m.Author).Include(m => m.Genres).Where(m => m.ProductionDate >= searchByDateFrom).OrderBy(m => m.ProductionDate).ToListAsync();
                 return View("Index", dBContext);
            }

            if (searchByDateFrom.Year == 0001 && searchByDateTo != null)
            {
                 dBContext = await _context.Movie.Include(m => m.Author).Include(m => m.Genres).Where(m => m.ProductionDate <= searchByDateTo).OrderBy(m => m.ProductionDate).ToListAsync();
                 return View("Index", dBContext);
            }
            dBContext = await _context.Movie.Include(m => m.Author).Include(m => m.Genres).Where(m => m.ProductionDate >= searchByDateFrom && m.ProductionDate <= searchByDateTo).OrderBy(m => m.ProductionDate).ToListAsync();
            return View("Index", dBContext);
            //return View("Index");
        }

    }
}
