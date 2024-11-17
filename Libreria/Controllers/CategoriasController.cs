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
    public class CategoriasController : Controller
    {
        private readonly LibreriaContext _context;

        public CategoriasController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CodigoCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoCategoria,Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(categoria);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Categoría creada con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }


        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoCategoria,Nombre")] Categoria categoria)
        {
            if (id != categoria.CodigoCategoria)
            {
                return Json(new { success = false, message = "El ID de la categoría no coincide." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Categoría editada con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }


        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CodigoCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var Categoria = await _context.Categorias.FindAsync(id);
                if (Categoria != null)
                {
                    _context.Categorias.Remove(Categoria);
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

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.CodigoCategoria == id);
        }
    }
}
