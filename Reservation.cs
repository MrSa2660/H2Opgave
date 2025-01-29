using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISOWeek;

[Table("Reservation")]
public class Reservation {
    [Key]
    public int ReservationId { get; set; }
    
    public int SommerhusId { get; set; }
    
    public int KundeId { get; set; }
    
    public DateTime StartDato { get; set; }
    
    public DateTime SlutDato { get; set; }
    
    // Navigation properties
    [ForeignKey("SommerhusId")]
    public virtual Sommerhus Sommerhus { get; set; }
    
    [ForeignKey("KundeId")]
    public virtual Kunde Kunde { get; set; }

    // Business logic methods
    public bool ErGyldigPeriode()
    {
        // Check om perioden starter og slutter på en lørdag
        if (StartDato.DayOfWeek != DayOfWeek.Saturday || SlutDato.DayOfWeek != DayOfWeek.Saturday)
            return false;

        // Check om perioden er mindst en uge
        if ((SlutDato - StartDato).TotalDays < 7)
            return false;

        // Check om perioden er et helt antal uger
        return (SlutDato - StartDato).TotalDays % 7 == 0;
    }

    public int AntalUger()
    {
        return (int)((SlutDato - StartDato).TotalDays / 7);
    }

    public decimal BeregnTotalPris()
    {
        if (Sommerhus == null)
        {
            throw new InvalidOperationException("Kan ikke beregne pris: Sommerhus er ikke tilgængeligt");
        }

        decimal totalPris = 0;
        var sæsonkategorier = SqlConnect.SæsonKategori.ReadAllSæsonKategorier();
        
        // For hver uge i reservationen
        DateTime currentUgeStart = StartDato;
        while (currentUgeStart < SlutDato)
        {
            // Find ugenummer for den aktuelle uge
            int ugeNummer = ISOWeek.GetWeekOfYear(currentUgeStart);
            
            // Find sæsonkategori for denne uge
            var sæsonForUge = sæsonkategorier.FirstOrDefault(s => s.UgeNumre.Contains(ugeNummer));
            
            // Beregn pris for denne uge baseret på basispris og evt. sæsonmultiplikator
            decimal ugePris = Sommerhus.BasePris;
            if (sæsonForUge != null)
            {
                ugePris = Sommerhus.BasePris * (sæsonForUge.PrisMultiplikator / 100.0m);
            }
            
            totalPris += ugePris;
            currentUgeStart = currentUgeStart.AddDays(7);
        }

        return totalPris;
    }

    public string HentUgeNumre()
    {
        var uger = new List<int>();
        var dato = StartDato;
        while (dato <= SlutDato)
        {
            uger.Add(ISOWeek.GetWeekOfYear(dato));
            dato = dato.AddDays(7);
        }
        return string.Join(", ", uger);
    }

    public override string ToString()
    {
        return $"Reservation {ReservationId}\n" +
               $"Sommerhus: {SommerhusId}\n" +
               $"Kunde: {KundeId}\n" +
               $"Periode: {StartDato:dd/MM/yyyy} - {SlutDato:dd/MM/yyyy}\n" +
               $"Total pris: {BeregnTotalPris():C}";
    }
}