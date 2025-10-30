using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsWebApi.Core.DTO
{
    public class DogAddRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name length must be between 1 and 50 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(50, ErrorMessage = "Color length must be at most 50 characters.")]
        public string Color { get; set; } = null!;

        [Required(ErrorMessage = "Tail length is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Tail length must be a non-negative number.")]
        public int TailLength { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Weight must be a non-negative number.")]
        public int Weight { get; set; }
    }
}
