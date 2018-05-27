﻿using System;
namespace _1C7BEC44.Models
{
	public class tbl_Admin_Group_Permission
	{
		public int Id { get; set;} 
		public int GroupId { get; set;} 
		public int PermissionId { get; set;} 
		public int Type { get; set;} 
		public string Code { get; set;} 
		public string Name { get; set;} 
		public string Note { get; set;} 
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
    public class tbl_Admin_Group_Permission_View00
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PermissionId { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
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
        public string PermissionIdCode { get; set; }
        public string PermissionIdName { get; set; }
    }
}
