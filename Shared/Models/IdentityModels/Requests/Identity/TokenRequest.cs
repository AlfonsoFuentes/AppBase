﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Models.IdentityModels.Requests.Identity
{
    public class TokenRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}