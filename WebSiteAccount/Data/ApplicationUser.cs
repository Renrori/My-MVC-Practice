using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebSiteAccount.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "國別碼不可為空")]
        [StringLength (3,ErrorMessage ="國別碼為3個字元")]
        [Display(Name = "國別碼")]
        public string CountryCode { get; set; }
    }
}
