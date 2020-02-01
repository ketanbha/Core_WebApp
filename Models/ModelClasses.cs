using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Core_WebApp.Models
{
    public class Category
    {
        [Key]   // primary identity key
        public int CategoryRowId { get; set; }
        [Required(ErrorMessage = "CategoryId is must")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "CategoryName is must")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "BasePrice is must")]
        public int BasePrice { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        [Key]   // primary identity key
        public int ProductRowId { get; set; }

        [Required(ErrorMessage = "ProductId is must")]
        public string ProductId { get; set; }
        
        [Required(ErrorMessage = "ProductName is must")]
        public string ProductName { get; set; }
        
        [Required(ErrorMessage = "Manufacturer is must")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Description is must")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is must")]
        public int Price { get; set; }

        public Category Category { get; set; }

        public int CategoryRowId { get; set; }
    }

    public class NonNumericCheck : ValidationAttribute
    {
    }
}
