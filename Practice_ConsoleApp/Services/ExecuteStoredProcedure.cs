using ConsoleApp.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ConsoleApp.Services
{
    class ExecuteStoredProcedure : IExecuteStoredProcedure{
        public DataSet CallStoredProcedure(string StoredProcedure, Dictionary<string, object>? Parameters)
        {
            String ConnectionString = "Server=localhost\\MYNEWINSTANCE;Database=Student_DB;User Id=sa;Password=Mehar_2512;TrustServerCertificate=True;";
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = StoredProcedure;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = sqlConnection;
                    if(Parameters != null){
                        foreach (var para in Parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(para.Key, para.Value?? DBNull.Value);
                        }
                    }
                    // sqlCommand.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);
                    sqlConnection.Close();
                    return ds;
        }
    }
}