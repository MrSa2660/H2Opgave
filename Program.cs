using System;
using System.Threading.Tasks;

/// <summary>
/// Hovedprogrammet for Sydvest-Bo administrationssystem.
/// Dette er indgangspunktet for applikationen.
/// </summary>
class Program
{
    public static string connectionString = "Data Source=localhost;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=True;Encrypt=True;Trust Server Certificate=True;Command Timeout=0";
    /// <summary>
    /// Hovedmetoden der starter applikationen.
    /// Initialiserer menusystemet og håndterer overordnede fejl.
    /// </summary>
    static async Task Main(string[] args)
    {
        try
        {
            Console.Title = "Sydvest-Bo Administrationssystem";
            
            // Sæt console encoding til at håndtere danske tegn
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            // Vis velkomstbesked
            Console.WriteLine("Velkommen til Sydvest-Bo Administrationssystem");
            Console.WriteLine("Indlæser...");
            Console.WriteLine();

            // Test database forbindelse
            try
            {
                // TODO: Implementer database connection test
                await Task.Delay(1000); // Simulerer database check
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl ved forbindelse til database:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("\nTryk på en tast for at afslutte...");
                Console.ReadKey();
                return;
            }

            // Start menusystemet
            var menu = new Menu();
            await menu.VisHovedMenu();
        }
        catch (Exception ex)
        {
            // Håndter uventede fejl
            Console.Clear();
            Console.WriteLine("Der opstod en uventet fejl i programmet:");
            Console.WriteLine(ex.Message);
            Console.WriteLine("\nDetaljer:");
            Console.WriteLine(ex.ToString());
            Console.WriteLine("\nProgrammet vil nu afslutte. Tryk på en tast...");
            Console.ReadKey();
        }
    }
}
