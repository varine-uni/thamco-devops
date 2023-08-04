using System.Collections.Generic;
using ThAmCo.Web.Models;

namespace ThAmCo.Web.Services
{
    public interface IEmployeeService
    {
        void ViewProductStockLevels();
        void CreatePurchaseRequest(List<Product> prices);
        void SetResellPrice(float resellPrice);
        void ViewPendingOrders();
        void ViewCustomerInvoices();
        void ViewCustomerOrderHistory();
        void DeleteCustomerAccount();
    }
}

