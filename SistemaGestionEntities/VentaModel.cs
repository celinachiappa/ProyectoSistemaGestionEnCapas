namespace SistemaGestionEntities
{
    public class Venta
    {
        private int id;
        private string comments;
        private int idUsuario;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
    }

}
   