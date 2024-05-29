using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs.Account
{
    public class LoginDTO
    {
        [Required]
        public string? EmailOrUsername { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
