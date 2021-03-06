using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
// using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TecH3Demo.API.Domain
{
    public class BaseModel
    {   [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
    }
}
