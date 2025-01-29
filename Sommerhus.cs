using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Sommerhus {
    [Key]
    public int SommerhusId { get; set; }
    
    public int OmrådeId { get; set; }
    
  
    public string Adresse { get; set; }
    

    public string Fstandard { get; set; }
    
    public int EjerId { get; set; }
    

    public string Klassifikation { get; set; }
    
    public int OpsynsmandId { get; set; }
    
    public decimal BasePris { get; set; }
    
    // Navigation properties
    public virtual Område Område { get; set; }

    public virtual Ejer Ejer { get; set; }
    
 
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

    public static class DatabaseHelper {
        // ---------------------------------------------------------
        // CREATE - Opretter et nyt Sommerhus i databasen
        // ---------------------------------------------------------
        /// <summary>
        /// Creates a new Sommerhus in the database.
        /// </summary>
        /// <param name="s">The Sommerhus object to create.</param>
        public static void CreateSommerhus(Sommerhus s) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    INSERT INTO Sommerhus (Adresse, BasePris, EjerId, OmrådeId, OpsynsmandId, Klassificering, AntalSenge)
                    VALUES (@Adresse, @BasePris, @EjerId, @OmrådeId, @OpsynsmandId, @Klassificering, @AntalSenge);
                    SELECT SCOPE_IDENTITY();
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Adresse", s.Adresse);
                    cmd.Parameters.AddWithValue("@BasePris", s.BasePris);
                    cmd.Parameters.AddWithValue("@EjerId", s.EjerId);
                    cmd.Parameters.AddWithValue("@OmrådeId", s.OmrådeId);
                    cmd.Parameters.AddWithValue("@OpsynsmandId", s.OpsynsmandId);
                    cmd.Parameters.AddWithValue("@Klassificering", (object)s.Klassificering ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AntalSenge", s.AntalSenge);

                    // Hent ID fra SCOPE_IDENTITY()
                    object newIdObj = cmd.ExecuteScalar();
                    int newId = Convert.ToInt32(newIdObj);

                    // Gem ID i Sommerhus-objektet
                    s.SommerhusId = newId;

                    // OBS: Hvis du vil gemme SæsonPriser i en separat tabel, kan du kalde en hjælper her, fx:
                    // SaveSæsonPriser(s.GetSommerhusId(), s.SæsonPriser);
                }
            }
        }

        // ---------------------------------------------------------
        // READ (1) - Henter alle Sommerhuse
        // ---------------------------------------------------------
        /// <summary>
        /// Reads all Sommerhuse.
        /// </summary>
        /// <returns>A list of Sommerhus objects.</returns>
        public static List<Sommerhus> ReadAllSommerhuse() {
            List<Sommerhus> liste = new List<Sommerhus>();

            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    SELECT SommerhusId, Adresse, BasePris, EjerId, OmrådeId, OpsynsmandId, Klassificering, AntalSenge
                    FROM Sommerhus
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Sommerhus s = new Sommerhus();
                            s.SommerhusId = reader.GetInt32(0);
                            s.Adresse = reader.GetString(1);
                            s.BasePris = reader.GetInt32(2);
                            s.EjerId = reader.GetInt32(3);
                            s.OmrådeId = reader.GetInt32(4);
                            s.OpsynsmandId = reader.GetInt32(5);
                            s.Klassificering = reader.IsDBNull(6) ? null : reader.GetString(6);
                            s.AntalSenge = reader.GetInt32(7);

                            // Her kunne du også hente SæsonPriser fra en separat tabel
                            // s.SæsonPriser = LoadSæsonPriser(s.GetSommerhusId());

                            liste.Add(s);
                        }
                    }
                }
            }

            return liste;
        }

        // ---------------------------------------------------------
        // READ (2) - Henter ét Sommerhus via ID
        // ---------------------------------------------------------
        /// <summary>
        /// Reads a Sommerhus by its ID.
        /// </summary>
        /// <param name="sommerhusId">The ID of the Sommerhus to read.</param>
        /// <returns>The Sommerhus object.</returns>
        public static Sommerhus ReadSommerhusById(int sommerhusId) {
            Sommerhus s = null;

            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    SELECT SommerhusId, Adresse, BasePris, EjerId, OmrådeId, OpsynsmandId, Klassificering, AntalSenge
                    FROM Sommerhus
                    WHERE SommerhusId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", sommerhusId);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            s = new Sommerhus();
                            s.SommerhusId = reader.GetInt32(0);
                            s.Adresse = reader.GetString(1);
                            s.BasePris = reader.GetInt32(2);
                            s.EjerId = reader.GetInt32(3);
                            s.OmrådeId = reader.GetInt32(4);
                            s.OpsynsmandId = reader.GetInt32(5);
                            s.Klassificering = reader.IsDBNull(6) ? null : reader.GetString(6);
                            s.AntalSenge = reader.GetInt32(7);

                            // Indlæs evt. SæsonPriser
                            // s.SæsonPriser = LoadSæsonPriser(s.GetSommerhusId());
                        }
                    }
                }
            }

            return s;
        }

        // ---------------------------------------------------------
        // UPDATE - Opdaterer et eksisterende Sommerhus
        // ---------------------------------------------------------
        /// <summary>
        /// Updates a Sommerhus.
        /// </summary>
        /// <param name="s">The Sommerhus object to update.</param>
        public static void UpdateSommerhus(Sommerhus s) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    UPDATE Sommerhus
                    SET Adresse = @Adresse,
                        BasePris = @BasePris,
                        EjerId = @EjerId,
                        OmrådeId = @OmrådeId,
                        OpsynsmandId = @OpsynsmandId,
                        Klassificering = @Klassificering,
                        AntalSenge = @AntalSenge
                    WHERE SommerhusId = @SommerhusId
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Adresse", s.Adresse);
                    cmd.Parameters.AddWithValue("@BasePris", s.BasePris);
                    cmd.Parameters.AddWithValue("@EjerId", s.EjerId);
                    cmd.Parameters.AddWithValue("@OmrådeId", s.OmrådeId);
                    cmd.Parameters.AddWithValue("@OpsynsmandId", s.OpsynsmandId);
                    cmd.Parameters.AddWithValue("@Klassificering", (object)s.Klassificering ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AntalSenge", s.AntalSenge);
                    cmd.Parameters.AddWithValue("@SommerhusId", s.SommerhusId);

                    cmd.ExecuteNonQuery();
                }

                // Evt. opdatering af SæsonPriser i en separat tabel
                // ClearSæsonPriser(s.GetSommerhusId());
                // SaveSæsonPriser(s.GetSommerhusId(), s.SæsonPriser);
            }
        }

        // ---------------------------------------------------------
        // DELETE - Sletter et Sommerhus via ID
        // ---------------------------------------------------------
        /// <summary>
        /// Deletes a Sommerhus by its ID.
        /// </summary>
        /// <param name="sommerhusId">The ID of the Sommerhus to delete.</param>
        public static void DeleteSommerhus(int sommerhusId) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    DELETE FROM Sommerhus
                    WHERE SommerhusId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", sommerhusId);
                    cmd.ExecuteNonQuery();
                }
            }

            // Evt. slette række(r) i en separat tabel for SæsonPriser
        }
    }
}