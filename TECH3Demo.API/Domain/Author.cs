using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TECH3Demo.API.Domain
{
    public class Author : BaseModel
    {
        public Author()
        {
            Books = new List<Book>();
        }
 
        [Required]
        [StringLength(32, ErrorMessage = "Length of First Name can't be longer than 32 chars!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Length of Last Name can't be longer than 32 chars!")]
        public string LastName { get; set; }

        public List<Book> Books { get; set; }

    }
}
