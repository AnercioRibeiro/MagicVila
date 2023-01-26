using AutoMapper;
using MagicVila_VilaAPI.Repository.IRepository;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Models.ViewModels;
using MagicVila_Web.Services;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace MagicVila_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;
        public readonly IVillaService _villaService;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }
        public async Task<IActionResult> Index()
        {
            List<VillaNumberDTO> list = new();
            var response = await _villaNumberService.GetAllAsync<APIResponse>();
            if (response !=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        
        public async Task<IActionResult> Create()
        {
            VillaNumberCreateVM villaNumber = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumber.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(i=> new SelectListItem
                {
                    Text= i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(villaNumber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
     
        }

        [HttpGet]
        public async Task<IActionResult> Update(int villaId)
        {
            var response = await _villaNumberService.GetAsync<APIResponse>(villaId);
            //var response = await _unitOfWork.VillaRepository.GetAsync(u => u.Id == villaId);
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaNumberUpdateDTO>(model));
            }
            return NotFound();
        }


    }
}
