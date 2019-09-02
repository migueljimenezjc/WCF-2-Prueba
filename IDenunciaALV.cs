using SevicioWCF.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SevicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IDenunciaALV" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IDenunciaALV
    {
        [OperationContract]
        Respuesta InsertarDenuncia(Denuncia denuncia);

    }


}
