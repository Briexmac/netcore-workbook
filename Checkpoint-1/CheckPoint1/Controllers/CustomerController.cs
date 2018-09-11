using Spatastic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Spatastic.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _repository.AddCustomer(customer);
            return View("Index", _repository.Customers);
        }

        public IActionResult Delete(Customer customer)
        {
            var itemToRemove = _repository.Customers.Single(r => r.Id == customer.Id);
            _repository.Customers.Remove(itemToRemove);
            return View("Index", _repository.Customers);
        }
    }
}