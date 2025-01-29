using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Kunde")]
public class Kunde {
    [Key]
    public int KundeId { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Email { get; set; }
    
    [Column(TypeName = "nchar(12)")]
    public string Telefon { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Adresse { get; set; }
    
    // Navigation property
    public virtual ICollection<Reservation> Reservationer { get; set; }
} 