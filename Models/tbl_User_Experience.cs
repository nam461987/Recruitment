using System;
namespace _1C7BEC44.Models
{
	public class tbl_User_Experience
	{
		public int Id { get; set;} 
		public int UserId { get; set;} 
		public int NganhId { get; set;} 
		public int LoaiViecId { get; set;} 
		public int NumOfYear { get; set;} 
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
    public class tbl_User_Experience_View
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NganhId { get; set; }
        public int LoaiViecId { get; set; }
        public int NumOfYear { get; set; }
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
        public string NganhIdName { get; set; }
        public string LoaiViecIdName { get; set; }
    }
}
