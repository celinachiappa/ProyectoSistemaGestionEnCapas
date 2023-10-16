using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaGestionEntities;


    public static class VentaData
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=sandbox_db;Integrated Security=False;User ID=sa;Password=sa;";

        public static string Get(int IDVenta)
        {
            Venta v = new Venta();
            string com = "SELECT ID, Comentarios, IdUsuario FROM Venta WHERE ID =" + IDVenta;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(com, conn);

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                v.ID = Convert.ToInt32(reader["ID"]);
                                v.Comments = reader["Comentarios"].ToString();
                                v.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            }
                        }
                    };

                    conn.Close();

                    if (v.ID == 0)
                    {
                        return("Venta no encontrada");
                    }

                    return "Venta encontrada:" + v;
                }
                catch (Exception ex)
                {
                    return "Error" + ex;
                }
            }
        }

        public static List<Venta> GetAll()
        {
            List<Venta> ventas = new List<Venta>();
            string com = "SELECT ID, Comentarios, IdUsuario FROM Venta";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(com, conn);

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Venta v = new Venta
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Comments = reader["Comentarios"].ToString(),
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"])
                            };

                            ventas.Add(v);
                        }
                    };

                    conn.Close();

                    if (ventas == null)
                    {
                        Console.WriteLine("Venta no encontrada");
                    }

                    return ventas;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public static string ModifyVenta(Venta modifiedSale)
        {
            List<Venta> ventas = new List<Venta>();
            string updateCommand = "UPDATE Venta SET Comentarios = @Comentarios, IdUsuario = @IdUsuario WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateCommand, conn);
                cmd.Parameters.AddWithValue("@ID", modifiedSale.ID);
                cmd.Parameters.AddWithValue("@Comentarios", modifiedSale.Comments);
                cmd.Parameters.AddWithValue("@IdUsuario", modifiedSale.IdUsuario);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected == 0)
                    {
                        return("Venta no encontrada");
                    }

                    return("Venta modificada exitosamente");
                }
                catch (Exception ex)
                {
                    return "Error" + ex;
                }
            }
        }

        public static string CrearVenta( Venta newSale)
        {
            string insertCommand = "INSERT INTO Venta (Comentarios, IdUsuario) VALUES (@Comentarios, @IdUsuario); SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(insertCommand, conn);
                cmd.Parameters.AddWithValue("@Comentarios", newSale.Comments);
                cmd.Parameters.AddWithValue("@IdUsuario", newSale.IdUsuario);

                try
                {
                    conn.Open();
                    var newSaleId = cmd.ExecuteScalar();
                    conn.Close();

                    if (newSaleId != null)
                    {
                        return($"Venta creada exitosamente con ID: {newSaleId}");
                    }

                    return("No se pudo crear la venta");
                }
                catch (Exception ex)
                {
                    return "Error" + ex;
                }
            }
        }

        public static string DeleteSale(int id)
        {
            string deleteCommand = "DELETE FROM Venta WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(deleteCommand, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        return($"Venta con ID {id} eliminada exitosamente");
                    }
                    else
                    {
                        return($"Venta con ID {id} no encontrada");
                    }
                }
                catch (Exception ex)
                {
                    return "Error"+ ex;
                }
            }
        }
    }


