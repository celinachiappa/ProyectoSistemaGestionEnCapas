using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public static class ProductData
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=sandbox_db;Integrated Security=False;User ID=sa;Password=sa;";

        public static string Get(int IDProducto)
        {
            ProductoModel p = new ProductoModel();
            string com = "SELECT ID, Descripciones, Costo, PrecioVenta, Stock, IdUsuario FROM Producto WHERE ID =" + IDProducto;

            using SqlConnection conn = new SqlConnection(connectionString);
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
                                p.ProductID = Convert.ToInt32(reader["ID"]);
                                p.ProductDescription = reader["Descripciones"].ToString();
                                p.ProductCost = Convert.ToInt32(reader["Costo"]);
                                p.ProductCost = Convert.ToInt32(reader["PrecioVenta"]);
                                p.ProductStock = Convert.ToInt32(reader["Stock"]);
                                p.IdUser = Convert.ToInt32(reader["idUsuario"]);
                            }
                        }
                    };

                    conn.Close();

                    if (p.ProductID == 0)
                    {
                        return("Error"); // Devuelve erro si no se encuentra el usuario.
                    }

                    return p.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return ("Error" + ex);
                }
            }
        }

        public static ProductoModel[] GetAllProducts()
        {
            List<ProductoModel> productos = new List<ProductoModel>();
            string com = "SELECT ID, Descripciones, Costo, PrecioVenta, Stock, IdUsuario FROM Producto"; // Selecciona todos los productos.

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
                            ProductoModel p = new ProductoModel
                            {
                                ProductID = Convert.ToInt32(reader["ID"]),
                                ProductDescription = reader["Descripciones"].ToString(),
                                ProductCost = Convert.ToInt32(reader["Costo"]),
                                ProductPrice = Convert.ToInt32(reader["PrecioVenta"]),
                                ProductStock = Convert.ToInt32(reader["Stock"]),
                                IdUser = Convert.ToInt32(reader["IdUsuario"])
                            };

                            productos.Add(p);
                        }
                    }

                    conn.Close();

                    return productos.ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public static string UpdateProduct(ProductoModel updatedProduct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string updateQuery = "UPDATE Producto SET Descripciones = @Descripciones, Costo = @Costo, PrecioVenta = @PrecioVenta, Stock = @Stock, IdUsuario = @IdUsuario WHERE ID = @IDProducto";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@IDProducto", updatedProduct.ProductID);
                    cmd.Parameters.AddWithValue("@Descripciones", updatedProduct.ProductDescription);
                    cmd.Parameters.AddWithValue("@Costo", updatedProduct.ProductCost);
                    cmd.Parameters.AddWithValue("@PrecioVenta", updatedProduct.ProductPrice);
                    cmd.Parameters.AddWithValue("@Stock", updatedProduct.ProductStock);
                    cmd.Parameters.AddWithValue("@IdUsuario", updatedProduct.IdUser);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        conn.Close();
                        return("Producto actualizado correctamente" + updatedProduct);
                    }
                    else
                    {
                        conn.Close();
                        return("Error"); // Devuelve ERROR si el producto con el ID especificado no se encuentra.
                    }
                }
            }
        }

        public static string CreateProduct(ProductoModel newProduct)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Descripciones", newProduct.ProductDescription);
                    cmd.Parameters.AddWithValue("@Costo", newProduct.ProductCost);
                    cmd.Parameters.AddWithValue("@PrecioVenta", newProduct.ProductPrice);
                    cmd.Parameters.AddWithValue("@Stock", newProduct.ProductStock);
                    cmd.Parameters.AddWithValue("@IdUsuario", newProduct.IdUser);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        conn.Close();
                        return("Producto creado correctamente");
                    }
                    else
                    {
                        conn.Close();
                        return("No se pudo crear el producto"); 
                    }
                }
            }
        }

        public static string DeleteProduct(int IDProducto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM Producto WHERE ID = @IDProducto";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@IDProducto", IDProducto);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        conn.Close();
                        return("Producto eliminado correctamente");
                    }
                    else
                    {
                        conn.Close();
                        return("Error"); 
                    }
                }
            }
        }
    }
}
