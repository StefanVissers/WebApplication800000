using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication800000.Models
{

    // Models For Everything Order Related
    // Use With EntityFramework
    // Model For Orders
    public class Order
    {
        [Column(Order = 0),Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int order_id { get; set; }

        [Column(Order = 1), Key]
        public int customer_id { get; set; }

        public String current_status { get; set; }
    }

    // Model For Orderedproducts
    public class Orderedproduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int product_id { get; set; }

        public float price_on_purchase { get; set; }

        public String catagory { get; set; }

        public String manufactorer { get; set; }

        public String name { get; set; }
    }

    // Model For Orderproducts
    public class Orderproduct
    {
        [Column(Order = 0), Key]
        public int order_id { get; set; }

        [Column(Order = 1), Key]
        public int product_id { get; set; }

        [Column(Order = 2), Key]
        public int customer_id { get; set; }
    }

    public class OrderModelView
    {
        public List<Order> orders { get; set; }
        public List<Orderedproduct> ordered_products { get; set; }
        public List<Orderproduct> order_products { get; set; }
    }
    public class OrderHistoryModelView
    {
        [Column(Order = 1), Key]
        [Display(Name = "customer_id")]
        public int customer_id { get; set; }

        [Column(Order = 0), Key]
        [Display(Name = "product_id")]
        public int product_id { get; set; }

        [Display(Name = "catagory")]
        public String catagory { get; set; }

        [Display(Name = "manufactorer")]
        public String manufactorer { get; set; }

        [Display(Name = "name")]
        public String name { get; set; }

        [Display(Name = "price_on_purchase")]
        public float price_on_purchase { get; set; }
    }
}