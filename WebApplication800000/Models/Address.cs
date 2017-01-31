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
        public String postal_code { get; set; }

        public String country { get; set; }

        public int house_number { get; set; }

        [Key]
        public int id { get; set; }

        public String city { get; set; }

        public String province { get; set; }

        public String street { get; set; }
    }
}