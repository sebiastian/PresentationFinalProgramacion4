using System.ComponentModel.DataAnnotations;


namespace Contract.Location.Request
{
    public class CreateLocationRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string SpaceType { get; set; } = null!;

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;
    }

}
