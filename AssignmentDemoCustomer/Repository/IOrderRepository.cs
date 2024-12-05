using AssignmentDemoCustomer.Model;
using AssignmentDemoCustomer.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDemoCustomer.Repository
{
    public interface IOrderRepository
    {
        #region 1 - Get all employees from DB
        public Task<ActionResult<IEnumerable<OrderTable>>> GetTblOrder();
        #endregion

        #region 2- ViewModel
        public Task<ActionResult<IEnumerable<ItemTableViewModel>>> GetItemTableViewModel();
        #endregion

        #region 3-Get an Employee based on Id
        public Task<ActionResult<OrderTable>> GetTblOrderById(int id);

        #endregion
        #region 4-Insert an Employee -Return Record
        public Task<ActionResult<OrderTable>> PostTblEmployeesReturnRecord(OrderTable tblEmployees);
        #endregion

        #region 5-Insert an Employee -Return ID
        public Task<ActionResult<int>> PostTblEmployeesReturnId(OrderTable tblEmployees);
        #endregion

        #region 6-Update an Employee with ID and employee
        public Task<ActionResult<OrderTable>> PutTblEmployees(int id, OrderTable tblEmployees);
        #endregion

        #region 7- Delete an Employee
        public JsonResult DeleteTblEmployees(int id);
        #endregion

        #region 8-Update an Employee with employee only
        public Task<ActionResult<IEnumerable<Customer>>> GetTblDepartment();
        #endregion

    }
}
