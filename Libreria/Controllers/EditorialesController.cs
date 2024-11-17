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
    public class EditorialesController : Controller
    {
        private readonly LibreriaContext _context;

        public EditorialesController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: Editoriales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Editoriales.ToListAsync());
        }

        // GET: Editoriales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.Nit == id);
            if (editoriale == null)
            {
                return NotFound();
            }

            return View(editoriale);
        }

        // GET: Editoriales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nit,Nombres,Telefono,Direccion,Email,Sitioweb")] Editoriale editoriale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(editoriale);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Editorial creada con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }


        // GET: Editoriales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales.FindAsync(id);
            if (editoriale == null)
            {
                return NotFound();
            }
            return View(editoriale);
        }

        // POST: Editoriales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nit,Nombres,Telefono,Direccion,Email,Sitioweb")] Editoriale editoriale)
        {
            if (id != editoriale.Nit)
            {
                return Json(new { success = false, message = "El ID de la editorial no coincide." });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editoriale);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "¡Editorial editada con éxito!" });
                }
                catch (Exception ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return Json(new { success = false, message = innerExceptionMessage });
                }
            }
            return Json(new { success = false, message = "Datos del modelo no válidos." });
        }


        // GET: Editoriales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.Nit == id);
            if (editoriale == null)
            {
                return NotFound();
            }

            return View(editoriale);
        }

        // POST: Editoriales/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var Editorial = await _context.Editoriales.FindAsync(id);
                if (Editorial != null)
                {
                    _context.Editoriales.Remove(Editorial);
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


        private bool EditorialeExists(int id)
        {
            return _context.Editoriales.Any(e => e.Nit == id);
        }
    }
}
