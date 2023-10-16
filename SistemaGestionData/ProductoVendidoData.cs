using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public static class ProductoVendidoData
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=sandbox_db;Integrated Security=False;User ID=sa;Password=sa;";

        public static string Get(int IDProductoVendido)
        {
            ProductoVendido pv = new ProductoVendido();
            string com = "SELECT ID, Stock, IdProducto, IdVenta from ProductoVendido WHERE ID =" + IDProductoVendido;

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
                                pv.ID = Convert.ToInt32(reader["ID"]);
                                pv.ProductStock = Convert.ToInt32(reader["Stock"]);
                                pv.IDProducto = Convert.ToInt32(reader["idProducto"]);
                                pv.IDVenta = Convert.ToInt32(reader["IdVenta"]);
                            }
                        }
                    };

                    conn.Close();

                    if (pv == null)
                    {
                        return($"Producto vendido con ID {IDProductoVendido} no encontrado");
                    }

                    return($"Producto vendido con ID {IDProductoVendido} traído exitosamente");
                }
                catch (Exception ex)
                {
                    return "Error"+ ex;
                    throw;
                }
            }
        }

        public static ProductoVendido[] GetAllProductsVendidos()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            string com = "SELECT ID, Stock, IdProducto, IdVenta FROM ProductoVendido";

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
                            ProductoVendido pv = new ProductoVendido
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                ProductStock = Convert.ToInt32(reader["Stock"]),
                                IDProducto = Convert.ToInt32(reader["idProducto"]),
                                IDVenta = Convert.ToInt32(reader["IdVenta"])
                            };

                            productosVendidos.Add(pv);
                            
                        }
                    }

                    conn.Close();

                    if (productosVendidos.Count == 0)
                    {
                        Console.WriteLine("No se encontraron productos vendidos");
                    }

                    return productosVendidos.ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public static string ModifyProductVendido(ProductoVendido modifiedProductVendido)
        {
            string updateCommand = "UPDATE ProductoVendido SET Stock = @Stock, IdProducto = @IdProducto, IdVenta = @IdVenta WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateCommand, conn);
                cmd.Parameters.AddWithValue("@ID", modifiedProductVendido.ID);
                cmd.Parameters.AddWithValue("@Stock", modifiedProductVendido.ProductStock);
                cmd.Parameters.AddWithValue("@IdProducto", modifiedProductVendido.IDProducto);
                cmd.Parameters.AddWithValue("@IdVenta", modifiedProductVendido.IDVenta);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected == 0)
                    {
                        return($"Producto vendido con ID {modifiedProductVendido.ID} no encontrado");
                    }

                    return($"Producto vendido con ID {modifiedProductVendido.ID} modificado exitosamente");
                }
                catch (Exception ex)
                {
                    return"Error" + ex;
                }
            }
        }

        public static string CreateProductVendido(ProductoVendido newProductVendido)
        {
            string insertCommand = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@Stock, @IdProducto, @IdVenta); SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(insertCommand, conn);
                cmd.Parameters.AddWithValue("@Stock", newProductVendido.ProductStock);
                cmd.Parameters.AddWithValue("@IdProducto", newProductVendido.IDProducto);
                cmd.Parameters.AddWithValue("@IdVenta", newProductVendido.IDVenta);

                try
                {
                    conn.Open();
                    var newProductId = cmd.ExecuteScalar();
                    conn.Close();

                    if (newProductId != null)
                    {
                        return($"Producto vendido creado con ID: {newProductId}");
                    }

                    return("No se pudo crear el producto vendido");
                }
                catch (Exception ex)
                {
                    return "Error" + ex;
                    throw;
                }
            }
        }

        public static string DeleteProductVendido(int id)
        {
            string deleteCommand = "DELETE FROM ProductoVendido WHERE ID = @ID";

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
                        return($"Producto vendido con ID {id} eliminado exitosamente");
                    }
                    else
                    {
                        return($"Producto vendido con ID {id} no encontrado");
                    }
                }
                catch (Exception ex)
                {
                    return "Error" + ex;
                }
            }
        }
    }

}
