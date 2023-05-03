//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebSite.ViewModels
{
    public class ContactViewModel : IValidatableObject
    {
        [Required(ErrorMessage ="帳號欄位不可為空!")]
        [StringLength(maximumLength:8 , MinimumLength = 3, ErrorMessage ="帳號輸入限制為3~8字元")]
        [Display(Name="帳號")]
        
        public string Name { get; set; }


        //[Required(ErrorMessage = "郵件欄位不可為空!")]
        [Display(Name = "郵件")]
        [EmailAddress(ErrorMessage ="郵件格式錯誤!")]
        
        public string? Email { get; set; }

        [Display(Name = "手機號碼")]
        //[Phone(ErrorMessage = "手機號碼格式錯誤!")]
        
        public string? Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if( string .IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            {
                yield return new ValidationResult(errorMessage: "二者擇一!", new string[] { "Email", "Phone" });
            }
        }
    }
}