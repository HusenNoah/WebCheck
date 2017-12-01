using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModels
{
    public class Customers
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAdres { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        public DateTime Date { get; set; }
    }
}