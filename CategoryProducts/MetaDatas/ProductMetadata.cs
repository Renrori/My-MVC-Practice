using CategoryProducts.LangResource;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CategoryProducts.Models
{
    internal class ProductMetadata
    {
        [Required(ErrorMessageResourceName = "ProductNameEmpty",
            ErrorMessageResourceType=typeof(WebResource))]
        [Display(Name = "ProductName",ResourceType =typeof(WebResource))]
        [StringLength(40)]
        public string ProductName { get; set; } = null!;

        [Display(Name ="每單位數量")]
        public string? QuantityPerUnit { get; set; }
        
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name = "單價")]
        public decimal? UnitPrice { get; set; }
        
        
        [Display(Name = "庫存")]
        public short? UnitsInStock { get; set; }


        [Display(Name ="訂購數量")]
        [Range(2,100,ErrorMessage ="{0}必須必須介於{1}至{2}之間！")]
        public short? UnitsOnOrder { get; set; }
    }
}