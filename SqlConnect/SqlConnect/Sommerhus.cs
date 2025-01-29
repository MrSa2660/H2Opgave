using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {
    public class Sommerhus {


        private static int SommerhusId;
        public string Adresse { get; set; }
        public int BasePris { get; set; }
        public int EjerId { get; set; }
        public int OmrådeId { get; set; }
        public int OpsynsmandId { get; set; }
        public string Klassificering { get; set; }
        public int AntalSenge { get; set; }
        public Dictionary<string, decimal> SæsonPriser { get; set; } = new Dictionary<string, decimal>();

        public Sommerhus() {
            SæsonPriser = new Dictionary<string, decimal>();
        }

        /// <summary>
        /// Gets the SommerhusId.
        /// </summary>
        /// <returns>The SommerhusId.</returns>
        public int GetSommerhusId() {
            return SommerhusId;
        }

        /// <summary>
        /// Sets the SommerhusId.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetSommerhusId(int value) {
            SommerhusId = value;
        }

        /// <summary>
        /// Creates a new Sommerhus.
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
                    s.SetSommerhusId(newId);

                    // OBS: Hvis du vil gemme SæsonPriser i en separat tabel, kan du kalde en hjælper her, fx:
                    // SaveSæsonPriser(s.GetSommerhusId(), s.SæsonPriser);
                }
            }

        }

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
                            s.SetSommerhusId(reader.GetInt32(0));
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
                            s.SetSommerhusId(reader.GetInt32(0));
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
                    cmd.Parameters.AddWithValue("@SommerhusId", s.GetSommerhusId());

                    cmd.ExecuteNonQuery();
                }

                // Evt. opdatering af SæsonPriser i en separat tabel
                // ClearSæsonPriser(s.GetSommerhusId());
                // SaveSæsonPriser(s.GetSommerhusId(), s.SæsonPriser);
            }
        }

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

