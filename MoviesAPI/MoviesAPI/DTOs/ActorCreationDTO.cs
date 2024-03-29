﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class ActorCreationDTO
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BioGraphy { get; set; }
        public IFormFile Picture { get; set; }
    }
}
