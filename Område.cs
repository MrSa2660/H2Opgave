using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Område")]
public class Område {
    [Key]
    public int OmrådeId { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Navn { get; set; }
    
    public int KonsulentId { get; set; }
    
    [ForeignKey("KonsulentId")]
    public virtual Konsulent Konsulent { get; set; }
    
    // Navigation property
    public virtual ICollection<Sommerhus> Sommerhuse { get; set; }

    public Område()
    {
        Sommerhuse = new List<Sommerhus>();
    }

    // Business logic methods
    public List<Sommerhus> HentLedigeSommerhuse(DateTime fraDato, DateTime tilDato)
    {
        return Sommerhuse
            .Where(s => !s.Reservationer.Any(r => 
                !(r.SlutDato < fraDato || r.StartDato > tilDato)))
            .ToList();
    }

    public List<Sommerhus> HentSommerhuseEfterStandard(string standard)
    {
        return Sommerhuse
            .Where(s => s.Fstandard.ToLower().Trim() == standard.ToLower().Trim())
            .ToList();
    }

    public Dictionary<string, int> HentStatistik()
    {
        return new Dictionary<string, int>
        {
            {"Antal sommerhuse", Sommerhuse.Count},
            {"Antal luksus", Sommerhuse.Count(s => s.Fstandard.ToLower().Trim() == "luksus")},
            {"Antal normal", Sommerhuse.Count(s => s.Fstandard.ToLower().Trim() == "normal")},
            {"Antal basic", Sommerhuse.Count(s => s.Fstandard.ToLower().Trim() == "basic")},
            {"Antal aktive reservationer", Sommerhuse.Sum(s => s.HentAktiveReservationer().Count)},
            {"Antal kommende reservationer", Sommerhuse.Sum(s => s.HentKommendeReservationer().Count)}
        };
    }

    public decimal GennemsnitligUgePris()
    {
        if (!Sommerhuse.Any())
            return 0;

        return Sommerhuse.Average(s => s.BasePris);
    }

    public override string ToString()
    {
        var stats = HentStatistik();
        return $"Område: {Navn}\n" +
               $"Konsulent: {Konsulent?.Navn ?? "Ingen"}\n" +
               $"Antal sommerhuse: {stats["Antal sommerhuse"]}\n" +
               $"- Luksus: {stats["Antal luksus"]}\n" +
               $"- Normal: {stats["Antal normal"]}\n" +
               $"- Basic: {stats["Antal basic"]}\n" +
               $"Aktive reservationer: {stats["Antal aktive reservationer"]}\n" +
               $"Kommende reservationer: {stats["Antal kommende reservationer"]}\n" +
               $"Gennemsnitlig ugepris: {GennemsnitligUgePris():C}";
    }
}