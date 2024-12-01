//using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using Newtonsoft.Json;

namespace LAB1.Models
{
    public class Car
    {
        [Key]
        [RegularExpression("\\d{6,9}",ErrorMessage ="Serial number must be from 6 to 9 digits, please retry")]
        [DisplayName("Serial Number")]
        public int SerialNumber { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }

        [Required]
        [NoFutureDateTime]
        [DisplayName("Registration date")]
        public DateTime RegistrationDate { get; set; }


        public string Motor { get; set; } = string.Empty;
		[DisplayName("Number of seats")]
		[Range(2, 8, ErrorMessage = "The number of seats must be from 2 to 8, please retry")]
		public int Seats { get; set; }
        
        [Required] 
		[RegularExpression("\\+370\\s8\\d{8}", ErrorMessage = "Insert a valid lithuanian phone number, please retry")]
        [DisplayName("Phone number of the owner")]
        public string PhoneNumberOwner { get; set; }
        
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage = "Email address not valid, please retry")]
        [DisplayName("Email address of the owner")]
        public string EmailAddressOwner { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Price { get; set; }
    }
}
public class NoFutureDateTimeAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dt && (DateTime.Compare(dt.Date, DateTime.Now.Date) > 0))
            return false;

        return true;
    }
    public override string FormatErrorMessage(string name)
    {
        return $"Registration date can't be later than today";
    }
}

