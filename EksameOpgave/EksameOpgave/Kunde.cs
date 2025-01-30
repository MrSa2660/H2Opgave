using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class Kunde {

    private int KundeId;


    public string Email { get; set; }

    public string Tlfnr { get; set; }

    public string Adresse { get; set; }

    public List<Reservation> Reservationer { get; set; } = new List<Reservation>();

    public Kunde() {
        Reservationer = new List<Reservation>();
        Email = string.Empty;
        Tlfnr = string.Empty;
        Adresse = string.Empty;
    }

    public int GetKundeId() {
        return KundeId;
    }

    public void SetKundeId(int value) {
        KundeId = value;
    }

    public static class DatabaseHelper {

        /// <summary>
        /// Create a new Kunde.
        /// </summary>
        /// <param name="k">The Kunde object to create.</param>
        public static void CreateKunde(Kunde k) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();
                string query = @"
                INSERT INTO Kunde (Adresse, Email, Tlfnr)
                VALUES (@Adresse, @Email, @Tlfnr);
                SELECT SCOPE_IDENTITY();
            ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Adresse", k.Adresse);
                    cmd.Parameters.AddWithValue("@Email", k.Email);
                    cmd.Parameters.AddWithValue("@Tlfnr", k.Tlfnr);

                    object newIdObj = cmd.ExecuteScalar();
                    int newId = Convert.ToInt32(newIdObj);
                    k.SetKundeId(newId);
                }
            }
        }
        // -------------------- READ ALL --------------------
        /// <summary>
        /// Retrieves all Reservations.
        /// </summary>
        /// <returns>A list of Reservation objects.</returns>
        public static List<Kunde> ReadAllKunde() {
            try {
                List<Kunde> liste = new List<Kunde>();

                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    SELECT KundeID, Email, Tlfnr, Adresse 
                    FROM Kunde
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Kunde r = new Kunde();
                                r.SetKundeId(reader.GetInt32(0));
                                r.Email = reader.GetString(1).TrimEnd();
                                r.Tlfnr = reader.GetString(2).TrimEnd();
                                r.Adresse = reader.GetString(3).TrimEnd();

                                liste.Add(r);
                            }
                        }
                    }
                }
                return liste;
            }
            catch (Exception e) {
                Console.WriteLine("test");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return new List<Kunde>();
            }
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
                SELECT KundeID, Email, Tlfnr, Adresse 
                FROM Kunde
                WHERE KundeId = @ID
            ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", KundeId);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            r = new Kunde();
                            r.SetKundeId(reader.GetInt32(0));
                            r.Email = reader.GetString(1).TrimEnd();
                            r.Tlfnr = reader.GetString(2).TrimEnd();
                            r.Adresse = reader.GetString(3).TrimEnd();
                        }
                    }
                }
            }
            return r ?? new Kunde();
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
                SET Email = @Email,
                    Tlfnr = @Tlfnr,
                    Adresse = @Adresse
                WHERE KundeId = @ID
            ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Email", r.Email);
                    cmd.Parameters.AddWithValue("@Tlfnr", r.Tlfnr);
                    cmd.Parameters.AddWithValue("@Adresse", r.Adresse);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // -------------------- DELETE --------------------
        /// <summary>
        /// Deletes a Reservation by its ID.
        /// </summary>
        /// <param name="KundeId">The ID of the Reservation to delete.</param>
        public static void DeleteKunde(int kundeId) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();
                string query = @"
                DELETE FROM kunde
                WHERE KundeId = @ID
            ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", kundeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
