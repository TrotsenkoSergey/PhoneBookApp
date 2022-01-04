using System.ComponentModel.DataAnnotations;

namespace PhoneBookApp.WebSite.AuthModels
{

    public class UserLogin
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [MaxLength(20)]
        public string LoginProp { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Пароль должен быть не менее 4х символов")]
        public string Password { get; set; }

        [Display(Name = "Запомнить пароль?")]
        public bool IsRemembered { get; set; }
    }
}
