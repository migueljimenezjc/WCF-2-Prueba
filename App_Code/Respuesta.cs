using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevicioWCF.App_Code
{
    [DataContract]
    public class Respuesta
    {
        [DataMember]
        public int Codigo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }
    }
}