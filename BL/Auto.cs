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
    }
}
