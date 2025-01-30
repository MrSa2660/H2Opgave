using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

public class Område {
   
    private int OmrådeId;
    
    public string Navn { get; set; }
    
    public int KonsulentId { get; set; }
    
    public Konsulent Konsulent { get; set; }
    
    public List<Sommerhus> Sommerhuse { get; set; } = new List<Sommerhus>();

    public Område()
    {
        Sommerhuse = new List<Sommerhus>();
    }

    public int GetOmrådeId() {
        return OmrådeId;
    }

    public void SetOmrådeId(int value) {
        OmrådeId = value;
    }

    // Business logic methods
    public List<Sommerhus> HentLedigeSommerhuse(DateTime fraDato, DateTime tilDato)
    {
        return Sommerhuse
            .Where(s => !s.Reservationer.Any(r => 
                !(r.SlutDato < fraDato || r.StartDato > tilDato)))
            .ToList();
    }

    /*public List<Sommerhus> HentSommerhuseEfterStandard(string standard) {
        return Sommerhuse
            .Where(s => s.Fstandard.ToLower().Trim() == standard.ToLower().Trim())
            .ToList();
    }

    public Dictionary<string, int> HentStatistik() {
        return new Dictionary<string, int>
        {
            {"Antal sommerhuse", Sommerhuse.Count},
            {"Antal luksus", Sommerhuse.Count(s => s.Fstandard.ToLower().Trim() == "luksus")},
            {"Antal normal", Sommerhuse.Count(s => s.Fstandard.ToLower().Trim() == "normal")},
            {"Antal basic", Sommerhuse.Count(s => s.Fstandard.ToLower().Trim() == "basic")},
            {"Antal aktive reservationer", Sommerhuse.Sum(s => s.HentAktiveReservationer().Count)},
            {"Antal kommende reservationer", Sommerhuse.Sum(s => s.HentKommendeReservationer().Count)}
        };
    }*/

    public decimal GennemsnitligUgePris()
    {
        if (!Sommerhuse.Any())
            return 0;

        return Sommerhuse.Average(s => s.BasePris);
    }

    /*public override string ToString()
    {
        var stats = HentStatistik();
        return $"Område: {Navn}\n" +
               $"Konsulent: {Konsulent?.Navn ?? "Ingen"}\n" +
               $"Antal sommerhuse: {stats["Antal sommerhuse"]}\n" +
               $"- Luksus: {stats["Antal luksus"]}\n" +
               $"- Normal: {stats["Antal normal"]}\n" +
               $"- Basic: {stats["Antal basic"]}\n" +
               $"Aktive reservationer: {stats["Antal aktive reservationer"]}\n" +
               $"Kommende reservationer: {stats["Antal kommende reservationer"]}\n" +
               $"Gennemsnitlig ugepris: {GennemsnitligUgePris():C}";
    }*/

    public static class DatabaseHelper {

            /// <summary>
            /// Create a new Område.
            /// </summary>
            /// <param name="o">The Område object to create.</param>
            public static void CreateOmråde(Område o) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    INSERT INTO Område ([Navn], KonsulentId)
                    VALUES (@Navn, @KonsulentId);
                    SELECT SCOPE_IDENTITY();
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@Navn", o.Navn);
                        cmd.Parameters.AddWithValue("@KonsulentId", o.KonsulentId);

                        object newIdObj = cmd.ExecuteScalar();
                        int newId = Convert.ToInt32(newIdObj);
                        o.SetOmrådeId(newId);
                    }
                }
            }

            /// <summary>
            /// Read all Områder.
            /// </summary>
            /// <returns>A list of Område objects.</returns>
            public static List<Område> ReadAllOmråder() {
                List<Område> områder = new List<Område>();
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    SELECT OmrådeId, [Navn], KonsulentId
                    FROM Område
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Område o = new Område();
                                o.SetOmrådeId(reader.GetInt32(0));
                                o.Navn = reader.GetString(1);
                                o.KonsulentId = reader.GetInt32(2);

                                // Hent evt. sommerhuse med en separat SELECT, fx:
                                // o.Sommerhuse = LoadSommerhuseForOmråde(o.GetOmrådeId());

                                områder.Add(o);
                            }
                        }
                    }
                }
                return områder;
            }

            /// <summary>
            /// Read an Område by its Id.
            /// </summary>
            /// <param name="områdeId">The Id of the Område to read.</param>
            /// <returns>The Område object.</returns>
            public static Område ReadOmrådeById(int områdeId) {
                Område o = null;
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    SELECT OmrådeId, [Navn], KonsulentId
                    FROM Område
                    WHERE OmrådeId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", områdeId);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                o = new Område();
                                o.SetOmrådeId(reader.GetInt32(0));
                                o.Navn = reader.GetString(1);
                                o.KonsulentId = reader.GetInt32(2);

                                // Hent evt. sommerhuse
                                // o.Sommerhuse = LoadSommerhuseForOmråde(o.GetOmrådeId());
                            }
                        }
                    }
                }
                return o;
            }

            /// <summary>
            /// Update an existing Område.
            /// </summary>
            /// <param name="o">The Område object to update.</param>
            public static void UpdateOmråde(Område o) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    UPDATE Område
                    SET [Navn] = @Navn,
                        KonsulentId = @KonsulentId
                    WHERE OmrådeId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@Navn", o.Navn);
                        cmd.Parameters.AddWithValue("@KonsulentId", o.KonsulentId);
                        cmd.Parameters.AddWithValue("@ID", o.GetOmrådeId());

                        cmd.ExecuteNonQuery();
                    }
                }

                // Hvis du vil opdatere sommerhuse, sker det via Sommerhus CRUD (OmrådeId).
            }

            /// <summary>
            /// Delete an Område by its Id.
            /// </summary>
            /// <param name="områdeId">The Id of the Område to delete.</param>
            public static void DeleteOmråde(int områdeId) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();

                    // Hvis Området er i brug (Sommerhuse?), skal du enten slette dem først
                    // eller sætte deres OmrådeId til NULL, afhængigt af database constraints.

                    string query = @"
                    DELETE FROM Område
                    WHERE OmrådeId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", områdeId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
}