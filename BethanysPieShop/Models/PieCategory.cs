using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShop.Models
{
	public class PieCategory
	{
		public int PieId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? ShortDescription { get; set; }
		public string? LongDescription { get; set; }
		public string? AllergyInformation { get; set; }
		public decimal Price { get; set; }
		public string? ImageUrl { get; set; }
		public string? ImageThumbnailUrl { get; set; }
		public bool IsPieOfTheWeek { get; set; }
		public bool InStock { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; } = default!;
		public List<SelectListItem> Categories { get; set; }
	}
}
