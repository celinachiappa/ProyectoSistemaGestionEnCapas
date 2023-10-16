
namespace SistemaGestionEntities
{
    public class ProductoModel
    {
        private int productID;
        private string productDescription;
        private int productCost;
        private int productPrice;
        private int prductStock;
        private int idUser;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public string ProductDescription
        {
            get { return productDescription; }
            set { productDescription = value; }
        }

        public int ProductCost
        {
            get { return productCost; }
            set { productCost = value; }
        }

        public int ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }

        public int ProductStock
        {
            get { return prductStock; }
            set { prductStock = value; }
        }
        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }
    }

}