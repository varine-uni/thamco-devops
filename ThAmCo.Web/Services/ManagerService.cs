namespace ThAmCo.Web.Services
{
    public class ManagerService
    {
        private readonly EmployeeService _employeeService;

        public ManagerService(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void AddStaffAccount()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteStaffAccount()
        {
            throw new System.NotImplementedException();
        }

        public void ApprovePurchaseRequest()
        {
            throw new System.NotImplementedException();
        }

        public void DenyPurchaseRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}
