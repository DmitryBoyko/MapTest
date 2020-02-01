using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MapTest.Models;
using MapTest.BizLogic;
using MapTest.Helpers;

namespace MapTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStatsRepository _statsRepository;
       
        public HomeController(ILogger<HomeController> logger, IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;         
            _logger = logger;
        }
              
        public IActionResult Index()
        {
            var model = new MainPageModel();
            model.MainFilter.Add(new KeyValuePair<int, string>((int)CommonFilters.ActiveFederalProjects, 
                CommonFilters.ActiveFederalProjects.DescriptionAttr()));
            model.MainFilter.Add(new KeyValuePair<int, string>((int)CommonFilters.SelectedFederalProjects,
                CommonFilters.SelectedFederalProjects.DescriptionAttr()));
            model.MainFilter.Add(new KeyValuePair<int, string>((int)CommonFilters.CompletedFederalProjects,
                CommonFilters.CompletedFederalProjects.DescriptionAttr()));

            for (int i = DateTime.Now.AddYears(-10).Year; i < DateTime.Now.AddYears(1).Year; i++) // 10 можем получать из конфигурации приложения (БД)
            {
                model.Years.Add(i);
            }           

            return View(model);
        }

        // Action Method Injection - Пример
        // public IActionResult GetTablePartialView([FromServices] IStatsRepository statsRepository) 
        public IActionResult GetTablePartialView()
        {
            var model = new TablePageModel()
            {             
                FederalDistricts = _statsRepository.FederalDistricts().ToList(),
            };

            // Get Service Instance Manually - еще пример
            // var services = this.HttpContext.RequestServices;
            // var statsRepository = (IStatsRepository)services.GetService(typeof(IStatsRepository));

            return PartialView("_TablePartialView", model);
        }

        public IActionResult GetMapRegionPartialView(string federalSubjectID)
        {
            return PartialView("_MapRegionStatPartialView", _statsRepository.GetFederalSubjectMainStats(federalSubjectID));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
