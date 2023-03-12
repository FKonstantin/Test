using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestFisenko.Models
{
    public class Goods
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string AdditFields { get; set; }
		public int ImageId { get; set; } 
		public string CategoryAdditFields { get; set; }
	}
}


