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
    public class AutorsController : Controller
    {
        private readonly LibreriaContext _context;

        public AutorsController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: Autors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autors.ToListAsync());
        }

        // GET: Autors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Autors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutor,Nombre,Apellido,Nacionalidad")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(autor);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Autor creado con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Autors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAutor,Nombre,Apellido,Nacionalidad")] Autor autor)
        {
            if (id != autor.IdAutor)
            {
                return Json(new { success = false, message = "El ID del autor no coincide." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Autor editado con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autors/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var Autor = await _context.Autors.FindAsync(id);
                if (Autor != null)
                {
                    _context.Autors.Remove(Autor);
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

        private bool AutorExists(int id)
        {
            return _context.Autors.Any(e => e.IdAutor == id);
        }
    }
}
