using AutoMapper;
using BethanysPieShop.Dtos;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IMapper _mapper;

        public HomeController(IPieRepository pieRepository , IMapper mapper)
        {
            _pieRepository = pieRepository;
            _mapper = mapper;
        }

        public async Task<ViewResult> Index()
        {
			IEnumerable<PieReadDTO> result = null;
			HttpClient _client = new HttpClient();
			HttpResponseMessage response = await _client.GetAsync("https://localhost:7109/api/PieOfTheWeek");
			if (response.IsSuccessStatusCode)
			{
				// Process the successful API response
				var data = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<IEnumerable<PieReadDTO>>(data);
				//return view(result);
			}
			var readPie = _mapper.Map<IEnumerable<Pie>>(result);

			var piesOfTheWeek = _pieRepository.PiesOfTheWeek;

            var homeViewModel = new HomeViewModel(readPie);

            return View(homeViewModel);
        }
    }
}
