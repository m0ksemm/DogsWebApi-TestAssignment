using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsWebApi.Core.Domain.Entities
{
    public class Dog
    {
        [Key]
        public Guid DogID { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
