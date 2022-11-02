using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using ProConcepts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProConcepts.Controllers
{
    
    public class EmployeeController : Controller
    {
        public IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public ViewResult Index()
        {
            //identify file is modify. Server side code. Dummy code.
            Random random = new Random();
            ViewBag.imageversion = random.Next();
            List<Employee> employees = _repository.GetEmployees();
            return View(employees);
        }

        public ViewResult DeleteEmployee()
        {
            return View();
        }
       
        
        [HttpGet]
        public ViewResult AddEmployee()
        {   
            List<Department> dept = _repository.GetDepartments();
            SelectList selectLists = new SelectList(dept, "Id", "Name");
            ViewBag.Departments = selectLists;

            return View();
        }

        [HttpPost]
        public ViewResult AddEmployee(Employee employee)
        {
            List<Department> dept = _repository.GetDepartments();
            SelectList selectLists = new SelectList(dept, "Id", "Name");
            ViewBag.Departments = selectLists;
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {
                _repository.AddEmployee(employee);
                ViewBag.Message = "Data Inserted Successfully.";
            }

            return View();
        }

        [HttpGet]
        public ViewResult EditEmployee(int id)
        {
            List<Department> dept = _repository.GetDepartments();
            SelectList selectLists = new SelectList(dept, "Id", "Name");
            ViewBag.Departments = selectLists;
            Employee employee = _repository.GetEmployees().Where(x => x.Id == id).FirstOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ViewResult EditEmployee(Employee employee)
        {
            List<Department> dept = _repository.GetDepartments();
            SelectList selectLists = new SelectList(dept, "Id", "Name");
            ViewBag.Departments = selectLists;
            if (ModelState.IsValid)
            {
                bool flag = _repository.EditEmployee(employee);
                if (flag)
                {
                    @ViewBag.Message = "Data updated successfully";
                }
                else
                {
                    @ViewBag.Message = "Internal Error. Not able to update data.";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            bool flag = _repository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
            
        }


    }
}
