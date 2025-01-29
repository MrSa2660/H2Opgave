using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

[Table("Sommerhus")]
public class Sommerhus {
    [Key]
    public int SommerhusId { get; set; }
    
    public int OmrådeId { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Adresse { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Fstandard { get; set; }
    
    public int EjerId { get; set; }
    
    [Column(TypeName = "nchar")]
    public string Klassifikation { get; set; }
    
    public int OpsynsmandId { get; set; }
    
    public decimal BasePris { get; set; }
    
    // Navigation properties
    [ForeignKey("OmrådeId")]
    public virtual Område Område { get; set; }
    
    [ForeignKey("EjerId")]
    public virtual Ejer Ejer { get; set; }
    
    [ForeignKey("OpsynsmandId")]
    public virtual Opsynsmand Opsynsmand { get; set; }
    
    public virtual ICollection<Reservation> Reservationer { get; set; }

    public Sommerhus()
    {
        Reservationer = new List<Reservation>();
    }

    public bool ErTilgængeligIUge(int ugeNummer)
    {
        foreach (var reservation in Reservationer)
        {
            // Konverter start og slut datoer til ugenumre
            int startUge = ISOWeek.GetWeekOfYear(reservation.StartDato);
            int slutUge = ISOWeek.GetWeekOfYear(reservation.SlutDato);
            
            if (ugeNummer >= startUge && ugeNummer <= slutUge)
            {
                return false;
            }
        }
        return true;
    }

    public List<Reservation> HentReservationer(DateTime fraDato, DateTime tilDato)
    {
        return Reservationer
            .Where(r => !(r.SlutDato < fraDato || r.StartDato > tilDato))
            .OrderBy(r => r.StartDato)
            .ToList();
    }

    public List<Reservation> HentAktiveReservationer()
    {
        var iDag = DateTime.Today;
        return Reservationer
            .Where(r => r.StartDato <= iDag && r.SlutDato >= iDag)
            .ToList();
    }

    public List<Reservation> HentKommendeReservationer()
    {
        var iDag = DateTime.Today;
        return Reservationer
            .Where(r => r.StartDato > iDag)
            .OrderBy(r => r.StartDato)
            .ToList();
    }

    public List<int> HentLedigePerioder(int år)
    {
        var ledigeUger = new List<int>();
        for (int uge = 1; uge <= 53; uge++)
        {
            if (ErTilgængeligIUge(uge))
            {
                ledigeUger.Add(uge);
            }
        }
        return ledigeUger;
    }

    public override string ToString()
    {
        return $"Sommerhus {SommerhusId}\n" +
               $"Adresse: {Adresse}\n" +
               $"Standard: {Fstandard}\n" +
               $"Klassifikation: {Klassifikation}\n" +
               $"Ugentlig basispris: {BasePris:C}";
    }
}