using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_31_Github.Models
{
	public class Repository
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Full_Name { get; set; }
		public string Html_Url { get; set; }
	}
}
