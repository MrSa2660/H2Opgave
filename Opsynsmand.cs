using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Opsynsmand")]
public class Opsynsmand {
    [Key]
    public int OpsynsmandId { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Navn { get; set; }
    
    [Column(TypeName = "nchar(12)")]
    public string Telefon { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Email { get; set; }
    
    // Navigation property
    public virtual ICollection<Sommerhus> Sommerhuse { get; set; }
} 