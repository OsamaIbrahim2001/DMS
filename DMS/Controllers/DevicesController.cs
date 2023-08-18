using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMS.Models;
using DMS.DTO;
using NuGet.Packaging;

namespace DMS.Controllers
{
    public class DevicesController : Controller
    {
        private readonly DataContext _context;

        public DevicesController(DataContext context)
        {
            _context = context;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            var devices=await _context.Devices.Include(e=>e.DeviceCategory).Include(e=>e.PropertyValues).ToListAsync();
            List<DevicesDTO> devicesDTOs = new();
            foreach (var device in devices)
            {
                DevicesDTO devicesDTO = new ();
                devicesDTO.ID = device.ID;
                devicesDTO.DeviceName = device.DeviceName;
                devicesDTO.DeviceCategory = device.DeviceCategory.CategoryName;
                devicesDTO.AcquisitionDate = device.AcquisitionDate;
                devicesDTO.Memo = device.Memo;
                foreach (var item in device.PropertyValues)
                {
                    var prop = _context.PropertyItems.Find(item.PropertyItemID).PropertyDescription;

                    devicesDTO.PropertiesValues.Add(prop,item.Value );
                }
                devicesDTOs.Add(devicesDTO);
            }

            return View(devicesDTOs);
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.DeviceCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        public async Task<IActionResult> Create()
        {
            var model = new DeviceViewModel();
            model.AvailableProperties = new List<PropertyItemDTO>();
           
            ViewData["DeviceCategoryID"] = new SelectList(_context.DeviceCategories, "ID", "CategoryName");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeviceViewModel model)
        {

                Device device = new()
                {
                    DeviceName=model.DeviceName,
                    AcquisitionDate=model.AcquisitionDate,
                    Memo=model.Memo,
                    DeviceCategoryID=model.DeviceCategoryID
                };
            for(int i=0; i<model.PropertyItemsID.Count;i++)
            {
                device.PropertyValues.Add(new PropertyValue{
                Devices=device,
                PropertyItemID = model.PropertyItemsID[i],
                Value = model.PropertyValues[i]
            });
            }
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Devices");
            }

     
        
        [HttpGet]
        //[Route("GetPropertiesForCategory/{categoryId}")]
        public IActionResult GetPropertiesForCategory(int categoryId)
        {
            var properties = _context.DeviceCategories
                .Include(dc => dc.Properties)
                .FirstOrDefault(dc => dc.ID == categoryId)?
                .Properties;
            List<PropertyItemDTO> propertiesDTO = new List<PropertyItemDTO>();
            foreach (var item in properties)
            {
                PropertyItemDTO propertyItemDTO = new()
                {
                    ID = item.ID,
                    PropertyDescription = item.PropertyDescription
                };
                propertiesDTO.Add(propertyItemDTO);
            }

            return PartialView("_PropertyValuesPartial", propertiesDTO);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var device = _context.Devices.Include(d => d.PropertyValues).FirstOrDefault(d => d.ID == id);
            if (device == null)
            {
                return NotFound();
            }
            var deviceCategory = await _context.DeviceCategories.Include(e => e.Properties).FirstOrDefaultAsync(e=>e.ID==device.DeviceCategoryID);    
            var model = new DeviceViewModel
            {
                DeviceID = device.ID,
                DeviceName = device.DeviceName,
                AcquisitionDate = device.AcquisitionDate,
                Memo = device.Memo,
                DeviceCategoryID=device.DeviceCategoryID
            };
            for(int i= 0; i< deviceCategory.Properties.Count;i++)
            {
                model.AvailableProperties.Add(new PropertyItemDTO {
                ID= deviceCategory.Properties[i].ID,
                PropertyDescription= deviceCategory.Properties[i].PropertyDescription
                });
                model.PropertyItemsID.Add(deviceCategory.Properties[i].ID);
                model.PropertyValues.Add(device.PropertyValues[i].Value);
            }
            var deviceCategories = _context.DeviceCategories.ToList();
            ViewData["DeviceCategoryID"] = new SelectList(deviceCategories, "ID", "CategoryName", device.DeviceCategoryID);
            return View(model);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeviceViewModel deviceViewModel)
        {
            if (id != deviceViewModel.DeviceID)
            {
                return NotFound();
            }


            try
            {

                Device device =await _context.Devices.Include(e=>e.PropertyValues).FirstOrDefaultAsync(e=>e.ID==id);
                device.DeviceName = deviceViewModel.DeviceName;
                device.AcquisitionDate = deviceViewModel.AcquisitionDate;
                device.Memo = deviceViewModel.Memo;
                device.DeviceCategoryID = deviceViewModel.DeviceCategoryID;
              
                for (int i = 0; i < deviceViewModel.PropertyItemsID.Count; i++)
                {
                    device.PropertyValues[i].Value = deviceViewModel.PropertyValues[i];
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Devices");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(deviceViewModel.DeviceID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }            
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Devices == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .Include(d => d.DeviceCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Devices == null)
            {
                return Problem("Entity set 'DataContext.Devices'  is null.");
            }
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
          return (_context.Devices?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
