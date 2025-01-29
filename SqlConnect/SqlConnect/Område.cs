using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {
    public class Område {
        private int OmrådeId;
        public string Name { get; set; }
        public int KonsulentId { get; set; }
        public List<Sommerhus> Sommerhuse { get; set; } = new List<Sommerhus>();

        public Område() {
            Sommerhuse = new List<Sommerhus>();
        }

        /// <summary>
        /// Get the OmrådeId.
        /// </summary>
        /// <returns>The OmrådeId.</returns>
        public int GetOmrådeId() {
            return OmrådeId;
        }

        /// <summary>
        /// Set the OmrådeId.
        /// </summary>
        /// <param name="value">The OmrådeId value.</param>
        public void SetOmrådeId(int value) {
            OmrådeId = value;
        }

        /// <summary>
        /// Helper class for database operations related to Område.
        /// </summary>
        public static class DatabaseHelper {

            /// <summary>
            /// Create a new Område.
            /// </summary>
            /// <param name="o">The Område object to create.</param>
            public static void CreateOmråde(Område o) {
                using (SqlConnection con = new SqlConnection(program.connectionString)) {
                    con.Open();
                    string query = @"
                    INSERT INTO Område ([Name], KonsulentId)
                    VALUES (@Name, @KonsulentId);
                    SELECT SCOPE_IDENTITY();
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@Name", o.Name);
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
                    SELECT OmrådeId, [Name], KonsulentId
                    FROM Område
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            while (reader.Read()) {
                                Område o = new Område();
                                o.SetOmrådeId(reader.GetInt32(0));
                                o.Name = reader.GetString(1);
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
                    SELECT OmrådeId, [Name], KonsulentId
                    FROM Område
                    WHERE OmrådeId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@ID", områdeId);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                o = new Område();
                                o.SetOmrådeId(reader.GetInt32(0));
                                o.Name = reader.GetString(1);
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
                    SET [Name] = @Name,
                        KonsulentId = @KonsulentId
                    WHERE OmrådeId = @ID
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con)) {
                        cmd.Parameters.AddWithValue("@Name", o.Name);
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
}
