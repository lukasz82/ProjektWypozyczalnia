using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        [NotMapped]
        public string FullName => Name + " " + Surname;
    }
}
