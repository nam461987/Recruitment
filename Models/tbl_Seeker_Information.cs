using System;
namespace _1C7BEC44.Models
{
	public class tbl_Seeker_Information
	{
		public int Id { get; set;} 
		public int UserId { get; set;} 
		public string Title { get; set;} 
		public int PositionNowId { get; set;} 
		public int PositionFutureId { get; set;} 
		public string Job { get; set;} 
		public string JobName { get; set;} 
		public string Location { get; set;} 
		public string LocationName { get; set;} 
		public int DiplomaId { get; set;} 
		public int ExperienceId { get; set;} 
		public int WorkTypeId { get; set;} 
		public float Salary { get; set;} 
		public int WageId { get; set;} 
		public string Target { get; set;} 
		public string CertificateFile { get; set;} 
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
		public int StaffIdAccept { get; set;} 
		public int StaffIdUpdate { get; set;} 
		public DateTime UpdateDate { get; set;} 
	}
}
