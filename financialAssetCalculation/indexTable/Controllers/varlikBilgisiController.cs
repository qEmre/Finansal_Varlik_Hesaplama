using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace indexTable.Controllers
{
    public class varlikBilgisiController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public varlikBilgisiController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(varlikBilgi varlik)
        {
            if (ModelState.IsValid) // boş kayıt atılmaması için
            {
                varlikBilgi bilgi = new varlikBilgi()
                {
                    Tutari = varlik.Tutari,
                    Tarihi = varlik.Tarihi,
                    Name = varlik.Name
                };
                _projeDbContext.varlikBilgis.Add(bilgi);
            }
            _projeDbContext.SaveChanges();

            return View();
        } 
    }
}