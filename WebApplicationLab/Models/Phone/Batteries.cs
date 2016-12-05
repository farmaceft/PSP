using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationLab.Models
{
    public class Battery
    {
        [Key]
        public int Id { get; set; }

        public int Name { get; set; }
    }
}