using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.Countries;
using OnlineStore.Code;
using OnlineStore.Common.DTOs;
using OnlineStore.Entities.Entities;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class CountryController : BaseController
    {

        private readonly CountriesService CountriesService;
        public CountryController(ControllerDependencies dependencies, CountriesService countriesService) : base(dependencies)
        {
            CountriesService = countriesService;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            var countryList = CountriesService.GetAllCountries();
            var myJson = Json(countryList);

            return Json(countryList);
        }

    }
}
