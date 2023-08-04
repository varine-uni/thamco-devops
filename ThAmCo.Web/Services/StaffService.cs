namespace ThAmCo.Web.Services
{
    public class StaffService
    {
        private readonly EmployeeService _employeeService;

        public StaffService(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
    }
}
