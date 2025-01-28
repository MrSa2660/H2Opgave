using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {

    public class Ejer {
        private int EjerId;
        public string Navn { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresse { get; set; }
        public List<Sommerhus> Sommerhuse { get; set; } = new List<Sommerhus>();
        public DateTime KontraktStartDato { get; set; }
        public DateTime KontraktSlutDato { get; set; }

        // Konstruktør
        public Ejer() {
            Sommerhuse = new List<Sommerhus>();
        }

        // Offentlige metoder til at læse og sætte EjerId:
        public int GetEjerId() {
            return EjerId;
        }

        public void SetEjerId(int value) {
            EjerId = value;
        }
    }

}

