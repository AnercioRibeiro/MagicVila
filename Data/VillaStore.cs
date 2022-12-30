using System;
using MagicVila_VilaAPI.Models.Dto;

namespace MagicVila_VilaAPI.Data
{
	public class VillaStore
	{
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
               new VillaDTO {Id=1, Name="Pool View", Sqft=100, Occupancy=4},
               new VillaDTO {Id=2, Name="Beach View", Sqft=300, Occupancy=3},
               new VillaDTO {Id=3, Name="Miami Lagoon", Sqft=400, Occupancy=4},
               new VillaDTO {Id=4, Name="Mussulo Resort", Sqft=400, Occupancy=5},

        };
     }
}

