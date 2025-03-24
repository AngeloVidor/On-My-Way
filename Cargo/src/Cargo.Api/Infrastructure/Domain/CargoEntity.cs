using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo.Api.Infrastructure.Domain
{
    public class CargoEntity
    {
        [Key]
        public long Cargo_ID { get; set; }
        public long Transporter_ID { get; set; }
        public long Vehicle_ID { get; set; }
    }
}