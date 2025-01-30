using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

public enum SæsonType
{
    Super,
    Høj,
    Mellem,
    Lav
}


public class SæsonKategori {
    private int KategoriID;
    public string Navn { get; set; }
    public int Pris { get; set; }
    public string Uger { get; set; }
    public List<int> UgeNumre { get; set; } = new List<int>();
    public List<Sommerhus> Sommerhuse { get; set; } = new List<Sommerhus>();

    public SæsonKategori() {
        Sommerhuse = new List<Sommerhus>();
        UgeNumre = new List<int>();
    }

    public int GetKategoriId() {
        return KategoriID;
    }

    public void SetKategoriId(int value) {
        KategoriID = value;
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
        decimal prisFaktor = (decimal)Pris / 100;
        return basisPris * prisFaktor;
    }

    public string HentSæsonBeskrivelse()
    {
        return $"Sæson: {Navn}\n" +
               $"Uger: {string.Join(", ", UgeNumre)}\n" +
               $"Prisfaktor: {BeregnPrisForUge(1000m) / 1000m:F1}x basis pris";
    }

    public override string ToString()
    {
        return HentSæsonBeskrivelse();
    }

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

    public static class DatabaseHelper {
        // ---------------------------------------------------------
        // CREATE - Opretter en ny SæsonKategori i databasen
        // ---------------------------------------------------------
        /// <summary>
        /// Creates a new SæsonKategori in the database.
        /// </summary>
        /// <param name="sk">The SæsonKategori object to create.</param>
        public static void CreateSæsonKategori(SæsonKategori sk) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                        INSERT INTO SæsonKategori (Navn, Uger, Pris)
                        VALUES (@Navn, @Uger, @Pris);
                        SELECT SCOPE_IDENTITY();
                    ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Navn", sk.Navn);
                    cmd.Parameters.AddWithValue("@Uger", sk.Uger ?? "");
                    cmd.Parameters.AddWithValue("@Pris", sk.Pris);

                    object newIdObj = cmd.ExecuteScalar();
                    int newId = Convert.ToInt32(newIdObj);
                    sk.KategoriID = newId;
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
                    SELECT KategoriID, Navn, Uger, Pris
                    FROM SæsonKategori
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            SæsonKategori sk = new SæsonKategori();
                            sk.KategoriID = reader.GetInt32(0);
                            sk.Navn = reader.GetString(1).TrimEnd();
                            sk.Uger = reader.GetString(2).TrimEnd();
                            sk.Pris = reader.GetInt32(3);
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
        /// <param name="kategoriId">The ID of the SæsonKategori to retrieve.</param>
        /// <returns>The SæsonKategori object.</returns>
        public static SæsonKategori ReadSæsonKategoriById(int kategoriId) {
            SæsonKategori sk = null;

            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                    SELECT KategoriID, Navn, Uger, Pris
                    FROM SæsonKategori
                    WHERE KategoriId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", kategoriId);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            sk = new SæsonKategori();
                            sk.KategoriID = reader.GetInt32(0);
                            sk.Navn = reader.GetString(1).TrimEnd();
                            sk.Uger = reader.GetString(2).TrimEnd();
                            sk.Pris = reader.GetInt32(3);
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
                    SET Navn = @Navn,
                        Uger = @Uger,   
                        Pris = @Pris
                    WHERE KategoriId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Navn", sk.Navn);
                    cmd.Parameters.AddWithValue("@Uger", sk.Uger);
                    cmd.Parameters.AddWithValue("@Pris", sk.Pris);
                    cmd.Parameters.AddWithValue("@ID", sk.KategoriID);

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
        /// <param name="KategoriId">The ID of the SæsonKategori to delete.</param>
        public static void DeleteSæsonKategori(int KategoriId) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                // Hvis du har en separat tabel til UgeNumre, skal de slettes først
                // DeleteUgeNumre(sæsonKategoriId);

                string query = @"
                    DELETE FROM SæsonKategori
                    WHERE KategoriId = @ID
                ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", KategoriId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}