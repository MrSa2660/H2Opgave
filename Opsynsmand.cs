using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Opsynsmand {
    // Ændre fra property til privat felt
    private int OpsynsmandId;
    public string Rolle { get; set; }
    public string Navn { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    
    // Ændre fra virtual ICollection til List med initialisering
    public List<Sommerhus> Sommerhuse { get; set; } = new List<Sommerhus>();

    // Tilføj konstruktør
    public Opsynsmand() {
        Sommerhuse = new List<Sommerhus>();
    }

    // Tilføj Get/Set metoder for OpsynsmandId
    public int GetOpsynsmandId() {
        return OpsynsmandId;
    }

    public void SetOpsynsmandId(int value) {
        OpsynsmandId = value;
    }

    public static void CreateOpsynsmand(Opsynsmand o) {
        using (SqlConnection con = new SqlConnection(program.connectionString)) {
            con.Open();

            // Bemærk: Da OpsynsmandID ikke er IDENTITY, skal du selv angive ID.
            // Ellers får du en fejl, hvis kolonnen er NOT NULL (og PK).
            string query = @"
                INSERT INTO Opsynsmand (OpsynsmandID, [Rolle], Navn, Telefon, Email)
                VALUES (@ID, @Rolle, @Navn, @Telefon, @Email);
            ";

            using (SqlCommand cmd = new SqlCommand(query, con)) {
                cmd.Parameters.AddWithValue("@ID", o.GetOpsynsmandId());  // Ændret fra OpsynsmandID
                cmd.Parameters.AddWithValue("@Rolle", o.Rolle ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Navn", o.Navn ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefon", o.Telefon ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", o.Email ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }
    }

    // ---------------------------------------------------------
    // READ (1): Henter alle Opsynsmænd
    // ---------------------------------------------------------
    public static List<Opsynsmand> ReadAllOpsynsmænd() {
        List<Opsynsmand> liste = new List<Opsynsmand>();

        using (SqlConnection con = new SqlConnection(program.connectionString)) {
            con.Open();
            string query = @"
                SELECT OpsynsmandID, [Rolle], Navn, Telefon, Email
                FROM Opsynsmand
            ";

            using (SqlCommand cmd = new SqlCommand(query, con)) {
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        Opsynsmand o = new Opsynsmand();
                        o.SetOpsynsmandId(reader.GetInt32(0));  // Ændret fra OpsynsmandID assignment
                        o.Rolle = reader.IsDBNull(1) ? null : reader.GetString(1).TrimEnd();
                        o.Navn = reader.IsDBNull(2) ? null : reader.GetString(2).TrimEnd();
                        o.Telefon = reader.IsDBNull(3) ? null : reader.GetString(3).TrimEnd();
                        o.Email = reader.IsDBNull(4) ? null : reader.GetString(4).TrimEnd();

                        liste.Add(o);
                    }
                }
            }
        }

        return liste;
    }

    // ---------------------------------------------------------
    // READ (2): Henter én Opsynsmand (via ID)
    // ---------------------------------------------------------
    public static Opsynsmand ReadOpsynsmandById(int opsynsmandId) {
        Opsynsmand o = null;
        using (SqlConnection con = new SqlConnection(program.connectionString)) {
            con.Open();
            string query = @"
                SELECT OpsynsmandID, [Rolle], Navn, Telefon, Email
                FROM Opsynsmand
                WHERE OpsynsmandID = @ID
            ";

            using (SqlCommand cmd = new SqlCommand(query, con)) {
                cmd.Parameters.AddWithValue("@ID", opsynsmandId);

                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        o = new Opsynsmand();
                        o.SetOpsynsmandId(reader.GetInt32(0));  // Ændret fra OpsynsmandID assignment
                        o.Rolle = reader.IsDBNull(1) ? null : reader.GetString(1).TrimEnd();
                        o.Navn = reader.IsDBNull(2) ? null : reader.GetString(2).TrimEnd();
                        o.Telefon = reader.IsDBNull(3) ? null : reader.GetString(3).TrimEnd();
                        o.Email = reader.IsDBNull(4) ? null : reader.GetString(4).TrimEnd();
                    }
                }
            }
        }
        return o;
    }

    // ---------------------------------------------------------
    // UPDATE - Opdaterer en eksisterende Opsynsmand (via ID)
    // ---------------------------------------------------------
    public static void UpdateOpsynsmand(Opsynsmand o) {
        using (SqlConnection con = new SqlConnection(program.connectionString)) {
            con.Open();
            string query = @"
                UPDATE Opsynsmand
                SET [Rolle] = @Rolle,
                    Navn = @Navn,
                    Telefon = @Telefon,
                    Email = @Email
                WHERE OpsynsmandID = @ID
            ";

            using (SqlCommand cmd = new SqlCommand(query, con)) {
                cmd.Parameters.AddWithValue("@Rolle", o.Rolle ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Navn", o.Navn ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefon", o.Telefon ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", o.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", o.GetOpsynsmandId());  // Ændret fra OpsynsmandID

                cmd.ExecuteNonQuery();
            }
        }
    }

    // ---------------------------------------------------------
    // DELETE - Sletter en Opsynsmand (via ID)
    // ---------------------------------------------------------
    public static void DeleteOpsynsmand(int opsynsmandID) {
        using (SqlConnection con = new SqlConnection(program.connectionString)) {
            con.Open();
            string query = @"
                DELETE FROM Opsynsmand
                WHERE OpsynsmandID = @ID
            ";

            using (SqlCommand cmd = new SqlCommand(query, con)) {
                cmd.Parameters.AddWithValue("@ID", opsynsmandID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
  

