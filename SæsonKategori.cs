using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum SæsonType
{
    Super,
    Høj,
    Mellem,
    Lav
}

[Table("SæsonKategori")]
public class SæsonKategori {
    [Key]
    public int KategoriId { get; set; }
    
    [Column(TypeName = "nchar")]
    public SæsonType Type { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Uger { get; set; }
    
    public int Pris { get; set; }
    
    // Navigation property
    public virtual ICollection<Sommerhus> Sommerhuse { get; set; }

    public SæsonKategori()
    {
        Sommerhuse = new List<Sommerhus>();
    }

    // Business logic methods
    public List<int> HentUgeNumre()
    {
        // Konverterer string af uger til en liste af integers
        return Uger.Split(',')
                  .Select(u => int.Parse(u.Trim()))
                  .ToList();
    }

    public bool IndeholderUge(int ugeNummer)
    {
        return HentUgeNumre().Contains(ugeNummer);
    }

    public decimal BeregnPrisForUge(decimal basisPris)
    {
        // Beregn pris baseret på sæsonkategori
        decimal prisFaktor = Type switch
        {
            SæsonType.Super => 2.0m,
            SæsonType.Høj => 1.5m,
            SæsonType.Mellem => 1.0m,
            SæsonType.Lav => 0.7m,
            _ => 1.0m
        };

        return basisPris * prisFaktor;
    }

    public string HentSæsonBeskrivelse()
    {
        return $"Sæson: {Type}\n" +
               $"Uger: {Uger}\n" +
               $"Prisfaktor: {BeregnPrisForUge(1000m) / 1000m:F1}x basis pris";
    }

    public override string ToString()
    {
        return HentSæsonBeskrivelse();
    }

    // Helper method til at få standard uger for en sæsontype
    public static string HentStandardUger(SæsonType type)
    {
        return type switch
        {
            SæsonType.Super => "28,29,30,52",  // Supersæson: Højsommer og jul
            SæsonType.Høj => "26,27,31,32,42", // Højsæson: Start/slut sommer og efterårsferie
            SæsonType.Mellem => "13,14,15,33,34,35,51,53", // Mellemsæson: Påske, sensommer, nytår
            SæsonType.Lav => string.Join(",", Enumerable.Range(1, 53)
                                   .Where(uge => !("28,29,30,52,26,27,31,32,42,13,14,15,33,34,35,51,53"
                                   .Split(',').Select(int.Parse).Contains(uge)))), // Alle andre uger
            _ => ""
        };
    }
}