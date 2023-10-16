using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionData;
using SistemaGestionEntities;



namespace SistemaGestionBussiness
{
    public static class ProductoBussines
    {
        public static string GetProducto(int productoID)
        {
            return ProductData.Get(productoID);
        }

        public static ProductoModel[] ListarProductos()
        {
            return ProductData.GetAllProducts();
        }

        public static string CrearProducto(ProductoModel newProducto)
        {
            return ProductData.CreateProduct(newProducto);
        }

        public static string ModificarProducto(ProductoModel modifiedProduct)
        {
            return ProductData.UpdateProduct(modifiedProduct);
        }

        public static string EliminarProducto(int productoID)
        {
            return ProductData.DeleteProduct(productoID);
        }


    }
}
