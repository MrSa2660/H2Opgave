using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Konsulent {
    private int KonsulentId;
    public string Navn { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public string Adresse { get; set; }
    public string Type { get; set; }
    public List<Område> AnsvarligForOmråder { get; set; } = new List<Område>();

    public Konsulent() {
        AnsvarligForOmråder = new List<Område>();
    }

    public int GetKonsulentId() {
        return KonsulentId;
    }

    public void SetKonsulentId(int value) {
        KonsulentId = value;
    }

    public static class DatabaseHelper {

            // -------------------- CREATE Konsulent --------------------
            /// <summary>
            /// Creates a new Konsulent in the database.
            /// </summary>
            /// <param name="k">The Konsulent object to create.</param>
            public static void CreateKonsulent(Konsulent k) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    INSERT INTO Konsulent (Navn, Email, Telefon, [Type])
                    VALUES (@Navn, @Email, @Telefon, @Type);
                    SELECT SCOPE_IDENTITY();
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@Navn", k.Navn);
                        cmd.Parameters.AddWithValue("@Email", k.Email);
                        cmd.Parameters.AddWithValue("@Telefon", k.Telefon);
                        cmd.Parameters.AddWithValue("@Type", k.Type);

                        object newIdObj = cmd.ExecuteScalar();
                        int newId = Convert.ToInt32(newIdObj);
                        k.SetKonsulentId(newId);
                    }
                }
            }

            // -------------------- READ ALL Konsulenter --------------------
            /// <summary>
            /// Retrieves all Konsulenter from the database.
            /// </summary>
            /// <returns>A list of Konsulent objects.</returns>
            public static List<Konsulent> ReadAllKonsulenter() {
                List<Konsulent> liste = new List<Konsulent>();

                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    SELECT KonsulentId, Navn, Email, Telefon, [Type]
                    FROM Konsulent
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Konsulent k = new Konsulent();
                                k.SetKonsulentId(reader.GetInt32(0));
                                k.Navn = reader.GetString(1);
                                k.Email = reader.GetString(2);
                                k.Telefon = reader.GetString(3);
                                k.Type = reader.GetString(4);

                                // Hent evt. Områder (ReadAllOmråder) => filtrer på KonsulentId = k.GetKonsulentId()
                                // k.AnsvarligForOmråder = LoadOmråderForKonsulent(k.GetKonsulentId());

                                liste.Add(k);
                            }
                        }
                    }
                }

                return liste;
            }

            // -------------------- READ ONE Konsulent --------------------
            /// <summary>
            /// Retrieves a single Konsulent from the database by ID.
            /// </summary>
            /// <param name="konsulentId">The ID of the Konsulent to retrieve.</param>
            /// <returns>The Konsulent object.</returns>
            public static Konsulent ReadKonsulentById(int konsulentId) {
                Konsulent k = null;

                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    SELECT KonsulentId, Navn, Email, Telefon, [Type]
                    FROM Konsulent
                    WHERE KonsulentId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", konsulentId);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                k = new Konsulent();
                                k.SetKonsulentId(reader.GetInt32(0));
                                k.Navn = reader.GetString(1);
                                k.Email = reader.GetString(2);
                                k.Telefon = reader.GetString(3);
                                k.Type = reader.GetString(4);

                                // k.AnsvarligForOmråder = LoadOmråderForKonsulent(k.GetKonsulentId());
                            }
                        }
                    }
                }

                return k;
            }

            // -------------------- UPDATE Konsulent --------------------
            /// <summary>
            /// Updates an existing Konsulent in the database.
            /// </summary>
            /// <param name="k">The Konsulent object to update.</param>
            public static void UpdateKonsulent(Konsulent k) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    UPDATE Konsulent
                    SET Navn = @Navn,
                        Email = @Email,
                        Telefon = @Telefon,
                        [Type] = @Type
                    WHERE KonsulentId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@Navn", k.Navn);
                        cmd.Parameters.AddWithValue("@Email", k.Email);
                        cmd.Parameters.AddWithValue("@Telefon", k.Telefon);
                        cmd.Parameters.AddWithValue("@Type", k.Type);
                        cmd.Parameters.AddWithValue("@ID", k.GetKonsulentId());

                        cmd.ExecuteNonQuery();
                    }
                }
                // Opdatering af ansvarlige områder sker i Område-tabellen (KonsulentId).
            }

            // -------------------- DELETE Konsulent --------------------
            /// <summary>
            /// Deletes a Konsulent from the database by ID.
            /// </summary>
            /// <param name="konsulentId">The ID of the Konsulent to delete.</param>
            public static void DeleteKonsulent(int konsulentId) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();

                    // Hvis konsulenten er knyttet til Områder, skal de håndteres
                    // fx Opdatere Område.KonsulentId = NULL, eller slette områderne?

                    string query = @"
                    DELETE FROM Konsulent
                    WHERE KonsulentId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", konsulentId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
} 