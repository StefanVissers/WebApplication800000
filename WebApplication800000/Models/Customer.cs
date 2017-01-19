using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication800000.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long.")]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Please enter your surname.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Must be at least 1 character long.")]
        [Display(Name = "Surname")]
        public String Surname { get; set; }

        [Required(ErrorMessage = "Please enter your e-mail address")]
        [Display(Name = "Email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password should be at least 6 characters long.")]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [Display(Name = "date_of_birth")]
        public DateTime date_of_birth { get; set; }

        [Display(Name = "phone_number")]
        public String phone_number { get; set; }

    }
}