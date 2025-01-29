using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {
    namespace SqlConnect {
        public class Reservation {
             public int ReservationId { get; set; }
            public int SommerhusId { get; set; }
            public int KundeId { get; set; }
            public DateTime StartDato { get; set; }
            public DateTime SlutDato { get; set; }

            /// <summary>
            /// Gets the ReservationId.
            /// </summary>
            /// <returns>The ReservationId.</returns>
        
        }
    

    public static class DatabaseHelper {

            // -------------------- CREATE --------------------
            /// <summary>
            /// Creates a new Reservation.
            /// </summary>
            /// <param name="r">The Reservation object to create.</param>
            public static void CreateReservation(Reservation r) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                INSERT INTO Reservation (SommerhusId, KundeId, StartDato, SlutDato)
                VALUES (@SommerhusId, @KundeId, @StartDato, @SlutDato);
                SELECT SCOPE_IDENTITY();
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@SommerhusId", r.SommerhusId);
                        cmd.Parameters.AddWithValue("@KundeId", r.KundeId);
                        cmd.Parameters.AddWithValue("@StartDato", r.StartDato);
                        cmd.Parameters.AddWithValue("@SlutDato", r.SlutDato);

                        object newIdObj = cmd.ExecuteScalar();
                        int newId = Convert.ToInt32(newIdObj);
                        r.ReservationId = newId;
                    }
                }
            }

            // -------------------- READ ALL --------------------
            /// <summary>
            /// Retrieves all Reservations.
            /// </summary>
            /// <returns>A list of Reservation objects.</returns>
            public static List<Reservation> ReadAllReservationer() {
                List<Reservation> liste = new List<Reservation>();
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                SELECT ReservationId, SommerhusId, KundeId, StartDato, SlutDato
                FROM Reservation
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Reservation r = new Reservation();
                                r.ReservationId = reader.GetInt32(0);
                                r.SommerhusId = reader.GetInt32(1);
                                r.KundeId = reader.GetInt32(2);
                                r.StartDato = reader.GetDateTime(3);
                                r.SlutDato = reader.GetDateTime(4);

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
            public static Reservation ReadReservationById(int reservationId) {
                Reservation r = null;
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                SELECT ReservationId, SommerhusId, KundeId, StartDato, SlutDato
                FROM Reservation
                WHERE ReservationId = @ID
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", reservationId);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                r = new Reservation();
                                r.ReservationId = reader.GetInt32(0);
                                r.SommerhusId = reader.GetInt32(1);
                                r.KundeId = reader.GetInt32(2);
                                r.StartDato = reader.GetDateTime(3);
                                r.SlutDato = reader.GetDateTime(4);
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
            public static void UpdateReservation(Reservation r) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                UPDATE Reservation
                SET SommerhusId = @SommerhusId,
                    KundeId = @KundeId,
                    StartDato = @StartDato,
                    SlutDato = @SlutDato
                WHERE ReservationId = @ID
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@SommerhusId", r.SommerhusId);
                        cmd.Parameters.AddWithValue("@KundeId", r.KundeId);
                        cmd.Parameters.AddWithValue("@StartDato", r.StartDato);
                        cmd.Parameters.AddWithValue("@SlutDato", r.SlutDato);
                        cmd.Parameters.AddWithValue("@ID", r.ReservationId);

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
