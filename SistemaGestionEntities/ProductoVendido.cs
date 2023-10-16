namespace SistemaGestionEntities
{
    public class ProductoVendido
    {
        private int id;
        private int idProducto;
        private int productStock;
        private int idVenta;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int IDProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        public int ProductStock
        {
            get { return productStock; }
            set { productStock = value; }
        }
        public int IDVenta
        {
            get { return idVenta; }
            set { idVenta = value; }
        }

    }


}