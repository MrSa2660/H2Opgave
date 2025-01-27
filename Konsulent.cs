public class Konsulent {
    private int KonsulentId;
    public string Navn { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public string Type { get; set; }  // Udlejningskonsulent eller Kundekonsulent
    public List<Område> AnsvarligForOmråder { get; set; } = new List<Område>();
    
    public Konsulent() {
        AnsvarligForOmråder = new List<Område>();
    }
} 