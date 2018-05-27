using System;
namespace _1C7BEC44.Models
{
	public class tbl_Recruitment_Post
	{
		public int Id { get; set;} 
		public int UserId { get; set;} 
		public int TypeId { get; set;} 
		public string TypeIdName { get; set; }
        public string Title { get; set;} 
		public int PositionId { get; set;} 
		public string Job { get; set;} 
		public string JobName { get; set; }
        public string Location { get; set;} 
        public string LocationName { get; set; }
        public int Wage { get; set;} 
		public string TrialTime { get; set;} 
		public int Num { get; set;} 
		public int Experience { get; set;} 
		public int Diploma { get; set;} 
		public string Gender { get; set;} 
		public int Age { get; set;} 
		public string Describe { get; set;} 
		public string Interest { get; set;} 
		public string Other { get; set;} 
		public string Folder { get; set;} 
		public DateTime EndDate { get; set;} 
		public int StaffId { get; set;} 
		public int KieuInt1 { get; set;} 
		public int KieuInt2 { get; set;} 
		public string KieuString1 { get; set;} 
		public string KieuString2 { get; set;} 
		public float KieuFloat1 { get; set;} 
		public float KieuFloat2 { get; set;} 
		public DateTime KieuNgay1 { get; set;} 
		public DateTime KieuNgay2 { get; set;} 
		public int Status { get; set;} 
		public DateTime CreateDate { get; set;}
        public int StaffIdUpdate { get; set; }

        public DateTime UpdateDate { get; set;} 
	}
}
