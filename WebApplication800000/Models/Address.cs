using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication800000.Models
{
    // Model Class For Address
    public class Address
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "id")]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int address_id { get; set; }

        [Display(Name = "postal_code")]
        public String postal_code { get; set; }

        [Display(Name = "country")]
        public String country { get; set; }

        [Display(Name = "house_number")]
        public int house_number { get; set; }

        [Display(Name = "city")]
        public String city { get; set; }

        [Display(Name = "province")]
        public String province { get; set; }

        [Display(Name = "street")]
        public String street { get; set; }
    }
}