using System.ComponentModel.DataAnnotations;

namespace Balloon.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите электронный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введи свое прозвище")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Вы не ввели пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}