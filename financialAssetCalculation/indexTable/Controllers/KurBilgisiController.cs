using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace indexTable.Controllers
{
    public class KurBilgisiController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public KurBilgisiController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }

        public async Task<IActionResult> Index()
        {
            // api adresi
            string apiLink = "https://testapi.finmaks.com/ExchangeRates?key=Finmaks123";
            //string apiLink = "https://testapi.finmaks.com/ExchangeRates?key=Finmaks123&startDate=2023-09-01&endDate=2023-09-05";

            using (var httpClient = new HttpClient())
            {
                // api'den gelen json verisini alıyor
                var kurBilgi = await httpClient.GetStringAsync(apiLink);

                // okunuyor
                var kurBilgisi = JObject.Parse(kurBilgi);

                // usd'nin kodu 1 o yüzden 1 numaralı veriyi buluyoruz
                var USD = kurBilgisi["ExchangeRates"].FirstOrDefault(k => (int)k["BaseCurrencyCode"] == 1);

                if (USD != null)
                {
                    var guncelKur = await _projeDbContext.kurBilgis.FirstOrDefaultAsync(k => k.Name == "USD");

                    if (guncelKur != null)
                    {
                        guncelKur.Tutar = Convert.ToDecimal(USD["CashExchangeRate"]);
                        guncelKur.Tarih = DateTime.Now;
                    }
                    else
                    {
                        var yeniKur = new kurBilgi
                        {
                            Name = "USD",
                            Tutar = Convert.ToDecimal(USD["CashExchangeRate"]),
                            Tarih = DateTime.Now
                        };
                        _projeDbContext.kurBilgis.Add(yeniKur);
                        
                    }
                    _projeDbContext.SaveChanges();
                    ViewBag.USD = USD["CashExchangeRate"];
                }
            }
            return View();
        }
    }
}