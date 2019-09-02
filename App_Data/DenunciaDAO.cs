using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SevicioWCF.App_Code
{
    public class DenunciaDAO : BaseDAO
    {
        public DenunciaDAO(ConnectionStringSettings connStr) : base(connStr)
        {

        }

        public DataSet Insertar(Denuncia denuncia)
        {
            DbCommand cmd = CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Rem_InsertarDenunciaAnonima";
            cmd.Parameters.Add(AddWithValue("@fecha", denuncia.fecha_suceso, DbType.String));
            cmd.Parameters.Add(AddWithValue("@lugar", denuncia.lugar, DbType.String));
            cmd.Parameters.Add(AddWithValue("@involucrado", denuncia.involucrado, DbType.String));
            cmd.Parameters.Add(AddWithValue("@trabaja_dt", denuncia.trabaja_dt, DbType.Boolean));
            cmd.Parameters.Add(AddWithValue("@prioridad", denuncia.prioridad, DbType.String));
            cmd.Parameters.Add(AddWithValue("@tipo", denuncia.tipo, DbType.String));
            cmd.Parameters.Add(AddWithValue("@hechos", denuncia.hechos, DbType.String));

            if (cmd == null)
                return null;
            else
                return GetDataSet(cmd);
        }
    }
}