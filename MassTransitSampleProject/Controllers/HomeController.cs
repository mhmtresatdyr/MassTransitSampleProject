using MassTransitSampleProject.Models;
using MassTransitSampleProject.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MassTransitSampleProject.Controllers
{
    //MessageContract Referans olarak eklemeyi unutmayın
    //Nuget üzerinden MassTransit indiriniz
    //Nugetten NLog.Web.AspNetCore indiriniz
    //Loglama olarak Nlog kullandım ayarları appsettings.json okuması ayarlandı
    public class HomeController : Controller
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private PublisherStudent PublisherStudent;
        public HomeController(PublisherStudent PublisherStudent)
        {
            logger.Info("HomeController dependency injection");
            this.PublisherStudent = PublisherStudent;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SaveStudent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                logger.Info("SaveStudent Post Bilgileri{student}", student);
                var pub = PublisherStudent.Publish(student);
                if (pub == "Başarı ile iletildi")
                {
                    logger.Info("Bigiler rabbitmq ilgili kuyruğa eklendi");
                    ViewData["result"] = "Öğrenci Kayıt Edildi!";
                }
                else
                {
                    logger.Info("Bigiler rabbitmq iletilen bilgilerde problem oldu");
                    ViewData["result"] = "Öğrenci Kayıt Edilemedi!";
                }
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
