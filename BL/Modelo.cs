using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Modelo
    {
        public static ML.Result ModeloGetByIdMarca(int IdMarca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "ModeloGetByIdMarca";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = query;

                    //1
                    cmd.Parameters.AddWithValue("@IdMarca", IdMarca);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Modelo modelo = new ML.Modelo();
                            modelo.IdModelo = Convert.ToInt16(row[0]);
                            modelo.Nombre = Convert.ToString(row[1]);

                            //agregamos materia a la lista
                            result.Objects.Add(modelo); //Boxing
                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.exception = ex;
            }
            return result;
        }
    }
}
