using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TecH3Demo.API.Domain
{
    public class Book : BaseModel
    {

        [Required]
        [StringLength(64, ErrorMessage = "Title can't be longer than 64 chars!")]
        public string Title { get; set; }
        [Required]
        public DateTime Published { get; set; }

        [ForeignKey("Author.Id")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

    }
}
