using System;
namespace _1C7BEC44.Models
{
	public class tbl_Provinces_Wards
	{
		public int Id { get; set;} 
		public int ProvinceId { get; set;} 
		public string Name { get; set;} 
		public string Code { get; set;} 
		public int InUse { get; set;} 
		public int InOrder { get; set;} 
	}
}
