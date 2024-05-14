using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace indexTable.Controllers
{
    public class varlikGuncelleController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public varlikGuncelleController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }

        public IActionResult varlikUpdate(int? id)
        {
            var varlik = _projeDbContext.varlikBilgis.FirstOrDefault(v => v.Id == id);
            if (varlik == null)
            {
                return NotFound();
            }
            return View(varlik);
        }

        [HttpPost]
        public IActionResult Update(varlikBilgi varlik)
        {
            var gelenVarlik = _projeDbContext.varlikBilgis.FirstOrDefault(v => v.Id == varlik.Id);

            if (gelenVarlik == null)
            {
                return NotFound();
            }

            gelenVarlik.Tutari = varlik.Tutari;
            gelenVarlik.Tarihi = varlik.Tarihi;
            gelenVarlik.Name = varlik.Name;

            _projeDbContext.SaveChanges();
            return RedirectToAction("varlikGetir", "varlikList");
        }
    }
}