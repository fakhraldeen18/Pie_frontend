using AutoMapper;
using BethanysPieShop.Dtos;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository,IMapper mapper)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ViewResult> List(string category)
        {
            IEnumerable<Pie> pies;
            IEnumerable<PieReadDTO> result=null;
			string ? currentCategory;

            HttpClient _client=new HttpClient();
			HttpResponseMessage response = await _client.GetAsync($"https://localhost:7109/api/PieWithCategory?category={category}");
			if (response.IsSuccessStatusCode)
			{
				// Process the successful API response
				var data = await response.Content.ReadAsStringAsync();
				 result = JsonConvert.DeserializeObject<IEnumerable<PieReadDTO>>(data);
				//return view(result);
			}

			if (string.IsNullOrEmpty(category))
            {
                currentCategory = "All pies";
            }
            else
            {
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            var mapPie = _mapper.Map<IEnumerable<Pie>>(result);

            return View(new PieListViewModel(mapPie, currentCategory));
        }

        public async Task<IActionResult> Details(int id)
        {
			PieReadDTO result = null;
			HttpClient _client = new HttpClient();
			HttpResponseMessage response = await _client.GetAsync($"https://localhost:7109/api/Pie/{id}");
			if (response.IsSuccessStatusCode)
			{
				// Process the successful API response
				var data = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<PieReadDTO>(data);
				//return view(result);
			}
            var readPie = _mapper.Map<Pie>(result);

			//var pie = _pieRepository.GetPieById(id);
            if (readPie == null)
                return NotFound();

            return View(readPie);
        }
    }
}
