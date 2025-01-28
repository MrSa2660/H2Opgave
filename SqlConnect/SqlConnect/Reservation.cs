using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnect {
    public class Reservation {
        private int ReservationId;
        public int SommerhusId { get; set; }
        public int KundeId { get; set; }
        public DateTime StartDato { get; set; }
        public DateTime SlutDato { get; set; }
    }
}
