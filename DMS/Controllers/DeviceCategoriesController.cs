using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMS.Models;
using DMS.DTO;

namespace DMS.Controllers
{
    public class DeviceCategoriesController : Controller
    {
        private readonly DataContext _context;

        public DeviceCategoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: DeviceCategories
        public async Task<IActionResult> Index()
        {
            try {
                var deviceCategories = await _context.DeviceCategories.Include(e => e.Properties).ToListAsync();
                List<DeviceCategoriesDTO> deviceCategoriesDTOs = new List<DeviceCategoriesDTO>();
                foreach (var item in deviceCategories)
                {
                    DeviceCategoriesDTO deviceCategoriesDTO = new();
                    deviceCategoriesDTO.ID = item.ID;
                    deviceCategoriesDTO.CategoryName = item.CategoryName;
                    foreach (var properity in item.Properties)
                    {
                        deviceCategoriesDTO.Properities.Add(properity.PropertyDescription);
                    }
                    deviceCategoriesDTOs.Add(deviceCategoriesDTO);
                }
               return View(deviceCategoriesDTOs);
            }
            catch
            {
               return Problem("Entity set 'DataContext.DeviceCategories'  is null.");
            }
        }

        // GET: DeviceCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeviceCategories == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // GET: DeviceCategories/Create
        public IActionResult Create()
        {
            var model = new DeviceCategoryViewModel
            {
                AvailableProperties = _context.PropertyItems.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeviceCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new DeviceCategory
                {
                    CategoryName = model.CategoryName
                };

                foreach (int propertyId in model.SelectedPropertyIds)
                {
                    newCategory.Properties.Add(_context.PropertyItems.Find(propertyId));
                }

                _context.DeviceCategories.Add(newCategory);
                _context.SaveChanges();

                return RedirectToAction("Index", "DeviceCategories"); // Redirect to some other page after creation
            }

            model.AvailableProperties = _context.PropertyItems.ToList();
            return View(model);
        }

        // POST: DeviceCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public IActionResult Edit(int id)
        {
            var category = _context.DeviceCategories.Include(dc => dc.Properties).FirstOrDefault(dc => dc.ID == id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new DeviceCategoryViewModel
            {
                ID = category.ID,
                CategoryName = category.CategoryName,
                AvailableProperties = _context.PropertyItems.ToList(),
                SelectedPropertyIds = category.Properties.Select(p => p.ID).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DeviceCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _context.DeviceCategories.Include(dc => dc.Properties).FirstOrDefault(dc => dc.ID == model.ID);

                if (category == null)
                {
                    return NotFound();
                }

                category.CategoryName = model.CategoryName;
                category.Properties.Clear();

                foreach (int propertyId in model.SelectedPropertyIds)
                {
                    category.Properties.Add(_context.PropertyItems.Find(propertyId));
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "DeviceCategories"); // Redirect to some other page after editing
            }

            model.AvailableProperties = _context.PropertyItems.ToList();
            return View(model);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeviceCategories == null)
            {
                return NotFound();
            }

            var deviceCategory = await _context.DeviceCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deviceCategory == null)
            {
                return NotFound();
            }

            return View(deviceCategory);
        }

        // POST: DeviceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeviceCategories == null)
            {
                return Problem("Entity set 'DataContext.DeviceCategories'  is null.");
            }
            var deviceCategory = await _context.DeviceCategories.FindAsync(id);
            if (deviceCategory != null)
            {
                _context.DeviceCategories.Remove(deviceCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceCategoryExists(int id)
        {
          return (_context.DeviceCategories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
