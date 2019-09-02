using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevicioWCF.App_Code
{
    [DataContract]
    public class Denuncia
    {
        [DataMember]
        public string fecha_suceso { get; set; }

        [DataMember]
        public string lugar { get; set; }

        [DataMember]
        public string involucrado { get; set; }

        [DataMember]
        public bool trabaja_dt { get; set; }

        [DataMember]
        public string prioridad { get; set; }

        [DataMember]
        public string tipo { get; set; }

        [DataMember]
        public string hechos { get; set; }

    }
}