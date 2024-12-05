using AssignmentDemoCustomer.Model;
using AssignmentDemoCustomer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDemoCustomer.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly CustomerAssignmentContext _context;
 
        //DI - Constructor Injection
        public OrderRepository(CustomerAssignmentContext context)
        {
            _context = context;
        }
        #region Get All Employees

        public async Task<ActionResult<IEnumerable<OrderTable>>> GetTblOrder()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.OrderTables.Include(order => order.Customer).Include(order => order.OrderItem).ToListAsync();
                }
                //if return an empty List
                return new List<OrderTable>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion 

        #region Search By Id
        public async Task<ActionResult<OrderTable>> GetTblOrderById(int id)
        {
            try
            {
                if (_context != null)
                {
                    //Find Employee By Id
                    var employee = await _context.OrderTables.Include(emp => emp.Customer).Include(order => order.OrderItem).FirstOrDefaultAsync(e => e.OrderId == id);
                    return employee;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region List ViewModel Records
        public async Task<ActionResult<IEnumerable<ItemTableViewModel>>> GetItemTableViewModel()       
        {
            try
            {
                if (_context != null)
                {
                    return await (from o in _context.OrderTables
                                  join c in _context.Customers on o.CustomerId equals c.CustomerId
                                  join oi in _context.OrderItems on o.OrderItemId equals oi.OrderItemId
                                  join i in _context.Items on oi.ItemId equals i.ItemId
                                  select new ItemTableViewModel
                                  {
                                      CustomerId = c.CustomerId,
                                      CustomerName = c.CustomerName,
                                      ItemName = i.ItemName,
                                      Price = i.Price,
                                      Quantity = oi.Quantity,
                                      OrderDate = o.OrderDate
                                  }).ToListAsync();

                }
                //if return an empty List
                return new List<ItemTableViewModel>();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region 4-Insert an Employee -Return Record
        public async Task<ActionResult<OrderTable>> PostTblEmployeesReturnRecord(OrderTable employees)
        {
            try
            {
                //Object is null or not check
                if (employees == null)
                {
                    throw new ArgumentNullException(nameof(employees), "Employee Data Is Null");
                }
                //ensure the content is not Null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database content Is Not Initialized");
                }
                await _context.OrderTables.AddAsync(employees);
                //Save Changes
                await _context.SaveChangesAsync();
                //Retrive the Employee with the related department
                var employeeWithDepartment = await _context.OrderTables.Include(e => e.OrderItem).FirstOrDefaultAsync(e => e.OrderId == employees.OrderId);

                //Return The  added Record
                return employeeWithDepartment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region 5-Insert an Employee -Return ID
        public async Task<ActionResult<int>> PostTblEmployeesReturnId(OrderTable employees)
        {
            try
            {
                //Object is null or not check
                if (employees == null)
                {
                    throw new ArgumentNullException(nameof(employees), "Employee Data Is Null");
                }
                //ensure the content is not Null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database content Is Not Initialized");
                }
                await _context.OrderTables.AddAsync(employees);
                //Save Changes
                var changeRecords = await _context.SaveChangesAsync();
                //Retrive the Employee with the related department
                if (changeRecords > 0)
                {
                    return employees.OrderId;
                }
                else
                {
                    throw new Exception("Faild to save employee Records to the database. ");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 6-Update an Employee with ID and employee
        public async Task<ActionResult<OrderTable>> PutTblEmployees(int id, OrderTable employees)
        {
            try
            {
                //Object is null or not check
                if (employees == null)
                {
                    throw new ArgumentNullException(nameof(employees), "Employee Data Is Null");
                }
                //ensure the content is not Null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database content Is Not Initialized");
                }
                //Find Employee
                var existingEmployee = await _context.OrderTables.Include(
                    order => order.Customer).Include(order => order.OrderItem).
                    FirstOrDefaultAsync(order => order.OrderId == employees.OrderId);
                if (existingEmployee == null)
                {
                    return null;
                }
                //Mapp Values With Fields 
                existingEmployee.OrderDate = employees.OrderDate;
                existingEmployee.CustomerId = employees.CustomerId;
                existingEmployee.OrderItemId = employees.OrderItemId;
                                    
                //Save Changes
                await _context.SaveChangesAsync();
                //Retrive the Employee with the related department
                var employeeWithDepartment = await _context.OrderTables.
                    Include(e => e.OrderItem).FirstOrDefaultAsync(e => e.OrderId == employees.OrderId);

                return employeeWithDepartment;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 7- Delete an Employee
        public JsonResult DeleteTblEmployees(int id)
        {
            try
            {
                //Object is null or not check
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        Message = "Invalid Employee Id"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //ensure the content is not Null
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        Message = "Db Not Found"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                //Find Employee
                var existingEmployee = _context.OrderTables.Find(id);
                if (existingEmployee == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        Message = "Employee Not Found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                //Remove Employee
                _context.OrderTables.Remove(existingEmployee);

                _context.SaveChangesAsync();
                //Retrive the Employee with the related department
                return new JsonResult(new
                {
                    success = true,
                    Message = "Employee Deleted Successfully.."
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };


            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    Message = "Database Not Found"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion

        #region 8-Update an Employee with employee only

        public async Task<ActionResult<IEnumerable<Customer>>> GetTblDepartment()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Customers.ToListAsync();
                }
                //if return an empty List
                return new List<Customer>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
