using System.ComponentModel.DataAnnotations;

namespace WebApplicationLab.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Логин")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Login { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        [MaxLength(128, ErrorMessage = "Превышена максимальная длина записи")]

        public string Password { get; set; }
        public string Salt { get; set; }
        [Required]
        [Display(Name = "Статус")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}