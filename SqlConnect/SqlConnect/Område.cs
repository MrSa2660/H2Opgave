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
    }
}
