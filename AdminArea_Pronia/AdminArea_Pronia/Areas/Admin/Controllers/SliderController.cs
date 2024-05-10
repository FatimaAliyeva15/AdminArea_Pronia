using AdminArea_ProniaBusiness.Services.Abstracts;
using AdminArea_ProniaCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminArea_Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        ISLiderService _sliderService;


        public SliderController(ISLiderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var sliders = _sliderService.GetAllSliders();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }


            string filename = slider.ImgFile.FileName;
            string path = @"C:\\Users\\Asus\\source\\repos\\AdminArea_Pronia\\AdminArea_Pronia\\wwwroot\\Upload\\" + filename;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                slider.ImgFile.CopyTo(stream);

            }

            slider.ImgUrl = filename;

            if (!ModelState.IsValid)
            {
                return View();
            }
            _sliderService.AddSlider(slider);
            return RedirectToAction(nameof(Index));
        }
    }
}
