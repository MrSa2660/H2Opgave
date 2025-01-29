using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Konsulent")]
public class Konsulent {
    [Key]
    public int KonsulentId { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Navn { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Email { get; set; }
    
    [Column(TypeName = "nchar(12)")]
    public string Telefon { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Adresse { get; set; }
    
    // Navigation property
    public virtual ICollection<Område> AnsvarligForOmråder { get; set; }
} 