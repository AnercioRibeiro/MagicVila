using System;
using MagicVila_VilaAPI.Models.Dto;

namespace MagicVila_VilaAPI.Data
{
	public class VillaStore
	{
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
               new VillaDTO {Id=1, Name="Pool View"},
               new VillaDTO {Id=2, Name="Beach View"},
            
        };
     }
}

