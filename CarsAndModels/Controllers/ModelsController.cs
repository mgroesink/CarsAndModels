using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarsAndModels.Models;

namespace CarsAndModels.Controllers
{
    public class ModelsController : Controller
    {
        private readonly CarDbContext _context;

        public ModelsController(CarDbContext context)
        {
            _context = context;
        }

        // GET: Models
        public async Task<IActionResult> Index()
        {
            var carDbContext = _context.CarModels.Include(c => c.CarBrand);
            return View(await carDbContext.ToListAsync());
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .Include(c => c.CarBrand)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: Models/Create
        public IActionResult Create()
        {
            // Maak een lijst van SelectListItem-objecten
            List<SelectListItem> items = new List<SelectListItem>();

            // Voeg eerst het item "Selecteer merk" toe
            items.Add(new SelectListItem { Text = "Selecteer merk", Value = "" });

            // Voeg vervolgens de merknamen toe
            foreach (var brand in _context.CarBrands.OrderBy(b=>b.BrandName))
            {
                items.Add(new SelectListItem { Text = brand.BrandName, Value = brand.BrandId.ToString() });
            }

            ViewData["BrandId"] = new SelectList(items, "Value", "Text");
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,ModelName,BrandId,ImageUrl")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Maak een lijst van SelectListItem-objecten
            List<SelectListItem> items = new List<SelectListItem>();

            // Voeg eerst het item "Selecteer merk" toe
            items.Add(new SelectListItem { Text = "Selecteer merk", Value = "" });

            // Voeg vervolgens de merknamen toe
            foreach (var brand in _context.CarBrands.OrderBy(b=>b.BrandName))
            {
                items.Add(new SelectListItem { Text = brand.BrandName, Value = brand.BrandId.ToString() });
            }

            ViewData["BrandId"] = new SelectList(items, "Value", "Text", carModel.BrandId);
            return View(carModel);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.CarBrands, "BrandId", "BrandName", carModel.BrandId);
            return View(carModel);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,ModelName,BrandId")] CarModel carModel)
        {
            if (id != carModel.ModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.ModelId))
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
            ViewData["BrandId"] = new SelectList(_context.CarBrands, "BrandId", "BrandName", carModel.BrandId);
            return View(carModel);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .Include(c => c.CarBrand)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel != null)
            {
                _context.CarModels.Remove(carModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.ModelId == id);
        }
    }
}
