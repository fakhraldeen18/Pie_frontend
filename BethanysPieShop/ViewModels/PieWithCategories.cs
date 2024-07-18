using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
	public class PieWithCategories
	{

		public Pie pie { get; set; }
		public IEnumerable<Category> categories { get; set; }
	}
}
