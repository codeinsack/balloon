using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Balloon.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите электронный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вы не ввели пароль!")]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}