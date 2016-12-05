using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLab.Models
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Логин")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int SystemOperationId { get; set; }
        public SystemOperation SystemOperation { get; set; }

        public int ScreenSizeId { get; set; }
        public ScreenSize ScreenSize { get; set; }

        public DateTime Date { get; set; }

        public int OZUId { get; set; }
        public OZU OZU { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int CameraId { get; set; }
        public Camera Camera { get; set; }

        public int BatteryId { get; set; }
        public Battery Battery { get; set; }

        public int Memory { get; set; }
    }
}