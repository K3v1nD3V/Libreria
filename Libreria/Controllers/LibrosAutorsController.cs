using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Libreria.Models;

namespace Libreria.Controllers
{
    public class LibrosAutorsController : Controller
    {
        private readonly LibreriaContext _context;

        public LibrosAutorsController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: LibrosAutors
        public async Task<IActionResult> Index()
        {
            var libreriaContext = _context.LibrosAutors.Include(l => l.IdAutorNavigation).Include(l => l.IdLibroNavigation);
            return View(await libreriaContext.ToListAsync());
        }

        // GET: LibrosAutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutor = await _context.LibrosAutors
                .Include(l => l.IdAutorNavigation)
                .Include(l => l.IdLibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (librosAutor == null)
            {
                return NotFound();
            }

            return View(librosAutor);
        }

        // GET: LibrosAutors/Create
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_context.Autors, "IdAutor", "IdAutor");
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Isbn", "Isbn");
            return View();
        }

        // POST: LibrosAutors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdLibro,IdAutor")] LibrosAutor librosAutor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(librosAutor);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Asociación de libro y autor creada con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }


        // GET: LibrosAutors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutor = await _context.LibrosAutors.FindAsync(id);
            if (librosAutor == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autors, "IdAutor", "IdAutor", librosAutor.IdAutor);
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Isbn", "Isbn", librosAutor.IdLibro);
            return View(librosAutor);
        }

        // POST: LibrosAutors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdLibro,IdAutor")] LibrosAutor librosAutor)
        {
            if (id != librosAutor.Id)
            {
                return Json(new { success = false, message = "El ID de la asociación no coincide." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librosAutor);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Asociación de libro y autor editada con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }

        // GET: LibrosAutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librosAutor = await _context.LibrosAutors
                .Include(l => l.IdAutorNavigation)
                .Include(l => l.IdLibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (librosAutor == null)
            {
                return NotFound();
            }

            return View(librosAutor);
        }

        // POST: LibrosAutors/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var LibroAutor = await _context.LibrosAutors.FindAsync(id);
                if (LibroAutor != null)
                {
                    _context.LibrosAutors.Remove(LibroAutor);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Registro no encontrado" });
            }
            catch (DbUpdateException dbEx)
            {
                // Verifica si la excepción es causada por una restricción de clave foránea
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    return Json(new { success = false, message = "No se puede eliminar el registro porque está siendo referenciado en otra parte del sistema." });
                }
                // En otros casos, devuelve un mensaje genérico
                return Json(new { success = false, message = "Ocurrió un error al eliminar el registro. Por favor, inténtalo de nuevo más tarde." });
            }
            catch (Exception ex)
            {
                // Maneja otras excepciones
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = innerExceptionMessage });
            }
        }

        private bool LibrosAutorExists(int id)
        {
            return _context.LibrosAutors.Any(e => e.Id == id);
        }
    }
}
