using System;
namespace _1C7BEC44.Models
{
	public class tbl_Admin_UserAuth
	{
		public int Id { get; set;} 
		public int TypeId { get; set;} 
		public int GroupTypeId { get; set;} 
		public string UserName { get; set;} 
		public string Email { get; set;} 
		public string PrimaryEmail { get; set;} 
		public string PhoneNumber { get; set;} 
		public string FirstName { get; set;} 
		public string LastName { get; set;} 
		public string DisplayName { get; set;} 
		public string Company { get; set;} 
		public DateTime BirthDate { get; set;} 
		public string BirthDateRaw { get; set;} 
		public string Address { get; set;} 
		public string Address2 { get; set;} 
		public string Avatar { get; set;} 
		public string MainJob { get; set;} 
		public string Describe { get; set;} 
		public int CityId { get; set;} 
		public string City { get; set;} 
		public int StateId { get; set;} 
		public string State { get; set;} 
		public string Country { get; set;} 
		public string Culture { get; set;} 
		public string FullName { get; set;} 
		public string Gender { get; set;} 
		public string Language { get; set;} 
		public string MailAddress { get; set;} 
		public string Nickname { get; set;} 
		public string PostalCode { get; set;} 
		public string TimeZone { get; set;} 
		public string PasswordHash { get; set;} 
		public string Roles { get; set;} 
		public string Permissions { get; set;} 
		public DateTime CreatedDate { get; set;} 
		public DateTime ModifiedDate { get; set;} 
		public int InvalidLoginAttempts { get; set;} 
		public DateTime LastLoginAttempt { get; set;} 
		public DateTime LockedDate { get; set;} 
		public int Status { get; set;} 
	}
}
