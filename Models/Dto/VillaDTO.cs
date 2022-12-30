using System;
using System.ComponentModel.DataAnnotations;

namespace MagicVila_VilaAPI.Models.Dto
{
	public class VillaDTO
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(40)]
		public string Name { get; set; }
		public int Occupancy { get; set; }
		public int Sqft { get; set; }
	}

}

