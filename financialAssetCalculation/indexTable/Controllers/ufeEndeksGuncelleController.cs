using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace indexTable.Controllers
{
    public class ufeEndeksGuncelleController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public ufeEndeksGuncelleController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }

        public IActionResult endeksGuncelle(int? id)
        {
            var endeks = _projeDbContext.tufeEndeks.FirstOrDefault(e => e.Id == id);
            
            if (endeks == null)
            {
                return NotFound();
            }

            return View(endeks);
        }

        [HttpPost]
        public IActionResult Guncelle(ufeEndeks endeks)
        {
            var gelenEndeks = _projeDbContext.tufeEndeks.FirstOrDefault(e => e.Id == endeks.Id);

            if (gelenEndeks == null)
            {
                return NotFound(); 
            }

            gelenEndeks.endeksOrani = endeks.endeksOrani;
            gelenEndeks.dolarKuru = endeks.dolarKuru;
            gelenEndeks.Tarihi = endeks.Tarihi;

            _projeDbContext.SaveChanges();

            return RedirectToAction("endeksGetir", "ufeEndeksList"); 
        }
    }
}