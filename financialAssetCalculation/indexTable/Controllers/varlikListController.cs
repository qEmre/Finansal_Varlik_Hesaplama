using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace indexTable.Controllers
{
    public class varlikListController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public varlikListController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }    

        public IActionResult varlikGetir()
        {
            List<varlikBilgi> varlikList = _projeDbContext.varlikBilgis.ToList();
            return View(varlikList);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var varlikBilgisi = _projeDbContext.varlikBilgis.FirstOrDefault(v => v.Id == id);

            if (varlikBilgisi == null)
            {
                return NotFound();
            }

            _projeDbContext.varlikBilgis.Remove(varlikBilgisi);
            _projeDbContext.SaveChanges();

            return RedirectToAction("varlikGetir", "varlikList");
        }
    }
}