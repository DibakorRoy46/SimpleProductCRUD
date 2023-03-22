using SimpleProductCRUD.Models;
using SimpleProductCRUD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleProductCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CustomerService customerService = new CustomerService();

        // GET: Employee
        public async Task<ActionResult> Index()
        {
            var customers =await customerService.GetCustomers();
            return View(customers);
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            var employee =await customerService.GetCustomerById(id);
            return View(employee);
        }

        // GET: Employee/Create
        public  ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await customerService.InsertCustomer(customer);
                    return RedirectToAction("Index");
                }
                return View("Create", customer);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer =await customerService.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)
        {
            try
            {
                await customerService.UpdateCustomer(customer);
                return RedirectToAction("Index", "Employee");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customers = await customerService.GetCustomerById(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> Deletes(Guid id)
        {
            try
            {
                await customerService.DeleteCustomer(id);
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }
    }
}