using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UspgPOS.Data;
using UspgPOS.Models;

namespace UspgPOS.Controllers
{
    public class DetallesVentasController : Controller
    {
        private readonly AppDbContext _context;

        public DetallesVentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DetallesVentas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DetallesVentas.Include(d => d.Producto).Include(d => d.Venta);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DetallesVentas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesVenta == null)
            {
                return NotFound();
            }

            return View(detallesVenta);
        }

        // GET: DetallesVentas/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id");
            return View();
        }

        // POST: DetallesVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VentaId,ProductoId,Cantidad,CantidadTotal")] DetallesVenta detallesVenta)
        {
            _context.Add(detallesVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    _context.Add(detallesVenta);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detallesVenta.ProductoId);
            //ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detallesVenta.VentaId);
            //return View(detallesVenta);
        }

        // GET: DetallesVentas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas.FindAsync(id);
            if (detallesVenta == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detallesVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detallesVenta.VentaId);
            return View(detallesVenta);
        }

        // POST: DetallesVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,VentaId,ProductoId,Cantidad,CantidadTotal")] DetallesVenta detallesVenta)
        {
            if (id != detallesVenta.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(detallesVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesVentaExists(detallesVenta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detallesVenta.ProductoId);
            //ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detallesVenta.VentaId);
            //return View(detallesVenta);
        }

        // GET: DetallesVentas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesVenta == null)
            {
                return NotFound();
            }

            return View(detallesVenta);
        }

        // POST: DetallesVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var detallesVenta = await _context.DetallesVentas.FindAsync(id);
            if (detallesVenta != null)
            {
                _context.DetallesVentas.Remove(detallesVenta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesVentaExists(long? id)
        {
            return _context.DetallesVentas.Any(e => e.Id == id);
        }
    }
}
