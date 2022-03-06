using HomePhysio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel
{
    public class RegisterPatientViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [Range(0,100)]
        [Required]
        public int Age { get; set; }


        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }

       [Required(ErrorMessage= "Place the marker in your location!")]
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string True { get; set; }

        [Required]
        [Compare("True", ErrorMessage = "You must agree to terms and conditions")]
        public string terms { get; set; }

    }
    public class RegisterPhysioViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Name { get; set; }


        [Range(18, 100)]
        [Required]
        public int age { get; set; }

        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        public int CategoryId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string ContactNo { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string LicenseNo { get; set; }
        [Required]
        public string CitizenshipNumber { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required(ErrorMessage = "Place the marker in your location!")]

        public string Longitude { get; set; }

        public string Latitude { get; set; }
        [Required]
        public string Address { get; set; }

        public decimal ConsultationCharge { get; set; }
        [Required]
        [Compare("True", ErrorMessage = "You must agree to terms and conditions")]
        public string terms { get; set; }
        public string True { get; set; }


    }
}
