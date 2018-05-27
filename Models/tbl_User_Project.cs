using System;
namespace _1C7BEC44.Models
{
	public class tbl_User_Project
	{
		public int Id { get; set;} 
		public int UserId { get; set;} 
		public int JobId { get; set;} 
		public string Name { get; set;} 
		public string Position { get; set;} 
		public string Partner { get; set;} 
		public DateTime FromDate { get; set;} 
		public DateTime ToDate { get; set;}
        public DateTime ProjectFromDate { get; set; }
        public DateTime ProjectToDate { get; set; }
		public string ProjectPosition { get; set;} 
        public string Note { get; set; }
        public string Image { get; set;} 
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
		public DateTime UpdateDate { get; set;} 
	}
    public class tbl_User_Project_View
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Partner { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime ProjectFromDate { get; set; }
        public DateTime ProjectToDate { get; set; }
        public string ProjectPosition { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
        public int KieuInt1 { get; set; }
        public int KieuInt2 { get; set; }
        public string KieuString1 { get; set; }
        public string KieuString2 { get; set; }
        public float KieuFloat1 { get; set; }
        public float KieuFloat2 { get; set; }
        public DateTime KieuNgay1 { get; set; }
        public DateTime KieuNgay2 { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string JobIdName { get; set; }
    }
}
