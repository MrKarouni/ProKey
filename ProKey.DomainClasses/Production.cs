using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses
{
    public class Production
    {
        public int Id { get; set; }

        /// <summary>
        /// Producer id
        /// </summary>
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalCount { get; set; }

        public int RemainingCount { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
