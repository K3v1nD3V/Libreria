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
    public class LibroesController : Controller
    {
        private readonly LibreriaContext _context;

        public LibroesController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: Libroes
        public async Task<IActionResult> Index()
        {
            var libreriaContext = _context.Libros.Include(l => l.CodigoCategoriaNavigation).Include(l => l.NitEditorialNavigation);
            return View(await libreriaContext.ToListAsync());
        }

        // GET: Libroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.CodigoCategoriaNavigation)
                .Include(l => l.NitEditorialNavigation)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libroes/Create
        public IActionResult Create()
        {
            ViewData["CodigoCategoria"] = new SelectList(_context.Categorias, "CodigoCategoria", "CodigoCategoria");
            ViewData["NitEditorial"] = new SelectList(_context.Editoriales, "Nit", "Nit");
            return View();
        }

        // POST: Libroes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Isbn,Titulo,Descripcion,NombreAutor,Publicacion,FechaRegistro,CodigoCategoria,NitEditorial")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(libro);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Libro creado con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }


        // GET: Libroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["CodigoCategoria"] = new SelectList(_context.Categorias, "CodigoCategoria", "CodigoCategoria", libro.CodigoCategoria);
            ViewData["NitEditorial"] = new SelectList(_context.Editoriales, "Nit", "Nit", libro.NitEditorial);
            return View(libro);
        }

        // POST: Libroes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Isbn,Titulo,Descripcion,NombreAutor,Publicacion,FechaRegistro,CodigoCategoria,NitEditorial")] Libro libro)
        {
            if (id != libro.Isbn)
            {
                return Json(new { success = false, message = "El ID del libro no coincide." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Libro editado con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }


        // GET: Libroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.CodigoCategoriaNavigation)
                .Include(l => l.NitEditorialNavigation)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libroes/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var libro = await _context.Libros.FindAsync(id);
                if (libro != null)
                {
                    _context.Libros.Remove(libro);
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




        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Isbn == id);
        }
    }
}
