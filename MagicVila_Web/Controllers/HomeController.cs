using AutoMapper;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVila_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController( IVillaService villaService, IMapper mapper)
        {
            _villaService= villaService;
            _mapper= mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = null;
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

       
    }
}