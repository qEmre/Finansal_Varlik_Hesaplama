using indexTable.DataLayer;
using indexTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace indexTable.Controllers
{
    public class hesaplamaController : Controller
    {
        private readonly ProjeDbContext _projeDbContext;

        public hesaplamaController(ProjeDbContext projeDbContext)
        {
            _projeDbContext = projeDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(List<int> secilivarliklar)
        {
            //viewdan gelen varlik bilgilerini hepsini al ve liste yap
            List<varlikBilgi> seciliVarlik = _projeDbContext.varlikBilgis.Where(v => secilivarliklar.Contains(v.Id)).ToList();
            //veri tabanında ki tüm endeks verilerini listele
            List<ufeEndeks> endeksBilgileri = _projeDbContext.tufeEndeks.ToList();
            // hesaplama yapılan verileri saklamak için oluşturulan sınıf
            List<hesaplanmisOranlar> hesaplanmisVarliklar = new List<hesaplanmisOranlar>();

            foreach (var varlik in seciliVarlik)
            {
                // varlık tarihi üfe endeksi / dolar kuru
                var firstEndeks = _projeDbContext.tufeEndeks.OrderBy(e => e.Tarihi).FirstOrDefault(e => e.Tarihi == varlik.Tarihi);
                var firstKur = _projeDbContext.tufeEndeks.OrderBy(k => k.Tarihi).FirstOrDefault(k => k.Tarihi == varlik.Tarihi);

                // rapor tarihi üfe endeksi / dolar kuru
                var lastEndeks = _projeDbContext.tufeEndeks.OrderByDescending(e => e.Tarihi).FirstOrDefault();
                var lastKur = _projeDbContext.tufeEndeks.OrderByDescending(k => k.Tarihi).FirstOrDefault();

                // güncel varlık verisininin tarihini al
                var simdikiAy = varlik.Tarihi;
                // güncel varlık verisinden bir öncekini al
                var oncekiAy = simdikiAy.AddMonths(-1);

                // güncel varlık verisinin tarihine göre tüm verileri çek
                var mevcutAyVarlik = _projeDbContext.varlikBilgis.FirstOrDefault(v => secilivarliklar.Contains(v.Id) && v.Tarihi == simdikiAy);

                // güncel varlık verisinin tarihine göre bir önceki ayın tüm verilerini çek
                var oncekiAyVarlik = _projeDbContext.varlikBilgis.FirstOrDefault(v => secilivarliklar.Contains(v.Id) && v.Tarihi == oncekiAy);

                // kullanıcı tarafından gelen varlık verilerinin tarihine göre sıralama yapıyor ve en son tarihi alıyor
                var lastVarlikTutari = _projeDbContext.varlikBilgis.Where(v => secilivarliklar.Contains(v.Id)).OrderByDescending(v => v.Tarihi).FirstOrDefault();

                // önceki ayın dolarizasyon ve enflasyon tutarını hesaplamak için kur bilgilerini alıyoruz
                var varlikDolarizasyonKur = _projeDbContext.tufeEndeks.OrderBy(d => d.Tarihi).FirstOrDefault(d => d.Tarihi == oncekiAy);
                var varlikEnflasyonKur = _projeDbContext.tufeEndeks.OrderBy(d => d.Tarihi).FirstOrDefault(d => d.Tarihi == oncekiAy);

                foreach (var item in endeksBilgileri)
                {
                    // enflasyon varlık tutarı
                    var enflasyonVarlikTutari = (lastEndeks.endeksOrani * varlik.Tutari) / firstEndeks.endeksOrani;

                    // dolarizasyon varlık tutarı
                    var dolarizasyonVarlikTutari = (lastKur.dolarKuru * varlik.Tutari) / firstKur.dolarKuru;

                    // ÖNCEKİ AYA GÖRE VARLIK ARTIŞ
                    // double türünde 0 olarak atıyoruz ilk varlık verisi 0 olcak
                    double degisimOraniArtis = 0;

                    // oncekiAyVarlik verisini null gelebilir o yüzden sıfır olarak atadığımız degisimOraniArtis ilk ayın değisim yüzdesi olacak
                    // asıl hesaplama ise daha sonraki aydan başlayacaktır.
                    if (oncekiAyVarlik != null)
                    {
                        // mevcut varlık tutarından, önceki ayın varlık tutarını çıkarıp kalan sonucu onceki ay varlık tutarına bölüp sonucu 100 ile çarpıyoruz
                        degisimOraniArtis = ((mevcutAyVarlik.Tutari - oncekiAyVarlik.Tutari) / oncekiAyVarlik.Tutari) * 100;
                    }

                    // VARLIK DEĞİŞİM ORANI
                    double degisimOraniZam = (lastVarlikTutari.Tutari / mevcutAyVarlik.Tutari) * 100 - 100;

                    // DOLARİZASYON ÖNCEKİ AYA GÖRE VARLIK ARTIŞ
                    double dolarizasyonOncekiAyaGoreArtis = 0;

                    // oncekiAyVarlik verisini null gelebilir o yüzden sıfır olarak atadığımız dolarizasyonOncekiAyaGoreArtis ilk ayın değisim yüzdesi olacak
                    // asıl hesaplama ise daha sonraki aydan başlayacaktır.
                    if (oncekiAyVarlik != null)
                    {
                        var oncekiDolarizasyonTutari = (lastKur.dolarKuru * oncekiAyVarlik.Tutari) / varlikDolarizasyonKur.dolarKuru;
                        dolarizasyonOncekiAyaGoreArtis = (dolarizasyonVarlikTutari - oncekiDolarizasyonTutari) / oncekiDolarizasyonTutari * 100;
                    }

                    // ENFLASYON ÖNCEKİ AYA GÖRE VARLIK ARTIŞ
                    double enflasyonOncekiAyaGoreArtis = 0;

                    // oncekiAyVarlik verisini null gelebilir o yüzden sıfır olarak atadığımız enflasyonOncekiAyaGoreArtis ilk ayın değisim yüzdesi olacak
                    // asıl hesaplama ise daha sonraki aydan başlayacaktır.
                    if (oncekiAyVarlik != null)
                    {
                        var oncekiEnflasyonTutari = (lastEndeks.endeksOrani * oncekiAyVarlik.Tutari) / varlikEnflasyonKur.endeksOrani;
                        enflasyonOncekiAyaGoreArtis = (enflasyonVarlikTutari - oncekiEnflasyonTutari) / oncekiEnflasyonTutari * 100;
                    }

                    // DOLARİZASYON ETKİSİ YÜZDE
                    double dolarizasyonEtkisiYuzde =  (mevcutAyVarlik.Tutari / dolarizasyonVarlikTutari) * 100 - 100;

                    // ENFLASYON ETKİSİ YÜZDE
                    double enflasyonEtkisiYüzde = (mevcutAyVarlik.Tutari / enflasyonVarlikTutari) * 100 - 100;

                    hesaplanmisVarliklar.Add(new hesaplanmisOranlar
                    {
                        Tarihi = varlik.Tarihi,
                        varlikTutari = varlik.Tutari,
                        varlikTarihiDolarKuru = firstKur.dolarKuru,
                        endeksOran = firstKur.endeksOrani,
                        enflasyonVarlikTutari = enflasyonVarlikTutari,
                        dolarizasyonVarlikTutari = dolarizasyonVarlikTutari,
                        oncekiAyaGoreArtis = degisimOraniArtis,
                        degisimOrani = degisimOraniZam,
                        dolarizasyonOncekiAyaGoreArtis = dolarizasyonOncekiAyaGoreArtis,
                        enflasyonOncekiAyaGorevarlikArtis = enflasyonOncekiAyaGoreArtis,
                        dolarizasyonEtkisiYüzde = dolarizasyonEtkisiYuzde,
                        enflasyonEtkisiYüzde = enflasyonEtkisiYüzde
                    });
                    break;
                }
            }
            // hesaplama için en eski tarihli tutarları alıyoruz
            var enfVarlikTutari = hesaplanmisVarliklar.OrderByDescending(e => e.Tarihi).FirstOrDefault().enflasyonVarlikTutari;
            var dolVarlikTutari = hesaplanmisVarliklar.OrderByDescending(d => d.Tarihi).FirstOrDefault().dolarizasyonVarlikTutari;

            foreach (var varlik in hesaplanmisVarliklar)
            {
                double enflasyonDegisimOrani = 0;
                enflasyonDegisimOrani = (enfVarlikTutari / varlik.enflasyonVarlikTutari) * 100 - 100;
                varlik.enflasyonVarlikDegisimOrani = enflasyonDegisimOrani;

                double dolarizasyonDegisimOrani = 0;
                dolarizasyonDegisimOrani = (dolVarlikTutari / varlik.dolarizasyonVarlikTutari) * 100 - 100;
                varlik.dolarizasyonVarlikDegisimOrani = dolarizasyonDegisimOrani;
                
            }
            return View(hesaplanmisVarliklar);
        }
    }
}