using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace indexTable.Controllers
{
    public class ufeEndeksListController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public ufeEndeksListController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }

        public IActionResult endeksGetir()
        {
            List<ufeEndeks> endeksList = _projeDbContext.tufeEndeks.ToList();
            return View(endeksList);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var endeksBilgi = _projeDbContext.tufeEndeks.FirstOrDefault(e => e.Id == id);

            if (endeksBilgi == null)
            {
                return NotFound();
            }

            _projeDbContext.tufeEndeks.Remove(endeksBilgi);
            _projeDbContext.SaveChanges();

            return RedirectToAction("endeksGetir", "ufeEndeksList");
        }
    }
}