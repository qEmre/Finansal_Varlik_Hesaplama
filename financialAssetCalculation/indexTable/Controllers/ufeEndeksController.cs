using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace indexTable.Controllers
{
    public class ufeEndeksController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public ufeEndeksController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ufeEndeks endeks)
        {
            if (ModelState.IsValid) // boş kayıt atılmaması için
            {
                ufeEndeks endeksBilgi = new ufeEndeks()
                {
                    endeksOrani = endeks.endeksOrani,
                    dolarKuru = endeks.dolarKuru,
                    Tarihi = endeks.Tarihi
                };
                _projeDbContext.tufeEndeks.Add(endeksBilgi);
            }
            _projeDbContext.SaveChanges();

            return View();
        }
    }
}