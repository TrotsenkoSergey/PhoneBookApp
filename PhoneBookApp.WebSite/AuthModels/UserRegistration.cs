using System.ComponentModel.DataAnnotations;

namespace PhoneBookApp.WebSite.AuthModels
{
    public class UserRegistration
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходимо заполнить это поле"), MaxLength(20)]
        public string LoginProp { get; set; }

        [Display(Name = "Электронный адрес")]
        [Required(ErrorMessage = "Электронный адрес необходимо ввести.")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Неверный формат адреса электронной почты.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Подтверждение электронного адреса")]
        [Required(ErrorMessage = "Электронный адрес необходимо ввести.")]
        [Compare(nameof(EmailAddress), ErrorMessage = "Электронный адрес должен совпадать.")]
        public string ConfirmEmailAddress { get; set; }

        //[UIHint("password")]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль необходимо ввести.")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Пароль должен быть не менее 4х символов")]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)] 
        [Compare(nameof(Password), ErrorMessage = "Пароль должен совпадать")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Запомнить пароль?")]
        public bool IsRemembered { get; set; }
    }
}
