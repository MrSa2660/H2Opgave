using Microsoft.Data.SqlClient;
using SqlConnect.SqlConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {
    class Kunde {

        private int KundeId { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        /// <summary>
        /// Helper class for database operations related to Kunde.
        /// </summary>
        public static class DatabaseHelper {

            /// <summary>
            /// Create a new Kunde.
            /// </summary>
            /// <param name="k">The Kunde object to create.</param>
            public static void CreateKunde(Kunde k) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    INSERT INTO Kunde (Kunde (KundeID, Adresse, Email, Tlfnr))
                    VALUES (@KundeID, @Adresse, @Email, @Tlfnr);
                    SELECT SCOPE_IDENTITY();
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@KundeID", k.KundeId);
                        cmd.Parameters.AddWithValue("@Adresse", k.Adresse);
                        cmd.Parameters.AddWithValue("@Email", k.Email);
                        cmd.Parameters.AddWithValue("@Tlfnr", k.Telefon);

                        object newIdObj = cmd.ExecuteScalar();
                        int newId = Convert.ToInt32(newIdObj);
                        k.KundeId = (newId);
                    }
                }
            }
            // -------------------- READ ALL --------------------
            /// <summary>
            /// Retrieves all Reservations.
            /// </summary>
            /// <returns>A list of Reservation objects.</returns>
            public static List<Kunde> ReadAllKunde() {
                List<Kunde> liste = new List<Kunde>();
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                SELECT KundeID, Adresse, Email, Tlfnr
                FROM Kunde
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Kunde r = new Kunde();
                                r.KundeId = (reader.GetInt32(0));
                                r.KundeId = reader.GetInt32(1);
                                r.Email = reader.GetString(2);
                                r.Telefon = reader.GetString(3);
                                r.Adresse = reader.GetString(4);

                                liste.Add(r);
                            }
                        }
                    }
                }
                return liste;
            }

            // -------------------- READ ONE --------------------
            /// <summary>
            /// Retrieves a Reservation by its ID.
            /// </summary>
            /// <param name="reservationId">The ID of the Reservation to retrieve.</param>
            /// <returns>The Reservation object.</returns>
            public static Kunde ReadKundeById(int KundeId) {
                Kunde r = null;
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                SELECT KundeId, SommerhusId, KundeId, StartDato, SlutDato
                FROM Kunde
                WHERE KundeId = @ID
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", KundeId);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                r = new Kunde();
                                r.KundeId = (reader.GetInt32(0));
                                r.KundeId = reader.GetInt32(1);
                                r.Email = reader.GetString(2);
                                r.Telefon = reader.GetString(3);
                                r.Adresse = reader.GetString(4);
                            }
                        }
                    }
                }
                return r;
            }

            // -------------------- UPDATE --------------------
            /// <summary>
            /// Updates a Reservation.
            /// </summary>
            /// <param name="r">The Reservation object to update.</param>
            public static void UpdateKunde(Kunde r) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                UPDATE Kunde
                SET KundeId = @KundeId,
                    Email = @Email,
                    Tlfnr = @Tlfnr,
                    Adresse = @Adresse
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@KundeId", r.KundeId);
                        cmd.Parameters.AddWithValue("@Email", r.Email);
                        cmd.Parameters.AddWithValue("@Tlfnr", r.Telefon);
                        cmd.Parameters.AddWithValue("@Adresse", r.Adresse);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // -------------------- DELETE --------------------
            /// <summary>
            /// Deletes a Reservation by its ID.
            /// </summary>
            /// <param name="reservationId">The ID of the Reservation to delete.</param>
            public static void DeleteReservation(int reservationId) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                DELETE FROM Reservation
                WHERE ReservationId = @ID
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", reservationId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
