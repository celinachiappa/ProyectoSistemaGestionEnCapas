using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaGestionData;
using SistemaGestionEntities;


public static class UserData
{
    private static readonly string connectionString = "Data Source=.;Initial Catalog=sandbox_db;Integrated Security=False;User ID=sa;Password=sa;";

    public static Usuario Get(int IDUsuario)
    {
        Usuario u = new Usuario();
        string com = "Select ID, Nombre, Apellido, NombreUsuario, Contraseña, Mail from Usuario Where ID =" + IDUsuario;

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
                            u.UserID = Convert.ToInt32(reader["ID"]);
                            u.Name = reader["Nombre"].ToString();
                            u.Lastname = reader["Apellido"].ToString();
                            u.Username = reader["NombreUsuario"].ToString();
                            u.Email = reader["Mail"].ToString();
                            u.Password = reader["Contraseña"].ToString();
                        }
                    }
                };

                conn.Close();

                if (u.UserID == 0)
                {
                    Console.WriteLine("Usuario no encontrado"); // Devuelve un 404 si no se encuentra el usuario.
                }

                return u;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }

    public static Usuario[] GetAll()
    {
        List<Usuario> usersList = new List<Usuario>();
        string com = "Select ID, Nombre, Apellido, NombreUsuario, Contraseña, Mail from Usuario";

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
                            var usuario = new Usuario();
                            usuario.UserID = Convert.ToInt32(reader["ID"]);
                            usuario.Name = reader["Nombre"].ToString();
                            usuario.Lastname = reader["Apellido"].ToString();
                            usuario.Username = reader["NombreUsuario"].ToString();
                            usuario.Email = reader["Mail"].ToString();
                            usuario.Password = reader["Contraseña"].ToString();

                            usersList.Add(usuario);
                        }
                    }
                };

                conn.Close();

                return usersList.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }

    public static string ModifyUser(Usuario modifiedUser)
    {

        string mensajeExitoso = "Usuario modificado exitosamente.";
        string mensajeFallido = "Usuario no encontrado.";

        string updateQuery = "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, NombreUsuario = @NombreUsuario, Contraseña = @Contraseña, Mail = @Mail WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(updateQuery, conn);

            cmd.Parameters.AddWithValue("@ID", modifiedUser.UserID);
            cmd.Parameters.AddWithValue("@Nombre", modifiedUser.Name);
            cmd.Parameters.AddWithValue("@Apellido", modifiedUser.Lastname);
            cmd.Parameters.AddWithValue("@NombreUsuario", modifiedUser.Username);
            cmd.Parameters.AddWithValue("@Contraseña", modifiedUser.Password);
            cmd.Parameters.AddWithValue("@Mail", modifiedUser.Email);

                    //private int userID;
                    //private string name;
                    //private string lastname;
                    //private string username;
                    //private string password;
                    //private string email;


            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return mensajeExitoso;
                }
                else
                {
                    return mensajeFallido;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Error");
                return "No se pudo modificar el usuario " + " - " + ex.Message; 
            }
        }
    }

    public static string CreateUser(Usuario newUser)
    {
        string mensajeExitoso = "Usuario creado exitosamente.";
        string mensajeFallido = "No se pudo crear el usuario.";

        string insertQuery = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail)";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(insertQuery, conn);

            cmd.Parameters.AddWithValue("@Nombre", newUser.Name);
            cmd.Parameters.AddWithValue("@Apellido", newUser.Lastname);
            cmd.Parameters.AddWithValue("@NombreUsuario", newUser.Username);
            cmd.Parameters.AddWithValue("@Contraseña", newUser.Password);
            cmd.Parameters.AddWithValue("@Mail", newUser.Email);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return mensajeExitoso;
                }
                else
                {
                    return mensajeFallido;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Error.";
            }
        }
    }

    public static string DeleteUser(int userID)
    {
        string deleteQuery = "DELETE FROM Usuario WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(deleteQuery, conn);

            cmd.Parameters.AddWithValue("@ID", userID);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return("Usuario eliminado exitosamente.");
                }
                else
                {
                    return("Usuario no encontrado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
               return ("Error");
            }
        }
    }
}
