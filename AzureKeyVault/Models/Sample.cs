using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureKeyVault.Models
{
    [Table("Samples")]
    [Index(nameof(Name), Name = "IX_Sample", IsUnique = true)]
    public class Sample
    {
        [Key]
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public Sample()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Description: {Description}";
        }
    }
}
