using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

public class Menu
{
    
    private const int ITEMS_PER_PAGE = 15;

    #region Hovedmenuer
    public async Task VisHovedMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Sydvest-Bo Administrationssystem ===");
            Console.WriteLine("1. Administrer Sommerhuse");
            Console.WriteLine("2. Administrer Ejere");
            Console.WriteLine("3. Administrer Reservationer");
            Console.WriteLine("4. Administrer Områder");
            Console.WriteLine("5. Administrer Sæsonkategorier");
            Console.WriteLine("6. Administrer Kunder");
            Console.WriteLine("7. Administrer Opsynsmænd");
            Console.WriteLine("8. Administrer Konsulenter");
            Console.WriteLine("0. Afslut");
            
            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();
            
            switch (valg)
            {
                case "1":
                    await VisSommerhusMenu();
                    break;
                case "2":
                    await VisEjerMenu();
                    break;
                case "3":
                    await VisReservationMenu();
                    break;
                case "4":
                    await VisOmrådeMenu();
                    break;
                case "5":
                    await VisSæsonMenu();
                    break;
                case "6":
                    await VisKundeMenu();
                    break;
                case "7":
                    await VisOpsynsmandMenu();
                    break;
                case "8":
                    await VisKonsulentMenu();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public async Task VisSommerhusMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Sommerhus Menu ===");
            Console.WriteLine("1. Opret nyt sommerhus");
            Console.WriteLine("2. Vis alle sommerhuse");
            Console.WriteLine("3. Søg efter sommerhus");
            Console.WriteLine("4. Rediger sommerhus");
            Console.WriteLine("5. Slet sommerhus");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretSommerhus();
                    break;
                case "2":
                    VisAlleSommerhuse();
                    break;
                case "3":
                    await SøgSommerhus();
                    break;
                case "4":
                    await RedigerSommerhus();
                    break;
                case "5":
                    await SletSommerhus();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Prøv igen.");
                    break;
            }

            if (fortsæt)
            {
                Console.WriteLine("\nTryk på en tast for at fortsætte...");
                Console.ReadKey();
            }
        }
    }

    private async Task VisReservationMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Reservation Administration ===");
            Console.WriteLine("1. Opret ny reservation");
            Console.WriteLine("2. Vis alle reservationer");
            Console.WriteLine("3. Søg efter reservation");
            Console.WriteLine("4. Rediger reservation");
            Console.WriteLine("5. Slet reservation");
            Console.WriteLine("6. Vis aktive reservationer");
            Console.WriteLine("7. Vis kommende reservationer");
            Console.WriteLine("8. Søg reservationer for sommerhus");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretNyReservation();
                    break;
                case "2":
                    await VisAlleReservationer();
                    break;
                case "3":
                    await SøgReservation();
                    break;
                case "4":
                    await RedigerReservation();
                    break;
                case "5":
                    await SletReservation();
                    break;
                case "6":
                    await VisAktiveReservationer();
                    break;
                case "7":
                    await VisKommendeReservationer();
                    break;
                case "8":
                    await SøgReservationerForSommerhus();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task VisOmrådeMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Område Administration ===");
            Console.WriteLine("1. Opret nyt område");
            Console.WriteLine("2. Vis alle områder");
            Console.WriteLine("3. Søg efter område");
            Console.WriteLine("4. Rediger område");
            Console.WriteLine("5. Slet område");
            Console.WriteLine("6. Vis områdestatistik");
            Console.WriteLine("7. Vis sommerhuse i område");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretNytOmråde();
                    break;
                case "2":
                    await VisAlleOmråder();
                    break;
                case "3":
                    await SøgOmråde();
                    break;
                case "4":
                    await RedigerOmråde();
                    break;
                case "5":
                    await SletOmråde();
                    break;
                case "6":
                    await VisOmrådeStatistik();
                    break;
                case "7":
                    await VisSommerhuseIOmråde();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task VisSæsonMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Sæsonkategori Administration ===");
            Console.WriteLine("1. Opret ny sæsonkategori");
            Console.WriteLine("2. Vis alle sæsonkategorier");
            Console.WriteLine("3. Søg efter sæsonkategori");
            Console.WriteLine("4. Rediger sæsonkategori");
            Console.WriteLine("5. Slet sæsonkategori");
            Console.WriteLine("6. Vis priser for sæsonkategori");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretNySæsonkategori();
                    break;
                case "2":
                    await VisAlleSæsonkategorier();
                    break;
                case "3":
                    await SøgSæsonkategori();
                    break;
                case "4":
                    await RedigerSæsonkategori();
                    break;
                case "5":
                    await SletSæsonkategori();
                    break;
                case "6":
                    await VisSæsonkategoriPriser();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task VisEjerMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Ejer Administration ===");
            Console.WriteLine("1. Opret ny ejer");
            Console.WriteLine("2. Vis alle ejere");
            Console.WriteLine("3. Søg efter ejer");
            Console.WriteLine("4. Rediger ejer");
            Console.WriteLine("5. Slet ejer");
            Console.WriteLine("6. Vis ejers sommerhuse");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretNyEjer();
                    break;
                case "2":
                    await VisAlleEjere();
                    break;
                case "3":
                    await SøgEjer();
                    break;
                case "4":
                    await RedigerEjer();
                    break;
                case "5":
                    await SletEjer();
                    break;
                case "6":
                    await VisEjersSommerhuse();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }
    #endregion

    #region Sommerhus-metoder
    private async Task OpretSommerhus()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Nyt Sommerhus ===");

        Sommerhus nytSommerhus = new Sommerhus();

        Console.Write("Indtast adresse: ");
        nytSommerhus.Adresse = Console.ReadLine();

        Console.Write("Indtast basepris: ");
        if (int.TryParse(Console.ReadLine(), out int basepris))
        {
            nytSommerhus.BasePris = basepris;
        }
        else
        {
            Console.WriteLine("Ugyldig basepris. Sommerhus ikke oprettet.");
            return;
        }

        Console.Write("Indtast ejer ID: ");
        if (int.TryParse(Console.ReadLine(), out int ejerId))
        {
            nytSommerhus.EjerId = ejerId;
        }
        else
        {
            Console.WriteLine("Ugyldigt ejer ID. Sommerhus ikke oprettet.");
            return;
        }

        Console.Write("Indtast område ID: ");
        if (int.TryParse(Console.ReadLine(), out int områdeId))
        {
            nytSommerhus.OmrådeId = områdeId;
        }
        else
        {
            Console.WriteLine("Ugyldigt område ID. Sommerhus ikke oprettet.");
            return;
        }

        Console.Write("Indtast opsynsmand ID: ");
        if (int.TryParse(Console.ReadLine(), out int opsynsmandId))
        {
            nytSommerhus.OpsynsmandId = opsynsmandId;
        }
        else
        {
            Console.WriteLine("Ugyldigt opsynsmand ID. Sommerhus ikke oprettet.");
            return;
        }

        Console.Write("Indtast klassificering (luksus/normal/basic): ");
        nytSommerhus.Klassificering = Console.ReadLine().ToLower();
        if (!new[] { "luksus", "normal", "basic" }.Contains(nytSommerhus.Klassificering))
        {
            Console.WriteLine("Ugyldig klassificering. Sommerhus ikke oprettet.");
            return;
        }

        Console.Write("Indtast antal senge: ");
        if (int.TryParse(Console.ReadLine(), out int antalSenge))
        {
            nytSommerhus.AntalSenge = antalSenge;
        }
        else
        {
            Console.WriteLine("Ugyldigt antal senge. Sommerhus ikke oprettet.");
            return;
        }

        try
        {
            Sommerhus.DatabaseHelper.CreateSommerhus(nytSommerhus);
            Console.WriteLine("Sommerhus oprettet succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved oprettelse af sommerhus: {ex.Message}");
        }
    }

    private void VisAlleSommerhuse()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Sommerhuse ===\n");

        try
        {
            var sommerhuse = Sommerhus.DatabaseHelper.ReadAllSommerhuse();
            VisPaginering(sommerhuse, VisSommerhusDetaljer, "sommerhuse");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af sommerhuse: {ex.Message}");
        }
    }

    private async Task SøgSommerhus()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Efter Sommerhus ===\n");
        Console.Write("Indtast sommerhus ID: ");

        if (int.TryParse(Console.ReadLine(), out int sommerhusId))
        {
            try
            {
                var sommerhus = Sommerhus.DatabaseHelper.ReadSommerhusById(sommerhusId);
                if (sommerhus != null)
                {
                    VisSommerhusDetaljer(sommerhus);
                }
                else
                {
                    Console.WriteLine("Sommerhus ikke fundet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter sommerhus: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt sommerhus ID.");
        }
    }

    private async Task RedigerSommerhus()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Sommerhus ===\n");
        Console.Write("Indtast sommerhus ID der skal redigeres: ");

        if (!int.TryParse(Console.ReadLine(), out int sommerhusId))
        {
            Console.WriteLine("Ugyldigt sommerhus ID.");
            return;
        }

        try
        {
            var sommerhus = Sommerhus.DatabaseHelper.ReadSommerhusById(sommerhusId);
            if (sommerhus == null)
            {
                Console.WriteLine("Sommerhus ikke fundet.");
                return;
            }

            Console.WriteLine("\nNuværende værdier:");
            VisSommerhusDetaljer(sommerhus);

            Console.WriteLine("\nIndtast nye værdier (tryk Enter for at beholde nuværende værdi):");

            Console.Write("Ny adresse: ");
            string nyAdresse = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyAdresse))
                sommerhus.Adresse = nyAdresse;

            Console.Write("Ny basepris: ");
            string nyBasepris = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyBasepris) && int.TryParse(nyBasepris, out int basepris))
                sommerhus.BasePris = basepris;

            Console.Write("Nyt ejer ID: ");
            string nytEjerId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytEjerId) && int.TryParse(nytEjerId, out int ejerId))
                sommerhus.EjerId = ejerId;

            Console.Write("Nyt område ID: ");
            string nytOmrådeId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytOmrådeId) && int.TryParse(nytOmrådeId, out int områdeId))
                sommerhus.OmrådeId = områdeId;

            Console.Write("Nyt opsynsmand ID: ");
            string nytOpsynsmandId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytOpsynsmandId) && int.TryParse(nytOpsynsmandId, out int opsynsmandId))
                sommerhus.OpsynsmandId = opsynsmandId;

            Console.Write("Ny klassificering (luksus/normal/basic): ");
            string nyKlassificering = Console.ReadLine().ToLower();
            if (!string.IsNullOrWhiteSpace(nyKlassificering))
            {
                if (new[] { "luksus", "normal", "basic" }.Contains(nyKlassificering))
                    sommerhus.Klassificering = nyKlassificering;
                else
                    Console.WriteLine("Ugyldig klassificering. Beholder nuværende værdi.");
            }

            Console.Write("Nyt antal senge: ");
            string nytAntalSenge = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytAntalSenge) && int.TryParse(nytAntalSenge, out int antalSenge))
                sommerhus.AntalSenge = antalSenge;

            Sommerhus.DatabaseHelper.UpdateSommerhus(sommerhus);
            Console.WriteLine("Sommerhus opdateret succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af sommerhus: {ex.Message}");
        }
    }

    private async Task SletSommerhus()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Sommerhus ===\n");
        Console.Write("Indtast sommerhus ID der skal slettes: ");

        if (int.TryParse(Console.ReadLine(), out int sommerhusId))
        {
            try
            {
                var sommerhus = Sommerhus.DatabaseHelper.ReadSommerhusById(sommerhusId);
                if (sommerhus == null)
                {
                    Console.WriteLine("Sommerhus ikke fundet.");
                    return;
                }

                Console.WriteLine("\nFølgende sommerhus vil blive slettet:");
                VisSommerhusDetaljer(sommerhus);

                Console.Write("\nEr du sikker på at du vil slette dette sommerhus? (ja/nej): ");
                if (Console.ReadLine().ToLower() == "ja")
                {
                    Sommerhus.DatabaseHelper.DeleteSommerhus(sommerhusId);
                    Console.WriteLine("Sommerhus slettet succesfuldt!");
                }
                else
                {
                    Console.WriteLine("Sletning annulleret.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved sletning af sommerhus: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt sommerhus ID.");
        }
    }

    private void VisSommerhusDetaljer(Sommerhus sommerhus)
    {
        Console.WriteLine($"ID: {sommerhus.SommerhusId}");
        Console.WriteLine($"Adresse: {sommerhus.Adresse}");
        Console.WriteLine($"Basepris: {sommerhus.BasePris} kr");
        Console.WriteLine($"Ejer ID: {sommerhus.EjerId}");
        Console.WriteLine($"Område ID: {sommerhus.OmrådeId}");
        Console.WriteLine($"Opsynsmand ID: {sommerhus.OpsynsmandId}");
        Console.WriteLine($"Klassificering: {sommerhus.Klassificering}");
        Console.WriteLine($"Antal senge: {sommerhus.AntalSenge}");
    }
    #endregion

    #region Reservations-metoder
    private bool ErGyldigReservationsPeriode(DateTime startDato, DateTime slutDato)
    {
        // Tjek om både start- og slutdato er lørdage
        if (startDato.DayOfWeek != DayOfWeek.Saturday || slutDato.DayOfWeek != DayOfWeek.Saturday)
        {
            Console.WriteLine("Reservationer skal starte og slutte på en lørdag.");
            return false;
        }

        // Tjek om perioden er et helt antal uger
        TimeSpan periode = slutDato - startDato;
        if (periode.Days % 7 != 0)
        {
            Console.WriteLine("Reservationsperioden skal være et helt antal uger.");
            return false;
        }

        // Tjek om startdato er i fremtiden
        if (startDato < DateTime.Today)
        {
            Console.WriteLine("Startdato kan ikke være i fortiden.");
            return false;
        }

        // Tjek om slutdato er efter startdato
        if (slutDato <= startDato)
        {
            Console.WriteLine("Slutdato skal være efter startdato.");
            return false;
        }

        return true;
    }

    private async Task OpretNyReservation()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Ny Reservation ===\n");

        Reservation nyReservation = new Reservation();

        Console.Write("Indtast sommerhus ID: ");
        if (!int.TryParse(Console.ReadLine(), out int sommerhusId))
        {
            Console.WriteLine("Ugyldigt sommerhus ID.");
            return;
        }
        nyReservation.SommerhusId = sommerhusId;

        Console.Write("Indtast kunde ID: ");
        if (!int.TryParse(Console.ReadLine(), out int kundeId))
        {
            Console.WriteLine("Ugyldigt kunde ID.");
            return;
        }
        nyReservation.KundeId = kundeId;

        Console.WriteLine("\nBemærk: Reservationer skal starte og slutte på en lørdag.");
        Console.Write("Indtast startdato (dd/MM/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDato))
        {
            Console.WriteLine("Ugyldig startdato.");
            return;
        }

        Console.Write("Indtast slutdato (dd/MM/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime slutDato))
        {
            Console.WriteLine("Ugyldig slutdato.");
            return;
        }

        if (!ErGyldigReservationsPeriode(startDato, slutDato))
        {
            return;
        }

        nyReservation.StartDato = startDato;
        nyReservation.SlutDato = slutDato;

        try
        {
            // Tjek for overlappende reservationer
            var alleReservationer = Reservation.DatabaseHelper.ReadAllReservationer()
                .Where(r => r.SommerhusId == sommerhusId)
                .ToList();

            bool harOverlap = alleReservationer.Any(r =>
                (startDato >= r.StartDato && startDato < r.SlutDato) ||  // Ny start er under eksisterende
                (slutDato > r.StartDato && slutDato <= r.SlutDato) ||    // Ny slut er under eksisterende
                (startDato <= r.StartDato && slutDato >= r.SlutDato));   // Ny omslutter eksisterende

            if (harOverlap)
            {
                Console.WriteLine("Der findes allerede en reservation i den valgte periode.");
                return;
            }

            Reservation.DatabaseHelper.CreateReservation(nyReservation);
            Console.WriteLine($"\nReservation oprettet med ID: {nyReservation.GetReservationId()}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved oprettelse af reservation: {ex.Message}");
        }
    }

    private async Task VisAlleReservationer()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Reservationer ===\n");

        try
        {
            var reservationer = Reservation.DatabaseHelper.ReadAllReservationer();
            VisPaginering(reservationer, VisReservationDetaljer, "reservationer");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af reservationer: {ex.Message}");
        }
    }

    private async Task SøgReservation()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Efter Reservation ===\n");
        Console.Write("Indtast reservations ID: ");

        if (int.TryParse(Console.ReadLine(), out int reservationId))
        {
            try
            {
                var reservation = Reservation.DatabaseHelper.ReadReservationById(reservationId);
                if (reservation != null)
                {
                    VisReservationDetaljer(reservation);
                }
                else
                {
                    Console.WriteLine("Reservation ikke fundet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter reservation: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt reservations ID.");
        }
    }

    private async Task RedigerReservation()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Reservation ===\n");
        Console.Write("Indtast reservations ID der skal redigeres: ");

        if (!int.TryParse(Console.ReadLine(), out int reservationId))
        {
            Console.WriteLine("Ugyldigt reservations ID.");
            return;
        }

        try
        {
            var reservation = Reservation.DatabaseHelper.ReadReservationById(reservationId);
            if (reservation == null)
            {
                Console.WriteLine("Reservation ikke fundet.");
                return;
            }

            Console.WriteLine("\nNuværende værdier:");
            VisReservationDetaljer(reservation);

            Console.WriteLine("\nIndtast nye værdier (tryk Enter for at beholde nuværende værdi):");

            Console.Write($"Nyt sommerhus ID ({reservation.SommerhusId}): ");
            string nytSommerhusId = Console.ReadLine();
            int sommerhusId = reservation.SommerhusId;
            if (!string.IsNullOrWhiteSpace(nytSommerhusId) && int.TryParse(nytSommerhusId, out int nytId))
                sommerhusId = nytId;

            Console.Write($"Nyt kunde ID ({reservation.KundeId}): ");
            string nytKundeId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytKundeId) && int.TryParse(nytKundeId, out int kundeId))
                reservation.KundeId = kundeId;

            DateTime startDato = reservation.StartDato;
            DateTime slutDato = reservation.SlutDato;

            Console.WriteLine("\nBemærk: Reservationer skal starte og slutte på en lørdag.");
            Console.Write($"Ny startdato ({reservation.StartDato:dd/MM/yyyy}): ");
            string nyStartDato = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyStartDato) && DateTime.TryParse(nyStartDato, out DateTime nyStart))
                startDato = nyStart;

            Console.Write($"Ny slutdato ({reservation.SlutDato:dd/MM/yyyy}): ");
            string nySlutDato = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nySlutDato) && DateTime.TryParse(nySlutDato, out DateTime nySlut))
                slutDato = nySlut;

            if (!ErGyldigReservationsPeriode(startDato, slutDato))
            {
                return;
            }

            // Tjek for overlappende reservationer (undtagen den nuværende reservation)
            var alleReservationer = Reservation.DatabaseHelper.ReadAllReservationer()
                .Where(r => r.SommerhusId == sommerhusId && r.GetReservationId() != reservationId)
                .ToList();

            bool harOverlap = alleReservationer.Any(r =>
                (startDato >= r.StartDato && startDato < r.SlutDato) ||  // Ny start er under eksisterende
                (slutDato > r.StartDato && slutDato <= r.SlutDato) ||    // Ny slut er under eksisterende
                (startDato <= r.StartDato && slutDato >= r.SlutDato));   // Ny omslutter eksisterende

            if (harOverlap)
            {
                Console.WriteLine("Der findes allerede en reservation i den valgte periode.");
                return;
            }

            reservation.SommerhusId = sommerhusId;
            reservation.StartDato = startDato;
            reservation.SlutDato = slutDato;

            Reservation.DatabaseHelper.UpdateReservation(reservation);
            Console.WriteLine("Reservation opdateret succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af reservation: {ex.Message}");
        }
    }

    private async Task SletReservation()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Reservation ===\n");
        Console.Write("Indtast reservations ID der skal slettes: ");

        if (int.TryParse(Console.ReadLine(), out int reservationId))
        {
            try
            {
                var reservation = Reservation.DatabaseHelper.ReadReservationById(reservationId);
                if (reservation == null)
                {
                    Console.WriteLine("Reservation ikke fundet.");
                    return;
                }

                Console.WriteLine("\nFølgende reservation vil blive slettet:");
                VisReservationDetaljer(reservation);

                Console.Write("\nEr du sikker på at du vil slette denne reservation? (ja/nej): ");
                if (Console.ReadLine().ToLower() == "ja")
                {
                    Reservation.DatabaseHelper.DeleteReservation(reservationId);
                    Console.WriteLine("Reservation slettet succesfuldt!");
                }
                else
                {
                    Console.WriteLine("Sletning annulleret.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved sletning af reservation: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt reservations ID.");
        }
    }

    private async Task VisAktiveReservationer()
    {
        Console.Clear();
        Console.WriteLine("=== Aktive Reservationer ===\n");

        try
        {
            var alleReservationer = Reservation.DatabaseHelper.ReadAllReservationer();
            var aktiveReservationer = alleReservationer.Where(r => 
                r.StartDato <= DateTime.Today && r.SlutDato >= DateTime.Today).ToList();

            if (aktiveReservationer.Count == 0)
            {
                Console.WriteLine("Ingen aktive reservationer fundet.");
                return;
            }

            foreach (var reservation in aktiveReservationer)
            {
                VisReservationDetaljer(reservation);
                Console.WriteLine(new string('-', 50));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af aktive reservationer: {ex.Message}");
        }
    }

    private async Task VisKommendeReservationer()
    {
        Console.Clear();
        Console.WriteLine("=== Kommende Reservationer ===\n");

        try
        {
            var alleReservationer = Reservation.DatabaseHelper.ReadAllReservationer();
            var kommendeReservationer = alleReservationer.Where(r => 
                r.StartDato > DateTime.Today).OrderBy(r => r.StartDato).ToList();

            if (kommendeReservationer.Count == 0)
            {
                Console.WriteLine("Ingen kommende reservationer fundet.");
                return;
            }

            foreach (var reservation in kommendeReservationer)
            {
                VisReservationDetaljer(reservation);
                Console.WriteLine(new string('-', 50));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af kommende reservationer: {ex.Message}");
        }
    }

    private async Task SøgReservationerForSommerhus()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Reservationer for Sommerhus ===\n");
        
        Console.Write("Indtast sommerhus ID: ");
        if (!int.TryParse(Console.ReadLine(), out int sommerhusId))
        {
            Console.WriteLine("Ugyldigt sommerhus ID.");
            return;
        }

        try
        {
            // Hent sommerhuset for at verificere at det eksisterer
            var sommerhus = Sommerhus.DatabaseHelper.ReadSommerhusById(sommerhusId);
            if (sommerhus == null)
            {
                Console.WriteLine("Sommerhus ikke fundet.");
                return;
            }

            Console.WriteLine($"\nSommerhus detaljer:");
            VisSommerhusDetaljer(sommerhus);
            Console.WriteLine(new string('-', 50));

            // Hent alle reservationer og filtrer efter sommerhus ID
            var alleReservationer = Reservation.DatabaseHelper.ReadAllReservationer();
            var sommerhusReservationer = alleReservationer
                .Where(r => r.SommerhusId == sommerhusId)
                .OrderBy(r => r.StartDato)
                .ToList();

            if (sommerhusReservationer.Count == 0)
            {
                Console.WriteLine("\nIngen reservationer fundet for dette sommerhus.");
                return;
            }

            Console.WriteLine($"\nReservationer for sommerhuset ({sommerhusReservationer.Count} i alt):");
            Console.WriteLine("\nHistoriske reservationer:");
            var historiske = sommerhusReservationer.Where(r => r.SlutDato < DateTime.Today).ToList();
            foreach (var reservation in historiske)
            {
                VisReservationDetaljer(reservation);
                Console.WriteLine(new string('-', 30));
            }

            Console.WriteLine("\nAktive reservationer:");
            var aktive = sommerhusReservationer
                .Where(r => r.StartDato <= DateTime.Today && r.SlutDato >= DateTime.Today)
                .ToList();
            foreach (var reservation in aktive)
            {
                VisReservationDetaljer(reservation);
                Console.WriteLine(new string('-', 30));
            }

            Console.WriteLine("\nKommende reservationer:");
            var kommende = sommerhusReservationer.Where(r => r.StartDato > DateTime.Today).ToList();
            foreach (var reservation in kommende)
            {
                VisReservationDetaljer(reservation);
                Console.WriteLine(new string('-', 30));
            }

            // Vis statistik
            Console.WriteLine("\nStatistik:");
            Console.WriteLine($"Antal historiske reservationer: {historiske.Count}");
            Console.WriteLine($"Antal aktive reservationer: {aktive.Count}");
            Console.WriteLine($"Antal kommende reservationer: {kommende.Count}");

            if (sommerhusReservationer.Any())
            {
                var førsteDato = sommerhusReservationer.Min(r => r.StartDato);
                var sidsteDato = sommerhusReservationer.Max(r => r.SlutDato);
                Console.WriteLine($"Første reservation: {førsteDato:dd/MM/yyyy}");
                Console.WriteLine($"Sidste reservation: {sidsteDato:dd/MM/yyyy}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved søgning efter reservationer: {ex.Message}");
        }
    }

    private void VisReservationDetaljer(Reservation reservation)
    {
        Console.WriteLine($"Reservations ID: {reservation.GetReservationId()}");
        Console.WriteLine($"Sommerhus ID: {reservation.SommerhusId}");
        Console.WriteLine($"Kunde ID: {reservation.KundeId}");
        Console.WriteLine($"Startdato: {reservation.StartDato:dd/MM/yyyy}");
        Console.WriteLine($"Slutdato: {reservation.SlutDato:dd/MM/yyyy}");
        Console.WriteLine($"Antal dage: {(reservation.SlutDato - reservation.StartDato).Days}");
    }
    #endregion

    #region Område-metoder
    private async Task OpretNytOmråde()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Nyt Område ===\n");

        Område nytOmråde = new Område();

        Console.Write("Indtast områdenavn: ");
        nytOmråde.Name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nytOmråde.Name))
        {
            Console.WriteLine("Områdenavn kan ikke være tomt.");
            return;
        }

        Console.Write("Indtast konsulent ID: ");
        if (!int.TryParse(Console.ReadLine(), out int konsulentId))
        {
            Console.WriteLine("Ugyldigt konsulent ID.");
            return;
        }
        nytOmråde.KonsulentId = konsulentId;

        try
        {
            Område.DatabaseHelper.CreateOmråde(nytOmråde);
            Console.WriteLine($"\nOmråde oprettet med ID: {nytOmråde.GetOmrådeId()}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved oprettelse af område: {ex.Message}");
        }
    }

    private async Task VisAlleOmråder()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Områder ===\n");

        try
        {
            var områder = Område.DatabaseHelper.ReadAllOmråder();
            VisPaginering(områder, VisOmrådeDetaljer, "områder");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af områder: {ex.Message}");
        }
    }

    private async Task SøgOmråde()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Efter Område ===\n");
        Console.Write("Indtast område ID: ");

        if (int.TryParse(Console.ReadLine(), out int områdeId))
        {
            try
            {
                var område = Område.DatabaseHelper.ReadOmrådeById(områdeId);
                if (område != null)
                {
                    VisOmrådeDetaljer(område);
                }
                else
                {
                    Console.WriteLine("Område ikke fundet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter område: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt område ID.");
        }
    }

    private async Task RedigerOmråde()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Område ===\n");
        Console.Write("Indtast område ID der skal redigeres: ");

        if (!int.TryParse(Console.ReadLine(), out int områdeId))
        {
            Console.WriteLine("Ugyldigt område ID.");
            return;
        }

        try
        {
            var område = Område.DatabaseHelper.ReadOmrådeById(områdeId);
            if (område == null)
            {
                Console.WriteLine("Område ikke fundet.");
                return;
            }

            Console.WriteLine("\nNuværende værdier:");
            VisOmrådeDetaljer(område);

            Console.WriteLine("\nIndtast nye værdier (tryk Enter for at beholde nuværende værdi):");

            Console.Write($"Nyt navn ({område.Name}): ");
            string nytNavn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytNavn))
                område.Name = nytNavn;

            Console.Write($"Ny konsulent ID ({område.KonsulentId}): ");
            string nyKonsulentId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyKonsulentId) && int.TryParse(nyKonsulentId, out int konsulentId))
                område.KonsulentId = konsulentId;

            Område.DatabaseHelper.UpdateOmråde(område);
            Console.WriteLine("Område opdateret succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af område: {ex.Message}");
        }
    }

    private async Task SletOmråde()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Område ===\n");
        Console.Write("Indtast område ID der skal slettes: ");

        if (int.TryParse(Console.ReadLine(), out int områdeId))
        {
            try
            {
                var område = Område.DatabaseHelper.ReadOmrådeById(områdeId);
                if (område == null)
                {
                    Console.WriteLine("Område ikke fundet.");
                    return;
                }

                Console.WriteLine("\nFølgende område vil blive slettet:");
                VisOmrådeDetaljer(område);

                Console.Write("\nEr du sikker på at du vil slette dette område? (ja/nej): ");
                if (Console.ReadLine().ToLower() == "ja")
                {
                    Område.DatabaseHelper.DeleteOmråde(områdeId);
                    Console.WriteLine("Område slettet succesfuldt!");
                }
                else
                {
                    Console.WriteLine("Sletning annulleret.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved sletning af område: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt område ID.");
        }
    }

    private async Task VisOmrådeStatistik()
    {
        Console.Clear();
        Console.WriteLine("=== Område Statistik ===\n");
        Console.Write("Indtast område ID: ");

        if (!int.TryParse(Console.ReadLine(), out int områdeId))
        {
            Console.WriteLine("Ugyldigt område ID.");
            return;
        }

        try
        {
            var område = Område.DatabaseHelper.ReadOmrådeById(områdeId);
            if (område == null)
            {
                Console.WriteLine("Område ikke fundet.");
                return;
            }

            var sommerhuse = Sommerhus.DatabaseHelper.ReadAllSommerhuse()
                .Where(s => s.OmrådeId == områdeId).ToList();

            Console.WriteLine($"\nStatistik for område: {område.Name}");
            Console.WriteLine($"Antal sommerhuse: {sommerhuse.Count}");
            
            if (sommerhuse.Any())
            {
                Console.WriteLine($"Gennemsnitlig basepris: {sommerhuse.Average(s => s.BasePris):C}");
                Console.WriteLine("\nFordeling af klassificeringer:");
                var klassificeringer = sommerhuse.GroupBy(s => s.Klassificering)
                    .Select(g => new { Klassificering = g.Key, Antal = g.Count() });
                foreach (var k in klassificeringer)
                {
                    Console.WriteLine($"{k.Klassificering}: {k.Antal}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af områdestatistik: {ex.Message}");
        }
    }

    private async Task VisSommerhuseIOmråde()
    {
        Console.Clear();
        Console.WriteLine("=== Sommerhuse i Område ===\n");
        Console.Write("Indtast område ID: ");

        if (!int.TryParse(Console.ReadLine(), out int områdeId))
        {
            Console.WriteLine("Ugyldigt område ID.");
            return;
        }

        try
        {
            var område = Område.DatabaseHelper.ReadOmrådeById(områdeId);
            if (område == null)
            {
                Console.WriteLine("Område ikke fundet.");
                return;
            }

            Console.WriteLine($"\nSommerhuse i {område.Name}:");
            var sommerhuse = Sommerhus.DatabaseHelper.ReadAllSommerhuse()
                .Where(s => s.OmrådeId == områdeId).ToList();

            if (sommerhuse.Count == 0)
            {
                Console.WriteLine("Ingen sommerhuse fundet i dette område.");
                return;
            }

            foreach (var sommerhus in sommerhuse)
            {
                VisSommerhusDetaljer(sommerhus);
                Console.WriteLine(new string('-', 50));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af sommerhuse: {ex.Message}");
        }
    }

    private void VisOmrådeDetaljer(Område område)
    {
        Console.WriteLine($"Område ID: {område.GetOmrådeId()}");
        Console.WriteLine($"Navn: {område.Navn}");
        Console.WriteLine($"Konsulent ID: {område.KonsulentId}");
    }
    #endregion

    #region Sæsonkategori-metoder
    private async Task OpretNySæsonkategori()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Ny Sæsonkategori ===\n");

        SæsonKategori nySæson = new SæsonKategori();

        Console.Write("Indtast kategori (super/høj/mellem/lav): ");
        nySæson.Kategori = Console.ReadLine().ToLower();
        if (!new[] { "super", "høj", "mellem", "lav" }.Contains(nySæson.Kategori))
        {
            Console.WriteLine("Ugyldig kategori.");
            return;
        }

        Console.Write("Indtast prismultiplikator (f.eks. 200 for dobbelt pris): ");
        if (!int.TryParse(Console.ReadLine(), out int multiplikator))
        {
            Console.WriteLine("Ugyldig prismultiplikator.");
            return;
        }
        nySæson.PrisMultiplikator = multiplikator;

        Console.WriteLine("\nIndtast ugenumre (1-52, adskilt af komma):");
        string ugenumreInput = Console.ReadLine();
        try
        {
            nySæson.UgeNumre = ugenumreInput.Split(',')
                .Select(u => int.Parse(u.Trim()))
                .Where(u => u >= 1 && u <= 52)
                .Distinct()
                .ToList();

            if (nySæson.UgeNumre.Count == 0)
            {
                Console.WriteLine("Ingen gyldige ugenumre angivet.");
                return;
            }
        }
        catch
        {
            Console.WriteLine("Ugyldigt format for ugenumre.");
            return;
        }

        try
        {
            SæsonKategori.CreateSæsonKategori(nySæson);
            Console.WriteLine($"\nSæsonkategori oprettet med ID: {nySæson.GetSæsonKategoriId()}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved oprettelse af sæsonkategori: {ex.Message}");
        }
    }

    private async Task VisAlleSæsonkategorier()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Sæsonkategorier ===\n");

        try
        {
            var kategorier = SæsonKategori.ReadAllSæsonKategorier();
            VisPaginering(kategorier, VisSæsonkategoriDetaljer, "sæsonkategorier");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af sæsonkategorier: {ex.Message}");
        }
    }

    private async Task SøgSæsonkategori()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Efter Sæsonkategori ===\n");
        Console.Write("Indtast sæsonkategori ID: ");

        if (int.TryParse(Console.ReadLine(), out int kategoriId))
        {
            try
            {
                var kategori = SæsonKategori.ReadSæsonKategoriById(kategoriId);
                if (kategori != null)
                {
                    VisSæsonkategoriDetaljer(kategori);
                }
                else
                {
                    Console.WriteLine("Sæsonkategori ikke fundet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter sæsonkategori: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt sæsonkategori ID.");
        }
    }

    private async Task RedigerSæsonkategori()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Sæsonkategori ===\n");
        Console.Write("Indtast sæsonkategori ID der skal redigeres: ");

        if (!int.TryParse(Console.ReadLine(), out int kategoriId))
        {
            Console.WriteLine("Ugyldigt sæsonkategori ID.");
            return;
        }

        try
        {
            var kategori = SæsonKategori.ReadSæsonKategoriById(kategoriId);
            if (kategori == null)
            {
                Console.WriteLine("Sæsonkategori ikke fundet.");
                return;
            }

            Console.WriteLine("\nNuværende værdier:");
            VisSæsonkategoriDetaljer(kategori);

            Console.WriteLine("\nIndtast nye værdier (tryk Enter for at beholde nuværende værdi):");

            Console.Write($"Ny kategori ({kategori.Kategori}): ");
            string nyKategori = Console.ReadLine().ToLower();
            if (!string.IsNullOrWhiteSpace(nyKategori))
            {
                if (new[] { "super", "høj", "mellem", "lav" }.Contains(nyKategori))
                    kategori.Kategori = nyKategori;
                else
                    Console.WriteLine("Ugyldig kategori. Beholder nuværende værdi.");
            }

            Console.Write($"Ny prismultiplikator ({kategori.PrisMultiplikator}): ");
            string nyMultiplikator = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyMultiplikator) && int.TryParse(nyMultiplikator, out int multiplikator))
                kategori.PrisMultiplikator = multiplikator;

            Console.Write($"Nye ugenumre ({string.Join(", ", kategori.UgeNumre)}): ");
            string nyeUgenumre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyeUgenumre))
            {
                try
                {
                    var ugenumre = nyeUgenumre.Split(',')
                        .Select(u => int.Parse(u.Trim()))
                        .Where(u => u >= 1 && u <= 52)
                        .Distinct()
                        .ToList();

                    if (ugenumre.Count > 0)
                        kategori.UgeNumre = ugenumre;
                    else
                        Console.WriteLine("Ingen gyldige ugenumre angivet. Beholder nuværende værdier.");
                }
                catch
                {
                    Console.WriteLine("Ugyldigt format for ugenumre. Beholder nuværende værdier.");
                }
            }

            SæsonKategori.UpdateSæsonKategori(kategori);
            Console.WriteLine("Sæsonkategori opdateret succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af sæsonkategori: {ex.Message}");
        }
    }

    private async Task SletSæsonkategori()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Sæsonkategori ===\n");
        Console.Write("Indtast sæsonkategori ID der skal slettes: ");

        if (int.TryParse(Console.ReadLine(), out int kategoriId))
        {
            try
            {
                var kategori = SæsonKategori.ReadSæsonKategoriById(kategoriId);
                if (kategori == null)
                {
                    Console.WriteLine("Sæsonkategori ikke fundet.");
                    return;
                }

                Console.WriteLine("\nFølgende sæsonkategori vil blive slettet:");
                VisSæsonkategoriDetaljer(kategori);

                Console.Write("\nEr du sikker på at du vil slette denne sæsonkategori? (ja/nej): ");
                if (Console.ReadLine().ToLower() == "ja")
                {
                    SæsonKategori.DeleteSæsonKategori(kategoriId);
                    Console.WriteLine("Sæsonkategori slettet succesfuldt!");
                }
                else
                {
                    Console.WriteLine("Sletning annulleret.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved sletning af sæsonkategori: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt sæsonkategori ID.");
        }
    }

    private async Task VisSæsonkategoriPriser()
    {
        Console.Clear();
        Console.WriteLine("=== Sæsonkategori Priser ===\n");
        Console.Write("Indtast sæsonkategori ID: ");

        if (!int.TryParse(Console.ReadLine(), out int kategoriId))
        {
            Console.WriteLine("Ugyldigt sæsonkategori ID.");
            return;
        }

        try
        {
            var kategori = SæsonKategori.ReadSæsonKategoriById(kategoriId);
            if (kategori == null)
            {
                Console.WriteLine("Sæsonkategori ikke fundet.");
                return;
            }

            Console.WriteLine($"\nPriser for {kategori.Kategori}-sæson (multiplikator: {kategori.PrisMultiplikator}%):");
            
            var sommerhuse = Sommerhus.DatabaseHelper.ReadAllSommerhuse();
            Console.WriteLine("\nEksempel priser for forskellige sommerhuse:");
            
            // Vis eksempler på priser for forskellige basispriser
            var eksempelSommerhuse = sommerhuse.GroupBy(s => s.BasePris)
                                             .Select(g => g.First())
                                             .Take(3)
                                             .OrderBy(s => s.BasePris);

            foreach (var sommerhus in eksempelSommerhuse)
            {
                decimal sæsonPris = sommerhus.BasePris * (kategori.PrisMultiplikator / 100.0m);
                Console.WriteLine($"Sommerhus {sommerhus.SommerhusId,-5}: {sommerhus.BasePris,8:C} -> {sæsonPris,8:C}");
            }

            Console.WriteLine($"\nGælder for uger: {string.Join(", ", kategori.UgeNumre)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved visning af sæsonkategori priser: {ex.Message}");
        }
    }

    private void VisSæsonkategoriDetaljer(SæsonKategori kategori)
    {
        Console.WriteLine($"Sæsonkategori ID: {kategori.GetSæsonKategoriId()}");
        Console.WriteLine($"Kategori: {kategori.Kategori}");
        Console.WriteLine($"Prismultiplikator: {kategori.PrisMultiplikator}%");
        Console.WriteLine($"Ugenumre: {string.Join(", ", kategori.UgeNumre)}");
    }
    #endregion

    #region Ejer-metoder
    private async Task OpretNyEjer()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Ny Ejer ===");
        
        Console.Write("Navn: ");
        string navn = Console.ReadLine();
        
        Console.Write("Email: ");
        string email = Console.ReadLine();
        
        Console.Write("Telefon: ");
        string telefon = Console.ReadLine();
        
        Console.Write("Adresse: ");
        string adresse = Console.ReadLine();

        Console.Write("Kontrakt startdato (dd/MM/yyyy): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime startDato);

        Console.Write("Kontrakt slutdato (dd/MM/yyyy): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime slutDato);

        var ejer = new Ejer
        {
            Navn = navn,
            Email = email,
            Telefon = telefon,
            Adresse = adresse,
            KontraktStartDato = startDato,
            KontraktSlutDato = slutDato
        };

        try
        {
            DatabaseHelper.CreateEjer(ejer);
            Console.WriteLine($"\nEjer oprettet med ID: {ejer.GetEjerId()}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFejl ved oprettelse af ejer: {ex.Message}");
        }

        Console.WriteLine("Tryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task VisAlleEjere()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Ejere ===\n");
        
        try
        {
            var ejere = DatabaseHelper.ReadAllEjere();
            VisPaginering(ejere, VisEjerDetaljer, "ejere");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af ejere: {ex.Message}");
        }
        
        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task SøgEjer()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Ejer ===\n");
        
        Console.Write("Indtast ejer ID: ");
        if (int.TryParse(Console.ReadLine(), out int ejerId))
        {
            try
            {
                var ejer = DatabaseHelper.ReadEjerById(ejerId);
                if (ejer != null)
                {
                    Console.WriteLine($"\nID: {ejer.GetEjerId()}");
                    Console.WriteLine($"Navn: {ejer.Navn}");
                    Console.WriteLine($"Email: {ejer.Email}");
                    Console.WriteLine($"Telefon: {ejer.Telefon}");
                    Console.WriteLine($"Adresse: {ejer.Adresse}");
                    Console.WriteLine($"Kontrakt: {ejer.KontraktStartDato:dd/MM/yyyy} - {ejer.KontraktSlutDato:dd/MM/yyyy}");
                }
                else
                {
                    Console.WriteLine("Ingen ejer fundet med det ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter ejer: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt ID format.");
        }
        
        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task RedigerEjer()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Ejer ===\n");
        
        Console.Write("Indtast ejer ID: ");
        if (int.TryParse(Console.ReadLine(), out int ejerId))
        {
            try
            {
                var ejer = DatabaseHelper.ReadEjerById(ejerId);
                if (ejer != null)
                {
                    Console.WriteLine("\nTryk Enter for at beholde eksisterende værdier");
                    
                    Console.Write($"Nyt navn ({ejer.Navn}): ");
                    string navn = Console.ReadLine();
                    if (!string.IsNullOrEmpty(navn)) ejer.Navn = navn;
                    
                    Console.Write($"Ny email ({ejer.Email}): ");
                    string email = Console.ReadLine();
                    if (!string.IsNullOrEmpty(email)) ejer.Email = email;
                    
                    Console.Write($"Nyt telefonnummer ({ejer.Telefon}): ");
                    string telefon = Console.ReadLine();
                    if (!string.IsNullOrEmpty(telefon)) ejer.Telefon = telefon;
                    
                    Console.Write($"Ny adresse ({ejer.Adresse}): ");
                    string adresse = Console.ReadLine();
                    if (!string.IsNullOrEmpty(adresse)) ejer.Adresse = adresse;

                    DatabaseHelper.UpdateEjer(ejer);
                    Console.WriteLine("Ejer opdateret!");
                }
                else
                {
                    Console.WriteLine("Ingen ejer fundet med det ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved opdatering af ejer: {ex.Message}");
            }
        }
        
        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task SletEjer()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Ejer ===\n");
        
        Console.Write("Indtast ejer ID: ");
        if (int.TryParse(Console.ReadLine(), out int ejerId))
        {
            try
            {
                var ejer = DatabaseHelper.ReadEjerById(ejerId);
                if (ejer != null)
                {
                    Console.WriteLine($"\nEr du sikker på du vil slette {ejer.Navn}? (j/n): ");
                    if (Console.ReadLine().ToLower() == "j")
                    {
                        DatabaseHelper.DeleteEjer(ejerId);
                        Console.WriteLine("Ejer slettet!");
                    }
                }
                else
                {
                    Console.WriteLine("Ingen ejer fundet med det ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved sletning af ejer: {ex.Message}");
            }
        }
        
        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task VisEjersSommerhuse()
    {
        Console.Clear();
        Console.WriteLine("=== Vis Ejers Sommerhuse ===\n");
        
        Console.Write("Indtast ejer ID: ");
        if (int.TryParse(Console.ReadLine(), out int ejerId))
        {
            // Her skal Person A's database-kode kaldes for at hente ejer og deres sommerhuse
        }
        
        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private void VisEjerDetaljer(Ejer ejer)
    {
        Console.WriteLine($"ID: {ejer.GetEjerId()}");
        Console.WriteLine($"Navn: {ejer.Navn}");
        Console.WriteLine($"Email: {ejer.Email}");
        Console.WriteLine($"Telefon: {ejer.Telefon}");
        Console.WriteLine($"Adresse: {ejer.Adresse}");
        Console.WriteLine($"Kontrakt: {ejer.KontraktStartDato:dd/MM/yyyy} - {ejer.KontraktSlutDato:dd/MM/yyyy}");
    }
    #endregion

    #region Kunde-metoder
    private async Task VisKundeMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Kunde Administration ===");
            Console.WriteLine("1. Opret ny kunde");
            Console.WriteLine("2. Vis alle kunder");
            Console.WriteLine("3. Søg efter kunde");
            Console.WriteLine("4. Rediger kunde");
            Console.WriteLine("5. Slet kunde");
            Console.WriteLine("6. Vis kundens reservationer");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretNyKunde();
                    break;
                case "2":
                    await VisAlleKunder();
                    break;
                case "3":
                    await SøgKunde();
                    break;
                case "4":
                    await RedigerKunde();
                    break;
                case "5":
                    await SletKunde();
                    break;
                case "6":
                    await VisKundensReservationer();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task OpretNyKunde()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Ny Kunde ===");
        
        Console.Write("Navn: ");
        string navn = Console.ReadLine();
        
        Console.Write("Email: ");
        string email = Console.ReadLine();
        
        Console.Write("Telefon: ");
        string telefon = Console.ReadLine();
        
        Console.Write("Adresse: ");
        string adresse = Console.ReadLine();

        var kunde = new Kunde
        {
            Navn = navn,
            Email = email,
            Telefon = telefon,
            Adresse = adresse
        };

        try
        {
            DatabaseHelper.CreateKunde(kunde);
            Console.WriteLine($"\nKunde oprettet med ID: {kunde.GetKundeId()}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFejl ved oprettelse af kunde: {ex.Message}");
        }
    }

    private async Task VisAlleKunder()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Kunder ===\n");

        try
        {
            var kunder = DatabaseHelper.ReadAllKunder();
            VisPaginering(kunder, VisKundeDetaljer, "kunder");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af kunder: {ex.Message}");
        }
    }

    private async Task SøgKunde()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Efter Kunde ===\n");
        Console.Write("Indtast kunde ID: ");

        if (int.TryParse(Console.ReadLine(), out int kundeId))
        {
            try
            {
                var kunde = DatabaseHelper.ReadKundeById(kundeId);
                if (kunde != null)
                {
                    VisKundeDetaljer(kunde);
                }
                else
                {
                    Console.WriteLine("Kunde ikke fundet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter kunde: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt kunde ID.");
        }
    }

    private async Task RedigerKunde()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Kunde ===\n");
        Console.Write("Indtast kunde ID der skal redigeres: ");

        if (!int.TryParse(Console.ReadLine(), out int kundeId))
        {
            Console.WriteLine("Ugyldigt kunde ID.");
            return;
        }

        try
        {
            var kunde = DatabaseHelper.ReadKundeById(kundeId);
            if (kunde == null)
            {
                Console.WriteLine("Kunde ikke fundet.");
                return;
            }

            Console.WriteLine("\nNuværende værdier:");
            VisKundeDetaljer(kunde);

            Console.WriteLine("\nIndtast nye værdier (tryk Enter for at beholde nuværende værdi):");

            Console.Write($"Nyt navn ({kunde.Navn}): ");
            string nytNavn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytNavn))
                kunde.Navn = nytNavn;

            Console.Write($"Ny email ({kunde.Email}): ");
            string nyEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyEmail))
                kunde.Email = nyEmail;

            Console.Write($"Nyt telefonnummer ({kunde.Telefon}): ");
            string nyTelefon = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyTelefon))
                kunde.Telefon = nyTelefon;

            Console.Write($"Ny adresse ({kunde.Adresse}): ");
            string nyAdresse = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyAdresse))
                kunde.Adresse = nyAdresse;

            DatabaseHelper.UpdateKunde(kunde);
            Console.WriteLine("Kunde opdateret succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af kunde: {ex.Message}");
        }
    }

    private async Task SletKunde()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Kunde ===\n");
        Console.Write("Indtast kunde ID der skal slettes: ");

        if (!int.TryParse(Console.ReadLine(), out int kundeId))
        {
            Console.WriteLine("Ugyldigt kunde ID.");
            return;
        }

        try
        {
            var kunde = DatabaseHelper.ReadKundeById(kundeId);
            if (kunde == null)
            {
                Console.WriteLine("Kunde ikke fundet.");
                return;
            }

            Console.WriteLine("\nFølgende kunde vil blive slettet:");
            VisKundeDetaljer(kunde);

            Console.Write("\nEr du sikker på at du vil slette denne kunde? (ja/nej): ");
            if (Console.ReadLine().ToLower() == "ja")
            {
                DatabaseHelper.DeleteKunde(kundeId);
                Console.WriteLine("Kunde slettet succesfuldt!");
            }
            else
            {
                Console.WriteLine("Sletning annulleret.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved sletning af kunde: {ex.Message}");
        }
    }

    private async Task VisKundensReservationer()
    {
        Console.Clear();
        Console.WriteLine("=== Kundens Reservationer ===\n");
        Console.Write("Indtast kunde ID: ");

        if (!int.TryParse(Console.ReadLine(), out int kundeId))
        {
            Console.WriteLine("Ugyldigt kunde ID.");
            return;
        }

        try
        {
            var kunde = DatabaseHelper.ReadKundeById(kundeId);
            if (kunde == null)
            {
                Console.WriteLine("Kunde ikke fundet.");
                return;
            }

            Console.WriteLine($"\nReservationer for {kunde.Navn}:");
            var alleReservationer = Reservation.DatabaseHelper.ReadAllReservationer();
            var kundeReservationer = alleReservationer.Where(r => r.KundeId == kundeId).ToList();

            if (kundeReservationer.Count == 0)
            {
                Console.WriteLine("Ingen reservationer fundet for denne kunde.");
                return;
            }

            foreach (var reservation in kundeReservationer)
            {
                VisReservationDetaljer(reservation);
                Console.WriteLine(new string('-', 50));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af kundens reservationer: {ex.Message}");
        }
    }

    private void VisKundeDetaljer(Kunde kunde)
    {
        Console.WriteLine($"Kunde ID: {kunde.GetKundeId()}");
        Console.WriteLine($"Navn: {kunde.Navn}");
        Console.WriteLine($"Email: {kunde.Email}");
        Console.WriteLine($"Telefon: {kunde.Telefon}");
        Console.WriteLine($"Adresse: {kunde.Adresse}");
    }
    #endregion

    #region Opsynsmand-metoder
    private async Task VisOpsynsmandMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Opsynsmand Administration ===");
            Console.WriteLine("1. Opret ny opsynsmand");
            Console.WriteLine("2. Vis alle opsynsmænd");
            Console.WriteLine("3. Søg efter opsynsmand");
            Console.WriteLine("4. Rediger opsynsmand");
            Console.WriteLine("5. Slet opsynsmand");
            Console.WriteLine("6. Vis opsynsmands sommerhuse");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretNyOpsynsmand();
                    break;
                case "2":
                    await VisAlleOpsynsmænd();
                    break;
                case "3":
                    await SøgOpsynsmand();
                    break;
                case "4":
                    await RedigerOpsynsmand();
                    break;
                case "5":
                    await SletOpsynsmand();
                    break;
                case "6":
                    await VisOpsynsmandsSommerhuse();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task OpretNyOpsynsmand()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Ny Opsynsmand ===");
        
        Console.Write("Navn: ");
        string navn = Console.ReadLine();
        
        Console.Write("Email: ");
        string email = Console.ReadLine();
        
        Console.Write("Telefon: ");
        string telefon = Console.ReadLine();
        
        Console.Write("Område ID: ");
        if (!int.TryParse(Console.ReadLine(), out int områdeId))
        {
            Console.WriteLine("Ugyldigt område ID.");
            return;
        }

        var opsynsmand = new Opsynsmand
        {
            Navn = navn,
            Email = email,
            Telefon = telefon,
            OmrådeId = områdeId
        };

        try
        {
            DatabaseHelper.CreateOpsynsmand(opsynsmand);
            Console.WriteLine($"\nOpsynsmand oprettet med ID: {opsynsmand.GetOpsynsmandId()}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFejl ved oprettelse af opsynsmand: {ex.Message}");
        }
    }

    private async Task VisAlleOpsynsmænd()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Opsynsmænd ===\n");

        try
        {
            var opsynsmænd = DatabaseHelper.ReadAllOpsynsmænd();
            VisPaginering(opsynsmænd, VisOpsynsmandDetaljer, "opsynsmænd");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af opsynsmænd: {ex.Message}");
        }
    }

    private async Task SøgOpsynsmand()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Efter Opsynsmand ===\n");
        Console.Write("Indtast opsynsmand ID: ");

        if (int.TryParse(Console.ReadLine(), out int opsynsmandId))
        {
            try
            {
                var opsynsmand = DatabaseHelper.ReadOpsynsmandById(opsynsmandId);
                if (opsynsmand != null)
                {
                    VisOpsynsmandDetaljer(opsynsmand);
                }
                else
                {
                    Console.WriteLine("Opsynsmand ikke fundet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter opsynsmand: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt opsynsmand ID.");
        }
    }

    private async Task RedigerOpsynsmand()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Opsynsmand ===\n");
        Console.Write("Indtast opsynsmand ID der skal redigeres: ");

        if (!int.TryParse(Console.ReadLine(), out int opsynsmandId))
        {
            Console.WriteLine("Ugyldigt opsynsmand ID.");
            return;
        }

        try
        {
            var opsynsmand = SqlConnect.DatabaseHelper.ReadOpsynsmandById(opsynsmandId);
            if (opsynsmand == null)
            {
                Console.WriteLine("Opsynsmand ikke fundet.");
                return;
            }

            Console.WriteLine("\nNuværende værdier:");
            VisOpsynsmandDetaljer(opsynsmand);

            Console.WriteLine("\nIndtast nye værdier (tryk Enter for at beholde nuværende værdi):");

            Console.Write($"Nyt navn ({opsynsmand.Navn}): ");
            string nytNavn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytNavn))
                opsynsmand.Navn = nytNavn;

            Console.Write($"Ny email ({opsynsmand.Email}): ");
            string nyEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyEmail))
                opsynsmand.Email = nyEmail;

            Console.Write($"Nyt telefonnummer ({opsynsmand.Telefon}): ");
            string nyTelefon = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyTelefon))
                opsynsmand.Telefon = nyTelefon;

            Console.Write($"Nyt område ID ({opsynsmand.OmrådeId}): ");
            string nytOmrådeId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytOmrådeId) && int.TryParse(nytOmrådeId, out int områdeId))
                opsynsmand.OmrådeId = områdeId;

            SqlConnect.DatabaseHelper.UpdateOpsynsmand(opsynsmand);
            Console.WriteLine("Opsynsmand opdateret succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af opsynsmand: {ex.Message}");
        }
    }

    private async Task SletOpsynsmand()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Opsynsmand ===\n");
        Console.Write("Indtast opsynsmand ID der skal slettes: ");

        if (!int.TryParse(Console.ReadLine(), out int opsynsmandId))
        {
            Console.WriteLine("Ugyldigt opsynsmand ID.");
            return;
        }

        try
        {
            var opsynsmand = SqlConnect.DatabaseHelper.ReadOpsynsmandById(opsynsmandId);
            if (opsynsmand == null)
            {
                Console.WriteLine("Opsynsmand ikke fundet.");
                return;
            }

            Console.WriteLine("\nFølgende opsynsmand vil blive slettet:");
            VisOpsynsmandDetaljer(opsynsmand);

            Console.Write("\nEr du sikker på at du vil slette denne opsynsmand? (ja/nej): ");
            if (Console.ReadLine().ToLower() == "ja")
            {
                SqlConnect.DatabaseHelper.DeleteOpsynsmand(opsynsmandId);
                Console.WriteLine("Opsynsmand slettet succesfuldt!");
            }
            else
            {
                Console.WriteLine("Sletning annulleret.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved sletning af opsynsmand: {ex.Message}");
        }
    }

    private async Task VisOpsynsmandsSommerhuse()
    {
        Console.Clear();
        Console.WriteLine("=== Opsynsmands Sommerhuse ===\n");
        Console.Write("Indtast opsynsmand ID: ");

        if (!int.TryParse(Console.ReadLine(), out int opsynsmandId))
        {
            Console.WriteLine("Ugyldigt opsynsmand ID.");
            return;
        }

        try
        {
            var opsynsmand = SqlConnect.DatabaseHelper.ReadOpsynsmandById(opsynsmandId);
            if (opsynsmand == null)
            {
                Console.WriteLine("Opsynsmand ikke fundet.");
                return;
            }

            var sommerhuse = SqlConnect.Sommerhus.ReadAllSommerhuse()
                .Where(s => s.OpsynsmandId == opsynsmandId)
                .ToList();

            if (sommerhuse.Count == 0)
            {
                Console.WriteLine($"Ingen sommerhuse fundet for opsynsmand {opsynsmand.Navn}");
                return;
            }

            Console.WriteLine($"\nSommerhuse under tilsyn af {opsynsmand.Navn}:");
            foreach (var sommerhus in sommerhuse)
            {
                VisSommerhusDetaljer(sommerhus);
                Console.WriteLine(new string('-', 50));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af sommerhuse: {ex.Message}");
        }
    }

    private void VisOpsynsmandDetaljer(SqlConnect.Opsynsmand opsynsmand)
    {
        Console.WriteLine($"Opsynsmand ID: {opsynsmand.GetOpsynsmandId()}");
        Console.WriteLine($"Navn: {opsynsmand.Navn}");
        Console.WriteLine($"Email: {opsynsmand.Email}");
        Console.WriteLine($"Telefon: {opsynsmand.Telefon}");
        Console.WriteLine($"Rolle: {opsynsmand.Rolle}");
    }
    #endregion

    #region Konsulent-metoder
    private async Task VisKonsulentMenu()
    {
        bool fortsæt = true;
        while (fortsæt)
        {
            Console.Clear();
            Console.WriteLine("=== Konsulent Administration ===");
            Console.WriteLine("1. Opret ny konsulent");
            Console.WriteLine("2. Vis alle konsulenter");
            Console.WriteLine("3. Søg efter konsulent");
            Console.WriteLine("4. Rediger konsulent");
            Console.WriteLine("5. Slet konsulent");
            Console.WriteLine("6. Vis konsulentens områder");
            Console.WriteLine("7. Vis konsulentens godkendte sommerhuse");
            Console.WriteLine("0. Tilbage til hovedmenu");

            Console.Write("\nVælg en mulighed: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    await OpretNyKonsulent();
                    break;
                case "2":
                    await VisAlleKonsulenter();
                    break;
                case "3":
                    await SøgKonsulent();
                    break;
                case "4":
                    await RedigerKonsulent();
                    break;
                case "5":
                    await SletKonsulent();
                    break;
                case "6":
                    await VisKonsulentsOmråder();
                    break;
                case "7":
                    await VisKonsulentsGodkendteSommerhuse();
                    break;
                case "0":
                    fortsæt = false;
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk en tast for at fortsætte.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task OpretNyKonsulent()
    {
        Console.Clear();
        Console.WriteLine("=== Opret Ny Konsulent ===");
        
        Console.Write("Navn: ");
        string navn = Console.ReadLine();
        
        Console.Write("Email: ");
        string email = Console.ReadLine();
        
        Console.Write("Telefon: ");
        string telefon = Console.ReadLine();
        
        Console.Write("Adresse: ");
        string adresse = Console.ReadLine();

        var konsulent = new Konsulent
        {
            Navn = navn,
            Email = email,
            Telefon = telefon,
            Adresse = adresse
        };

        try
        {
            Konsulent.DatabaseHelper.CreateKonsulent(konsulent);
            Console.WriteLine($"\nKonsulent oprettet med ID: {konsulent.GetKonsulentId()}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFejl ved oprettelse af konsulent: {ex.Message}");
        }

        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task VisAlleKonsulenter()
    {
        Console.Clear();
        Console.WriteLine("=== Alle Konsulenter ===\n");

        try
        {
            var konsulenter = Konsulent.DatabaseHelper.ReadAllKonsulenter();
            VisPaginering(konsulenter, VisKonsulentDetaljer, "konsulenter");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af konsulenter: {ex.Message}");
            Console.WriteLine("\nTryk en tast for at fortsætte...");
            Console.ReadKey();
        }
    }

    private async Task SøgKonsulent()
    {
        Console.Clear();
        Console.WriteLine("=== Søg Efter Konsulent ===\n");
        Console.Write("Indtast konsulent ID: ");

        if (int.TryParse(Console.ReadLine(), out int konsulentId))
        {
            try
            {
                var konsulent = Konsulent.DatabaseHelper.ReadKonsulentById(konsulentId);
                if (konsulent != null)
                {
                    VisKonsulentDetaljer(konsulent);
                }
                else
                {
                    Console.WriteLine("Konsulent ikke fundet.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved søgning efter konsulent: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldigt konsulent ID.");
        }

        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task RedigerKonsulent()
    {
        Console.Clear();
        Console.WriteLine("=== Rediger Konsulent ===\n");
        Console.Write("Indtast konsulent ID der skal redigeres: ");

        if (!int.TryParse(Console.ReadLine(), out int konsulentId))
        {
            Console.WriteLine("Ugyldigt konsulent ID.");
            Console.WriteLine("\nTryk en tast for at fortsætte...");
            Console.ReadKey();
            return;
        }

        try
        {
            var konsulent = Konsulent.DatabaseHelper.ReadKonsulentById(konsulentId);
            if (konsulent == null)
            {
                Console.WriteLine("Konsulent ikke fundet.");
                Console.WriteLine("\nTryk en tast for at fortsætte...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nNuværende værdier:");
            VisKonsulentDetaljer(konsulent);

            Console.WriteLine("\nIndtast nye værdier (tryk Enter for at beholde nuværende værdi):");

            Console.Write($"Nyt navn ({konsulent.Navn}): ");
            string nytNavn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nytNavn))
                konsulent.Navn = nytNavn;

            Console.Write($"Ny email ({konsulent.Email}): ");
            string nyEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyEmail))
                konsulent.Email = nyEmail;

            Console.Write($"Nyt telefonnummer ({konsulent.Telefon}): ");
            string nyTelefon = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyTelefon))
                konsulent.Telefon = nyTelefon;

            Console.Write($"Ny adresse ({konsulent.Adresse}): ");
            string nyAdresse = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyAdresse))
                konsulent.Adresse = nyAdresse;

            Konsulent.DatabaseHelper.UpdateKonsulent(konsulent);
            Console.WriteLine("Konsulent opdateret succesfuldt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved opdatering af konsulent: {ex.Message}");
        }

        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task SletKonsulent()
    {
        Console.Clear();
        Console.WriteLine("=== Slet Konsulent ===\n");
        Console.Write("Indtast konsulent ID der skal slettes: ");

        if (!int.TryParse(Console.ReadLine(), out int konsulentId))
        {
            Console.WriteLine("Ugyldigt konsulent ID.");
            Console.WriteLine("\nTryk en tast for at fortsætte...");
            Console.ReadKey();
            return;
        }

        try
        {
            var konsulent = Konsulent.DatabaseHelper.ReadKonsulentById(konsulentId);
            if (konsulent == null)
            {
                Console.WriteLine("Konsulent ikke fundet.");
                Console.WriteLine("\nTryk en tast for at fortsætte...");
                Console.ReadKey();
                return;
            }

            // Tjek om konsulenten har tilknyttede områder
            var områder = Område.DatabaseHelper.ReadAllOmråder()
                .Where(o => o.KonsulentId == konsulentId)
                .ToList();

            if (områder.Any())
            {
                Console.WriteLine("\nKonsulenten har følgende tilknyttede områder:");
                foreach (var område in områder)
                {
                    Console.WriteLine($"- {område.Navn}");
                }
                Console.WriteLine("\nDu skal først tildele områderne til en anden konsulent.");
                Console.WriteLine("\nTryk en tast for at fortsætte...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nFølgende konsulent vil blive slettet:");
            VisKonsulentDetaljer(konsulent);

            Console.Write("\nEr du sikker på at du vil slette denne konsulent? (ja/nej): ");
            if (Console.ReadLine().ToLower() == "ja")
            {
                Konsulent.DatabaseHelper.DeleteKonsulent(konsulentId);
                Console.WriteLine("Konsulent slettet succesfuldt!");
            }
            else
            {
                Console.WriteLine("Sletning annulleret.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved sletning af konsulent: {ex.Message}");
        }

        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task VisKonsulentsOmråder()
    {
        Console.Clear();
        Console.WriteLine("=== Konsulentens Områder ===\n");
        Console.Write("Indtast konsulent ID: ");

        if (!int.TryParse(Console.ReadLine(), out int konsulentId))
        {
            Console.WriteLine("Ugyldigt konsulent ID.");
            Console.WriteLine("\nTryk en tast for at fortsætte...");
            Console.ReadKey();
            return;
        }

        try
        {
            var konsulent = Konsulent.DatabaseHelper.ReadKonsulentById(konsulentId);
            if (konsulent == null)
            {
                Console.WriteLine("Konsulent ikke fundet.");
                Console.WriteLine("\nTryk en tast for at fortsætte...");
                Console.ReadKey();
                return;
            }

            var områder = Område.DatabaseHelper.ReadAllOmråder()
                .Where(o => o.KonsulentId == konsulentId)
                .ToList();

            if (områder.Count == 0)
            {
                Console.WriteLine($"Ingen områder fundet for konsulent {konsulent.Navn}");
            }
            else
            {
                Console.WriteLine($"\nOmråder under ansvar af {konsulent.Navn}:");
                foreach (var område in områder)
                {
                    VisOmrådeDetaljer(område);
                    Console.WriteLine(new string('-', 50));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af konsulentens områder: {ex.Message}");
        }

        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private async Task VisKonsulentsGodkendteSommerhuse()
    {
        Console.Clear();
        Console.WriteLine("=== Konsulentens Godkendte Sommerhuse ===\n");
        Console.Write("Indtast konsulent ID: ");

        if (!int.TryParse(Console.ReadLine(), out int konsulentId))
        {
            Console.WriteLine("Ugyldigt konsulent ID.");
            Console.WriteLine("\nTryk en tast for at fortsætte...");
            Console.ReadKey();
            return;
        }

        try
        {
            var konsulent = Konsulent.DatabaseHelper.ReadKonsulentById(konsulentId);
            if (konsulent == null)
            {
                Console.WriteLine("Konsulent ikke fundet.");
                Console.WriteLine("\nTryk en tast for at fortsætte...");
                Console.ReadKey();
                return;
            }

            // Hent først konsulentens områder
            var områder = Område.DatabaseHelper.ReadAllOmråder()
                .Where(o => o.KonsulentId == konsulentId)
                .ToList();

            if (områder.Count == 0)
            {
                Console.WriteLine($"Ingen områder fundet for konsulent {konsulent.Navn}");
            }
            else
            {
                // Hent sommerhuse i konsulentens områder
                var sommerhuse = new List<Sommerhus>();
                foreach (var område in områder)
                {
                    var områdeSommerhuse = Sommerhus.DatabaseHelper.ReadAllSommerhuse()
                        .Where(s => s.OmrådeId == område.GetOmrådeId())
                        .ToList();
                    sommerhuse.AddRange(områdeSommerhuse);
                }

                if (sommerhuse.Count == 0)
                {
                    Console.WriteLine($"Ingen sommerhuse fundet i konsulentens områder.");
                }
                else
                {
                    Console.WriteLine($"\nSommerhuse i {konsulent.Navn}'s områder:");
                    foreach (var sommerhus in sommerhuse)
                    {
                        VisSommerhusDetaljer(sommerhus);
                        Console.WriteLine(new string('-', 50));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved hentning af konsulentens sommerhuse: {ex.Message}");
        }

        Console.WriteLine("\nTryk en tast for at fortsætte...");
        Console.ReadKey();
    }

    private void VisKonsulentDetaljer(Konsulent konsulent)
    {
        Console.WriteLine($"Konsulent ID: {konsulent.GetKonsulentId()}");
        Console.WriteLine($"Navn: {konsulent.Navn}");
        Console.WriteLine($"Email: {konsulent.Email}");
        Console.WriteLine($"Telefon: {konsulent.Telefon}");
        Console.WriteLine($"Adresse: {konsulent.Adresse}");
        Console.WriteLine($"Type: {konsulent.Type}");
    }
    #endregion

    #region Hjælpemetoder
    private void VisPaginering<T>(List<T> items, Action<T> displayAction, string itemType)
    {
        if (items.Count == 0)
        {
            Console.WriteLine($"Ingen {itemType} fundet.");
            return;
        }

        int currentPage = 1;
        int totalPages = (int)Math.Ceiling(items.Count / (double)ITEMS_PER_PAGE);

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== {itemType} (Side {currentPage}/{totalPages}) ===\n");

            var pageItems = items
                .Skip((currentPage - 1) * ITEMS_PER_PAGE)
                .Take(ITEMS_PER_PAGE)
                .ToList();

            foreach (var item in pageItems)
            {
                displayAction(item);
                Console.WriteLine(new string('-', 50));
            }

            Console.WriteLine($"\nViser {pageItems.Count} af {items.Count} {itemType}");
            Console.WriteLine("\nNavigation:");
            Console.WriteLine("← = Forrige side");
            Console.WriteLine("→ = Næste side");
            Console.WriteLine("↑ = Første side");
            Console.WriteLine("↓ = Sidste side");
            Console.WriteLine("ESC = Afslut");

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    currentPage = 1;
                    break;
                case ConsoleKey.RightArrow:
                    if (currentPage < totalPages)
                        currentPage++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentPage > 1)
                        currentPage--;
                    break;
                case ConsoleKey.DownArrow:
                    currentPage = totalPages;
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        }
    }
    #endregion
} 