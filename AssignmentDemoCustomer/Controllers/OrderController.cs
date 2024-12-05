using AssignmentDemoCustomer.Model;
using AssignmentDemoCustomer.Repository;
using AssignmentDemoCustomer.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmentDemoCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>
        private readonly IOrderRepository _repository;
        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        /*
           // GET: api/<Employee>
           [HttpGet]
           public IEnumerable<string> Get()
           {
               return new string[] { "Thanya", "Ahalya" ,"Karthika"};
           }

           // GET api/<Employee>/5
           [HttpGet("{id}")]
           public string Get(int id)
           {
               return "value " + id;
           }*/


        #region 1  Get All Employees Search
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTable>>> GetViewModelEmployees()
        {
            var employees = await _repository.GetTblOrder();
            if (employees == null)
            {
                return NotFound("No Employee Found");
            }
            return Ok(employees);
        }
        #endregion

        #region 2  Get All View Model Search
        [HttpGet("vm")]
        public async Task<ActionResult<IEnumerable<ItemTableViewModel>>> GetAllOrderViewModel()
        {
            var employees = await _repository.GetItemTableViewModel();
            if (employees == null)
            {
                return NotFound("No Employee Found");
            }
            return Ok(employees);
        }
        #endregion

        #region 3 Get Employee Search By ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTable>> GetOrderById(int id)
        {
            var employee = await _repository.GetTblOrderById(id);
            if (employee == null)
            {
                return NotFound("No Employee Found");
            }
            return Ok(employee);
        }
        #endregion

        #region 4-Insert an Employee -Return Record
        [HttpPost]
        public async Task<ActionResult<OrderTable>> InsertTblEmployeeReturnRecord(OrderTable employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = await _repository.PostTblEmployeesReturnRecord(employee);
                if (newEmployee != null)
                {
                    return Ok(newEmployee);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion


        #region 5-Insert an Employee -Return ID
        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertTblEmployeeReturnId(OrderTable employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployeeId = await _repository.PostTblEmployeesReturnId(employee);
                if (newEmployeeId != null)
                {
                    return Ok(newEmployeeId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  6-Update an Employee with ID and employee
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderTable>> UpdateblEmployeesReturnRecord(int id, OrderTable employee)
        {
            if (ModelState.IsValid)
            {
                //update a record and return as an object named employee
                var updatemployee = await _repository.PutTblEmployees(id, employee);
                if (updatemployee != null)
                {
                    return Ok(updatemployee);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        #endregion


        #region 7- Delete an Employee
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployees(int id)
        {
            try
            {
                //update a record and return as an object named employee
                var result = _repository.DeleteTblEmployees(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        success = true,
                        Message = "Employee Deleted Successfully.."
                    });
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = true,
                    Message = "Employee Deleted Successfully.."
                });
            }

        }
        #endregion


        #region 8-Update the Department only
        [HttpGet("v2")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllDepartments()
        {
            var departments = await _repository.GetTblDepartment();

            if (departments == null)
            {
                return NotFound("No department Found");
            }

            return Ok(departments);
        }


        #endregion

    }
}
