using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {
    
    ///<summary>
    /// Represents a SæsonKategori object.
    /// </summary>
    public class SæsonKategori {
        private int SæsonKategoriId;
        public string Kategori { get; set; }  // "super", "høj", "mellem", "lav"
        public int PrisMultiplikator { get; set; }
        public List<int> UgeNumre { get; set; } = new List<int>();

        public SæsonKategori() {
            UgeNumre = new List<int>();
        }

        // Get/set til SæsonKategoriId
        /// <summary>
        /// Gets the SæsonKategoriId.
        /// </summary>
 

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
}
