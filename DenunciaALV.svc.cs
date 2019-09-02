using SevicioWCF.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SevicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "DenunciaALV" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione DenunciaALV.svc o DenunciaALV.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class DenunciaALV : IDenunciaALV
    {
        public Respuesta InsertarDenuncia(Denuncia denuncia)
        {
            Respuesta respuesta = new Respuesta();
            DenunciaDAO db = new DenunciaDAO(ConfigurationManager.ConnectionStrings["conStr"]);
            DataSet resultado = db.Insertar(denuncia);

            if (resultado == null)
                respuesta = new Respuesta() { Codigo = 404, Descripcion = "No se puede procesar la solicitud" };

            respuesta = new Respuesta() { Codigo = 0, Descripcion = "La denuncia se inserto exitosamente" };
            return respuesta;
        }
    }
}
