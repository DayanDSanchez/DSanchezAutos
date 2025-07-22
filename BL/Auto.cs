using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Auto
    {
        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                //intenta
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    //destruir cuando se ejecute todo
                    string query = "AutoGetAll";

                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = context;
                    command.CommandText = query;

                    DataTable dataTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    //inicializa lista
                    result.Objects = new List<object>();

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Auto auto = new ML.Auto();
                            auto.Version = new ML.Version();

                            auto.IdAuto = Convert.ToInt16(row[0]);
                            auto.Color = Convert.ToString(row[1]);
                            auto.Asientos = Convert.ToInt16(row[2]);
                            auto.Puertas = Convert.ToInt16(row[3]);
                            auto.Version.IdVersion = Convert.ToInt16(row[4]);

                            //agregamos materia a la lista
                            result.Objects.Add(auto); //Boxing
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result Add(ML.Auto auto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "AutoAdd";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Color", auto.Color);
                    cmd.Parameters.AddWithValue("@Asientos", auto.Asientos);
                    cmd.Parameters.AddWithValue("@Puertas", auto.Puertas);
                    cmd.Parameters.AddWithValue("@IdVersion", auto.Version.IdVersion);

                    cmd.Parameters.Add("@IdAuto", SqlDbType.Int);
                    cmd.Parameters["@IdAuto"].Direction = ParameterDirection.Output;

                    context.Open();

                    int i = cmd.ExecuteNonQuery();

                    //byte, short, int, long

                    int idAuto = Convert.ToInt32(cmd.Parameters["@IdAuto"].Value);

                    result.Object = idAuto;

                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.exception = ex;
            }

            return result;
        }

        public static ML.Result AddImagen(int idAuto, byte[] imagen)
        {

        }
    }
}
