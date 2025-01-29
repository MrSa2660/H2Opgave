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


public class SæsonKategori {

    public int KategoriId { get; set; }
    
   
    public SæsonType Type { get; set; }
    

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
    public static void CreateSæsonKategori(SæsonKategori sk) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    INSERT INTO SæsonKategori (Kategori, PrisMultiplikator)
                    VALUES (@Kategori, @PrisMultiplikator);
                    SELECT SCOPE_IDENTITY();
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Kategori", sk.Kategori);
                    cmd.Parameters.AddWithValue("@PrisMultiplikator", sk.PrisMultiplikator);

                    object newIdObj = cmd.ExecuteScalar();
                    int newId = Convert.ToInt32(newIdObj);

                    sk.SæsonKategoriId = newId;

                    // Hvis du vil gemme UgeNumre i en separat tabel:
                    // SaveUgeNumre(sk.GetSæsonKategoriId(), sk.UgeNumre);
                }
            }
        }

        // ---------------------------------------------------------
        // READ (1) - Henter alle SæsonKategorier
        // ---------------------------------------------------------
        /// <summary>
        /// Retrieves all SæsonKategorier from the database.
        /// </summary>
        /// <returns>A list of SæsonKategori objects.</returns>
        public static List<SæsonKategori> ReadAllSæsonKategorier() {
            List<SæsonKategori> liste = new List<SæsonKategori>();

            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    SELECT SæsonKategoriId, Kategori, PrisMultiplikator
                    FROM SæsonKategori
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            SæsonKategori sk = new SæsonKategori();
                            sk.SæsonKategoriId = reader.GetInt32(0);
                            sk.Kategori = reader.GetString(1);
                            sk.PrisMultiplikator = reader.GetInt32(2);

                            // Hent evt. UgeNumre fra separat tabel eller JSON
                            // sk.UgeNumre = LoadUgeNumre(sk.GetSæsonKategoriId());

                            liste.Add(sk);
                        }
                    }
                }
            }

            return liste;
        }

        // ---------------------------------------------------------
        // READ (2) - Henter én SæsonKategori via ID
        // ---------------------------------------------------------
        /// <summary>
        /// Retrieves a SæsonKategori from the database by ID.
        /// </summary>
        /// <param name="sæsonKategoriId">The ID of the SæsonKategori to retrieve.</param>
        /// <returns>The SæsonKategori object.</returns>
        public static SæsonKategori ReadSæsonKategoriById(int sæsonKategoriId) {
            SæsonKategori sk = null;

            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    SELECT SæsonKategoriId, Kategori, PrisMultiplikator
                    FROM SæsonKategori
                    WHERE SæsonKategoriId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", sæsonKategoriId);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            sk = new SæsonKategori();
                            sk.SæsonKategoriId = reader.GetInt32(0);
                            sk.Kategori = reader.GetString(1);
                            sk.PrisMultiplikator = reader.GetInt32(2);

                            // sk.UgeNumre = LoadUgeNumre(sk.GetSæsonKategoriId());
                        }
                    }
                }
            }

            return sk;
        }

        // ---------------------------------------------------------
        // UPDATE - Opdaterer en eksisterende SæsonKategori
        // ---------------------------------------------------------
        /// <summary>
        /// Updates an existing SæsonKategori in the database.
        /// </summary>
        /// <param name="sk">The SæsonKategori object to update.</param>
        public static void UpdateSæsonKategori(SæsonKategori sk) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    UPDATE SæsonKategori
                    SET Kategori = @Kategori,
                        PrisMultiplikator = @PrisMultiplikator
                    WHERE SæsonKategoriId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Kategori", sk.Kategori);
                    cmd.Parameters.AddWithValue("@PrisMultiplikator", sk.PrisMultiplikator);
                    cmd.Parameters.AddWithValue("@ID", sk.SæsonKategoriId);

                    cmd.ExecuteNonQuery();
                }
            }

            // Hvis du bruger en separat tabel til UgeNumre:
            // ClearUgeNumre(sk.GetSæsonKategoriId());
            // SaveUgeNumre(sk.GetSæsonKategoriId(), sk.UgeNumre);
        }

        // ---------------------------------------------------------
        // DELETE - Sletter en SæsonKategori via ID
        // ---------------------------------------------------------
        /// <summary>
        /// Deletes a SæsonKategori from the database by ID.
        /// </summary>
        /// <param name="sæsonKategoriId">The ID of the SæsonKategori to delete.</param>
        public static void DeleteSæsonKategori(int sæsonKategoriId) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                // Hvis du har en separat tabel til UgeNumre, skal de slettes først
                // DeleteUgeNumre(sæsonKategoriId);

                string query = @"
                    DELETE FROM SæsonKategori
                    WHERE SæsonKategoriId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", sæsonKategoriId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
}