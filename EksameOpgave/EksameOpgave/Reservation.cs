using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Globalization;

public class Reservation {
    private int ReservationId;
    public int SommerhusId { get; set; }
    public int KundeId { get; set; }
    public decimal Pris { get; set; }
    public DateTime StartDato { get; set; }
    public DateTime SlutDato { get; set; }

    public Sommerhus Sommerhus { get; set; }
    public Kunde Kunde { get; set; }

    public int GetReservationId() {
        return ReservationId;
    }

    public void SetReservationId(int value) {
        ReservationId = value;
    }

    // Business logic methods
    public bool ErGyldigPeriode() {
        // Business logic methodsstarter og slutter på en lørdag
        if (StartDato.DayOfWeek != DayOfWeek.Saturday || SlutDato.DayOfWeek != DayOfWeek.Saturday)
            return false;

        // Check om perioden er mindst en uge
        if ((SlutDato - StartDato).TotalDays < 7)
            return false;

        // Check om perioden er et helt antal uger
        return (SlutDato - StartDato).TotalDays % 7 == 0;
    }

    public int AntalUger() {
        return (int)((SlutDato - StartDato).TotalDays / 7);
    }

    public decimal BeregnTotalPris() {
        if (Sommerhus == null) {
            throw new InvalidOperationException("Kan ikke beregne pris: Sommerhus er ikke tilgængeligt");
        }

        decimal totalPris = 0;
        var sæsonkategorier = SæsonKategori.DatabaseHelper.ReadAllSæsonKategorier();

        // For hver uge i reservationen
        DateTime currentUgeStart = StartDato;
        while (currentUgeStart < SlutDato) {

            // Find ugenummer for den aktuelle uge
            int ugeNummer = ISOWeek.GetWeekOfYear(currentUgeStart);
            SæsonKategori sæsonForUge = new SæsonKategori();
            Console.WriteLine(ugeNummer);
            Console.ReadKey();
            // Find sæsonkategori for denne uge
            foreach (var sæson in sæsonkategorier) {
                foreach (var uge in sæson.UgeNumre) {
                    if (uge == ugeNummer) {
                        sæsonForUge = sæson;
                    }
                }
            }

            Console.WriteLine(sæsonForUge.Navn);
            Console.ReadKey();
            // Beregn pris for denne uge baseret på basispris og evt. sæsonmultiplikator
            decimal ugePris = Sommerhus.BasePris;
            if (sæsonForUge != null) {
                ugePris = Sommerhus.BasePris * (sæsonForUge.Pris / 100.0m);
            }

            totalPris += ugePris;
            currentUgeStart = currentUgeStart.AddDays(7);
        }

        return totalPris;
    }

    public string HentUgeNumre() {
        var uger = new List<int>();
        var dato = StartDato;
        while (dato <= SlutDato) {
            uger.Add(ISOWeek.GetWeekOfYear(dato));
            dato = dato.AddDays(7);
        }
        return string.Join(", ", uger);
    }

    public override string ToString() {
        return $"Reservation {ReservationId}\n" +
               $"Sommerhus: {SommerhusId}\n" +
               $"Kunde: {KundeId}\n" +
               $"Periode: {StartDato:dd/MM/yyyy} - {SlutDato:dd/MM/yyyy}\n" +
               $"Total pris: {BeregnTotalPris():C}";
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
                    INSERT INTO Reservation (SommerhusID, KundeID, Pris, StartDato, SlutDato)
                    VALUES (@SommerhusID, @KundeID, @Pris, @StartDato, @SlutDato);
                    SELECT SCOPE_IDENTITY();
                ";
                r.Sommerhus = Sommerhus.DatabaseHelper.ReadSommerhusById(r.SommerhusId);
                r.Pris = r.BeregnTotalPris();
                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@SommerhusID", r.SommerhusId);
                    cmd.Parameters.AddWithValue("@KundeID", r.KundeId);
                    cmd.Parameters.AddWithValue("@Pris", r.Pris); // Convert decimal to string
                    cmd.Parameters.AddWithValue("@StartDato", r.StartDato);
                    cmd.Parameters.AddWithValue("@SlutDato", r.SlutDato);

                    object newIdObj = cmd.ExecuteScalar();
                    int newId = Convert.ToInt32(newIdObj);
                    r.SetReservationId(newId);
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
                    SELECT ReservationId, SommerhusID, KundeID, Pris, StartDato, SlutDato
                    FROM Reservation
                ";
                
                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Reservation r = new Reservation();
                            r.ReservationId = reader.GetInt32(0);
                           
                            if (!reader.IsDBNull(1))
                                r.SommerhusId = reader.GetInt32(1);

                            if (!reader.IsDBNull(2))
                                r.KundeId = reader.GetInt32(2);

                            if (!reader.IsDBNull(3))
                                r.Pris = reader.GetDecimal(3);

                            if (!reader.IsDBNull(4))
                                r.StartDato = reader.GetDateTime(4);

                            if (!reader.IsDBNull(5))
                                r.SlutDato = reader.GetDateTime(5);

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
                SELECT ReservationId, SommerhusId, KundeId, Pris, StartDato, SlutDato
                FROM Reservation
                WHERE ReservationId = @ID
            ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ID", reservationId);

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            r = new Reservation();
                            r.SetReservationId(reader.GetInt32(0));
                            r.SommerhusId = reader.GetInt32(1);
                            r.KundeId = reader.GetInt32(2);
                            r.Pris = reader.GetDecimal(3);
                            r.StartDato = reader.GetDateTime(4);
                            r.SlutDato = reader.GetDateTime(5);
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
                    Pris = @Pris,
                    StartDato = @StartDato,
                    SlutDato = @SlutDato
                WHERE ReservationId = @ID
            ";

                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@SommerhusId", r.SommerhusId);
                    cmd.Parameters.AddWithValue("@KundeId", r.KundeId);
                    cmd.Parameters.AddWithValue("@Pris", r.Pris);
                    cmd.Parameters.AddWithValue("@StartDato", r.StartDato);
                    cmd.Parameters.AddWithValue("@SlutDato", r.SlutDato);
                    cmd.Parameters.AddWithValue("@ID", r.GetReservationId());

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
