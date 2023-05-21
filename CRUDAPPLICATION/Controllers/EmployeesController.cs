using CRUDAPPLICATION.DATA;
using CRUDAPPLICATION.Models;
using CRUDAPPLICATION.Models.DEMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPPLICATION.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;
        public EmployeesController(MVCDemoDbContext mvcDemoDbContext) { 
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        public async Task<IActionResult> Index() {
           var employee = await mvcDemoDbContext.Employees.ToListAsync();

           return View(employee);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            Employee emp = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
            };

            await mvcDemoDbContext.Employees.AddAsync(emp);
            await mvcDemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async  Task<IActionResult> View(Guid Id) {
           var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);
           
            if (employee != null)
            {
                var modelView = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };

                return await Task.Run(() => View("View", modelView));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async  Task<IActionResult> View(UpdateEmployeeViewModel model) {

           var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


    }
}
