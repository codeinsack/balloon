using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Balloon.Models
{
    public class UserViewModel
    {
        [Display(Name = "Идентификатор пользователя")]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Электронный ящик")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {1} characters long", MinimumLength = 2)]
        public string NickName { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Роль пользователя")]
        public IdentityRole Role { get; set; }
    }
}