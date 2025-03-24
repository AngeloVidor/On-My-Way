using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Api.Controllers.DTOs
{
    public class CargoDto
    {
        [Key]
        public long Cargo_ID { get; set; }
        public long Transporter_ID { get; set; }
        public long Vehicle_ID { get; set; }
    }
}