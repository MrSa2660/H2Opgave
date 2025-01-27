using System;
using System.Collections.Generic;

public class Ejer
{
    private int EjerId;
    public string Navn { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public string Adresse { get; set; }
    public List<Sommerhus> Sommerhuse { get; set; } = new List<Sommerhus>();
    public DateTime KontraktStartDato { get; set; }
    public DateTime KontraktSlutDato { get; set; }
    
    public Ejer() {
        Sommerhuse = new List<Sommerhus>();
    }
}