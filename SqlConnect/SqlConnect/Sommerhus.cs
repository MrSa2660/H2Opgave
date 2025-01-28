using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {
    public class Sommerhus {
        private int SommerhusId;
        public string Adresse { get; set; }
        public int BasePris { get; set; }
        public int EjerId { get; set; }
        public int OmrådeId { get; set; }
        public int OpsynsmandId { get; set; }
        public string Klassificering { get; set; }
        public int AntalSenge { get; set; }
        public Dictionary<string, decimal> SæsonPriser { get; set; } = new Dictionary<string, decimal>();

        public Sommerhus() {
            SæsonPriser = new Dictionary<string, decimal>();
        }
    }
}
