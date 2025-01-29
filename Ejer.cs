using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ejer
{
    public int EjerId { get; set; }
    public string Navn { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public string Adresse { get; set; }
    public DateTime KontraktStartDato { get; set; }
    public DateTime KontraktSlutDato { get; set; }

    // Navigation property
    public virtual ICollection<Sommerhus> Sommerhuse { get; set; }


    public static class DatabaseHelper {

        /// <summary>
        /// CREATE - Opretter en ny Ejer i databasen og sætter EjerId på objektet.
        /// </summary>
        /// <param name="e">The Ejer object to create.</param>
        public static void CreateEjer(Ejer e) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                        INSERT INTO Ejer (Navn, Email, Telefon, Adresse, KontraktStartDato, KontraktSlutDato)
                        VALUES (@Navn, @Email, @Telefon, @Adresse, @StartDato, @SlutDato);
                        SELECT SCOPE_IDENTITY(); -- Hent nyoprettet ID
                    ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Navn", e.Navn);
                    cmd.Parameters.AddWithValue("@Email", e.Email);
                    cmd.Parameters.AddWithValue("@Telefon", e.Telefon);
                    cmd.Parameters.AddWithValue("@Adresse", e.Adresse);
                    cmd.Parameters.AddWithValue("@StartDato", e.KontraktStartDato);
                    cmd.Parameters.AddWithValue("@SlutDato", e.KontraktSlutDato);

                    // ExecuteScalar returnerer første kolonne i den første række fra SCOPE_IDENTITY()
                    object newIdObj = cmd.ExecuteScalar();

                    // SCOPE_IDENTITY() returnerer typisk et decimal, så vi caster til int:
                    int newId = Convert.ToInt32(newIdObj);
                    e.SetEjerId(newId);
                }
            }
        }

        /// <summary>
        /// READ (1): Henter alle Ejer-poster i databasen.
        /// </summary>
        /// <returns>A list of all Ejer objects.</returns>
        public static List<Ejer> ReadAllEjere() {
            List<Ejer> ejere = new List<Ejer>();

            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                        SELECT EjerID, Navn, Email, Telefon, Adresse, 
                               KontraktStartDato, KontraktSlutDato
                        FROM Ejer
                    ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Ejer temp = new Ejer();
                            temp.EjerId(reader.GetInt32(0));
                            temp.Navn = reader.GetString(1);
                            temp.Email = reader.GetString(2);
                            temp.Telefon = reader.GetString(3);
                            temp.Adresse = reader.GetString(4);
                            temp.KontraktStartDato = reader.GetDateTime(5);
                            temp.KontraktSlutDato = reader.GetDateTime(6);

                            ejere.Add(temp);
                        }
                    }
                }
            }

            return ejere;
        }

        /// <summary>
        /// READ (2): Henter én Ejer fra databasen (via ID).
        /// </summary>
        /// <param name="ejerId">The EjerId to search for.</param>
        /// <returns>The Ejer object with the specified EjerId, or null if not found.</returns>
        public static Ejer ReadEjerById(int ejerId) {
            Ejer ejer = null;

            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                        SELECT EjerID, Navn, Email, Telefon, Adresse,
                               KontraktStartDato, KontraktSlutDato
                        FROM Ejer
                        WHERE EjerID = @EjerID
                    ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@EjerID", ejerId);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            ejer = new Ejer();
                            ejer.EjerId(reader.GetInt32(0));
                            ejer.Navn = reader.GetString(1);
                            ejer.Email = reader.GetString(2);
                            ejer.Telefon = reader.GetString(3);
                            ejer.Adresse = reader.GetString(4);
                            ejer.KontraktStartDato = reader.GetDateTime(5);
                            ejer.KontraktSlutDato = reader.GetDateTime(6);
                        }
                    }
                }
            }

            return ejer;
        }

        /// <summary>
        /// UPDATE - Opdaterer en eksisterende Ejer i databasen (via EjerID).
        /// </summary>
        /// <param name="e">The Ejer object to update.</param>
        public static void UpdateEjer(Ejer e) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                        UPDATE Ejer
                        SET Navn = @Navn,
                            Email = @Email,
                            Telefon = @Telefon,
                            Adresse = @Adresse,
                            KontraktStartDato = @StartDato,
                            KontraktSlutDato = @SlutDato
                        WHERE EjerID = @EjerID
                    ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@Navn", e.Navn);
                    cmd.Parameters.AddWithValue("@Email", e.Email);
                    cmd.Parameters.AddWithValue("@Telefon", e.Telefon);
                    cmd.Parameters.AddWithValue("@Adresse", e.Adresse);
                    cmd.Parameters.AddWithValue("@StartDato", e.KontraktStartDato);
                    cmd.Parameters.AddWithValue("@SlutDato", e.KontraktSlutDato);
                    cmd.Parameters.AddWithValue("@EjerID", e.GetEjerId());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// DELETE - Sletter en Ejer (via EjerID).
        /// </summary>
        /// <param name="ejerId">The EjerId to delete.</param>
        public static void DeleteEjer(int ejerId) {
            using (SqlConnection con = new SqlConnection(program.connectionString)) {
                con.Open();

                string query = @"
                        DELETE FROM Ejer
                        WHERE EjerID = @EjerID
                    ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@EjerID", ejerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}