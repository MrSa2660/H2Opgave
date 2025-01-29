using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Opsynsmand {

    public int OpsynsmandId { get; set; }
    
    public string Rolle { get; set; }
    public string Navn { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    
    // Navigation property
    public virtual ICollection<Sommerhus> Sommerhuse { get; set; }

        public static void CreateOpsynsmand(Opsynsmand o) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();

                    // Bemærk: Da OpsynsmandID ikke er IDENTITY, skal du selv angive ID.
                    // Ellers får du en fejl, hvis kolonnen er NOT NULL (og PK).
                    string query = @"
                INSERT INTO Opsynsmand (OpsynsmandID, [Rolle], Navn, Phone, Email)
                VALUES (@ID, @Rolle, @Navn, @Phone, @Email);
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", o.OpsynsmandID);
                        cmd.Parameters.AddWithValue("@Rolle", o.Rolle ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Navn", o.Navn ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Phone", o.Phone ?? (object)DBNull.Value);
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
                SELECT OpsynsmandID, [Rolle], Navn, Phone, Email
                FROM Opsynsmand
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Opsynsmand o = new Opsynsmand();
                                o.OpsynsmandID = reader.GetInt32(0);
                                o.Rolle = reader.IsDBNull(1) ? null : reader.GetString(1).TrimEnd();
                                o.Navn = reader.IsDBNull(2) ? null : reader.GetString(2).TrimEnd();
                                o.Phone = reader.IsDBNull(3) ? null : reader.GetString(3).TrimEnd();
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
            public static Opsynsmand ReadOpsynsmandById(int opsynsmandID) {
                Opsynsmand o = null;
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                SELECT OpsynsmandID, [Rolle], Navn, Phone, Email
                FROM Opsynsmand
                WHERE OpsynsmandID = @ID
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", opsynsmandID);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                o = new Opsynsmand();
                                o.OpsynsmandID = reader.GetInt32(0);
                                o.Rolle = reader.IsDBNull(1) ? null : reader.GetString(1).TrimEnd();
                                o.Navn = reader.IsDBNull(2) ? null : reader.GetString(2).TrimEnd();
                                o.Phone = reader.IsDBNull(3) ? null : reader.GetString(3).TrimEnd();
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
                    Phone = @Phone,
                    Email = @Email
                WHERE OpsynsmandID = @ID
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@Rolle", o.Rolle ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Navn", o.Navn ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Phone", o.Phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", o.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ID", o.OpsynsmandID);

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
  

