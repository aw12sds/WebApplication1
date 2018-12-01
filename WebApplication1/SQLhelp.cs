using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WebApplication1
{
    class SQLhelp
    {
        //private static readonly string connStr1 = "Data Source=10.15.1.252;Initial Catalog=";
        private static readonly string connStr1 = "Data Source=222.184.250.34,444;Initial Catalog=";

        //private static readonly string connStr = "Data Source=222.184.250.34,444;Initial Catalog=db_xiangmuguanli;user id=sa;password=zttZTT123";
        //private static readonly string connStr1 = "Data Source=222.184.250.34,444;Initial Catalog=db_office;user id=sa;password=zttZTT123";
        //private static readonly string connStr = "Data Source=10.15.1.252;Initial Catalog=db_xiangmuguanli;user id=sa;password=zttZTT123";
        //private static readonly string connStr1 = "Data Source=10.15.1.252;Initial Catalog=db_office;user id=sa;password=zttZTT123";
        public static DataSet GetDataTable(string sql, string db, CommandType type, params SqlParameter[] pars)
        {
            string connStr = connStr1 + db+";user id=sa;password=zttZTT123";
            SqlConnection conn = new SqlConnection(connStr);
            {
                using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                {
                    if (pars != null)
                    {
                        apter.SelectCommand.Parameters.AddRange(pars);
                    }
                    apter.SelectCommand.CommandType = type;
                    DataSet ds = new DataSet();
                    apter.Fill(ds);

                    return ds;

                }
            }
        }
        

       


        public static DataTable GetDataTable1(string sql, string db, CommandType type, params SqlParameter[] pars)
        {
            string connStr = connStr1 + db + ";user id=sa;password=zttZTT123";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                {
                    if (pars != null)
                    {
                        apter.SelectCommand.Parameters.AddRange(pars);
                    }
                    apter.SelectCommand.CommandType = type;
                    DataTable ds = new DataTable();
                    apter.Fill(ds);

                    return ds;

                }
            }
        }


        public static int ExecuteNonquery(string sql, string db, byte[] files, CommandType type,  params SqlParameter[] pars)
        {
            string connStr = connStr1 + db + ";user id=sa;password=zttZTT123";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //if (pars != null)
                    //{
                    //    cmd.Parameters.AddRange(pars);
                    //}
                    conn.Open();
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("@pic", SqlDbType.VarBinary).Value = files;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int ExecuteNonquerytuzhi(string sql, string db, byte[] tuzhifiles, CommandType type,  params SqlParameter[] pars)
        {
            string connStr = connStr1 + db + ";user id=sa;password=zttZTT123";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //if (pars != null)
                    //{
                    //    cmd.Parameters.AddRange(pars);
                    //}
                    conn.Open();
                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("@pictuzhi", SqlDbType.VarBinary).Value = tuzhifiles;
                    return cmd.ExecuteNonQuery();

                }
            }
        }
        public static int innn(string sql, string db, CommandType type, params SqlParameter[] pars)
        {
            string connStr = connStr1 + db + ";user id=sa;password=zttZTT123";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    cmd.CommandType = type;
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }

        }
        public static object ExecuteScalar(string sql, string db, CommandType type, params SqlParameter[] pars)
        {
            int a = 1;
            string connStr = connStr1 + db + ";user id=sa;password=zttZTT123";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    cmd.CommandType = type;
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
      
  
        public static byte[] duqu(string sql, string db, CommandType type, params SqlParameter[] pars)
        {
            string connStr = connStr1 + db + ";user id=sa;password=zttZTT123";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    cmd.CommandType = type;
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    byte[] mybuffer = null;
                    while (dr.Read())
                    {
                        mybuffer = (byte[])dr.GetValue(0);

                    }
                    return mybuffer;

                }
            }

        }

    }
}
