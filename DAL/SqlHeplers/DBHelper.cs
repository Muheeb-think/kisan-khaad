using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SqlHeplers
{
    public interface IDBHelpers
    {
        public DataTable ExecuteDataTable(string CommandName, SqlParameter[] parameters);
        public DataSet ExecuteDataSet(string CommandName, SqlParameter[] parameters);
        public int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters);
        public int ExecuteInsert(string CommandName, SqlParameter[] parameters);
       
    }
    public class DBHelper : IDBHelpers
    {
        private readonly string? _ConStr;

        public DBHelper(IConfiguration configuration)
        {
            _ConStr = configuration.GetConnectionString("ConStr");
        }
        public DataTable ExecuteDataTable(string CommandName, SqlParameter[] param)
        {
            if (string.IsNullOrWhiteSpace(CommandName))
            {
                throw new ArgumentException("Cannot be empty", nameof(CommandName));
            }
            using (SqlConnection conn = new SqlConnection(_ConStr))
            {
                SqlCommand cmd = new SqlCommand(CommandName, conn);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.CommandTimeout = 300;
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            cmd.Parameters.Add(param[i]);
                        }
                    }

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                    }
                    return dt;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Execption from db:" + ex.Message);
                }
                finally
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    conn.Close();
                }
            }
        }
        public DataSet ExecuteDataSet(string CommandName, SqlParameter[] param)
        {
            if (string.IsNullOrWhiteSpace(CommandName))
            {
                throw new ArgumentException("Cannot be empty", nameof(CommandName));
            }
            using (SqlConnection conn = new SqlConnection(_ConStr))
            {
                SqlCommand cmd = new SqlCommand(CommandName, conn);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure; cmd.CommandTimeout = 300;
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            cmd.Parameters.Add(param[i]);
                        }
                    }

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                    return ds;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Execption from db:" + ex.Message);
                }
                finally
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    conn.Close();
                }
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            
            using (SqlConnection connection = new SqlConnection(_ConStr))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType; command.CommandTimeout = 300;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
        public int ExecuteInsert(string commandName, SqlParameter[] param)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentException("Cannot be empty", nameof(commandName));

            using (SqlConnection conn = new SqlConnection(_ConStr))
            using (SqlCommand cmd = new SqlCommand(commandName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure; cmd.CommandTimeout = 300;

                if (param != null)
                    cmd.Parameters.AddRange(param);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

       






    }
}
