using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Ejer")]
public class Ejer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EjerId { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Navn { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Email { get; set; }
    
    [Column(TypeName = "nchar(12)")]
    public string Telefon { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Adresse { get; set; }
    
    // Navigation property
    public virtual ICollection<Sommerhus> Sommerhuse { get; set; }
}