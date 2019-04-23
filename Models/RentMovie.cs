using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class RentMovie
    {
        public int RentMovieId { get; set; }
        public bool IsRent { get; set; }
        [DataType(DataType.Date)] // oznacza że ta właściwość potrzebuje tylko date, bez godizny min itp. użytkownik nie musi wpisaywać
        public DateTime RentDate { get; set; }
        public int RentDays { get; set; }
        public float DayRentPrice { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
