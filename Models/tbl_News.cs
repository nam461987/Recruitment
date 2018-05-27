using System;
namespace _1C7BEC44.Models
{
	public class tbl_News
	{
		public int Id { get; set;} 
		public int BranchId { get; set;} 
		public int PageId { get; set;} 
		public int NewsTypeId { get; set;} 
		public int NewsGroupTypeId { get; set;} 
		public string StyleLayout { get; set;} 
		public string comment { get; set;} 
		public string CSS { get; set;} 
		public string URLmain { get; set;} 
		public int IsShowContentParentOrChild { get; set;} 
		public string hinhanhImageSample { get; set;} 
		public int IsBuildContent { get; set;} 
		public int ShowGobalPoup { get; set;} 
		public int NewsOrProducct { get; set;} 
		public string StyeImageLayout { get; set;} 
		public string Name { get; set;} 
		public string ShortContentNews { get; set;} 
		public string ContentNews { get; set;} 
		public string TagName { get; set;} 
		public DateTime NgayDeliver { get; set;} 
		public DateTime NgayExpire { get; set;} 
		public int EmployeeId { get; set;} 
		public string DataIsFromWhre { get; set;} 
		public string FullContent { get; set;} 
		public string UrlName { get; set;} 
		public bool ShowPublic { get; set;} 
		public int isPrgOrdered { get; set;} 
		public int CreateStaffId { get; set;} 
		public int UpdateStaffId { get; set;} 
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
}
