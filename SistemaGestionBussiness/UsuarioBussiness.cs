using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness
{
    public static class UsuarioBussiness
    {
        public static Usuario GetUsuario(int IDUsuario)
        {
            return UserData.Get(IDUsuario);
        }

        public static Usuario[] ListarUsuarios()
        {
            return UserData.GetAll();
        }

        public static string ModificarUsuario(Usuario modifiedUser)
        {
            return UserData.ModifyUser(modifiedUser);
        }

        public static string CrearUsuario(Usuario usuario)
        {
            return UserData.CreateUser(usuario);
        }

        public static string EliminarUsuario(int UserID)
        {
            return UserData.DeleteUser(UserID);
        }
    }
}
