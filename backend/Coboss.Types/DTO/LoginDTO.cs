﻿using System.ComponentModel.DataAnnotations;

namespace Coboss.Types.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Login { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
