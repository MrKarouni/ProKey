using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string SessionKey { get; set; }

        public string RndD { get; set; }

        public string RndS { get; set; }

        public decimal FramewareVersion { get; set; }

        public int ProductionId { get; set; }

        [ForeignKey("ProductionId")]
        public virtual Production Production { get; set; }

        public int? OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User User { get; set; }

        public DateTime? FirstUseDate { get; set; }

        public bool IsActive { get; set; }
    }
}
