using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public bool IsReserved { get; set; }
        public int ReservedDays { get; set; }
        [DataType(DataType.Date)] // oznacza że ta właściwość potrzebuje tylko date, bez godizny min itp. użytkownik nie musi wpisaywać
        public DateTime ReservedDate { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
