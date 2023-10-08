using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using spotify_api.Entities;

namespace spotify_api.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public async Task<ActionResult> Index(string id = "2Hkut4rAAyrQxRdof7FVJq")
        {
            var artist = new Artist();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"https://localhost:7242/GetArtist/{id}"))
                {
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        artist = JsonConvert.DeserializeObject<Artist>(result);
                        if (artist.Name is null)

                            return RedirectToAction("NotFound");
                    }
                };


            }
            return View(artist);
            }

            public ActionResult NotFound() {

                return View();
            }
        }
}
