using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WsPortfolioExpress.Common.Entities;
using WsPortfolioExpress.Web.Context;
using WsPortfolioExpress.Web.Models;
using WsPortfolioExpress.Web.Services;

namespace WsPortfolioExpress.Web.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomersController> _logger;
        private readonly IWebHostEnvironment _environment;

        public CustomersController(ApplicationDbContext context, ILogger<CustomersController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            List<CustomerViewModel> models = new List<CustomerViewModel>();
            foreach (var item in _context.Customers.ToList())
            {
                CustomerViewModel? model = new CustomerViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    SurName = item.SurName,
                    Document = item.Document,
                    DocumentType = item.DocumentType,
                    Email = item.Email,
                    Imagen = item.Imagen,
                    CreateDate = item.CreateDate
                };
                models.Add(model);
            }

            GetUserLoginInfo(UserService.UserLogin);
            OnLoadHeaderComponent();
            return View(models);
        }

        public IActionResult AddEditCustomers(int itemId)
        {
            CustomerViewModel model = new CustomerViewModel();
            if (itemId > 0)
            {
                model = GetCustomerData(itemId);
            }
            return PartialView("_customerForm", model);
        }

        ///[HttpPost]
        public IActionResult CreateEditCustomer(int id)
        {
            if (id == 0)
            {
                var model = new CustomerViewModel();
                OnLoadHeaderComponent();
                return View("CreateEditCustomer", model);
            }
            else
            {
                var model = GetCustomerData(id);
                OnLoadHeaderComponent();
                return View("CreateEditCustomer", model);
            }
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    UpdateCustomer(model);
                }
                else
                {
                    AddCustomer(model);
                }
                SwalAlert("Registro Guardado Correctamente !!!", Enums.CommonEnums.NotifyType.success);
                return RedirectToAction("Index");
            }
            OnLoadHeaderComponent();
            return View("CreateEditCustomer", model);
        }

        public void AddCustomer(CustomerViewModel model)
        {
            try
            {
                Customer customer;
                string uniqueFileName = ProcessUploadedFile(model);
                model.Imagen = string.Format("/storage/customers/img/{0}", uniqueFileName);
                model.CreateDate = DateTime.Now;
                customer = new Customer()
                {
                    Name = model.Name,
                    SurName = model.SurName,
                    Document = model.Document,
                    DocumentType = model.DocumentType,
                    Email = model.Email,
                    Imagen = model.Imagen,
                    CreateDate = model.CreateDate
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateCustomer(CustomerViewModel model)
        {
            try
            {
                Customer customer;
                if (model.ImageFile != null)
                {
                    string uniqueFileName = ProcessUploadedFile(model);
                    model.Imagen = string.Format("/storage/customers/img/{0}", uniqueFileName);
                }
                customer = new Customer()
                {
                    Id = model.Id,
                    Name = model.Name,
                    SurName = model.SurName,
                    Document = model.Document,
                    DocumentType = model.DocumentType,
                    Email = model.Email,
                    Imagen = model.Imagen,
                    CreateDate = model.CreateDate
                };
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult Delete(CustomerViewModel vmodel)
        {
            Customer model = new Customer();
            IEnumerable<Customer> lstModel = new List<Customer>();
            lstModel = _context.Customers.ToList();

            model = lstModel.Where(x => x.Id == vmodel.Id).FirstOrDefault();

            DeleteCustomer(model);
            SwalAlert("Registro borrado correctamente !!!", Enums.CommonEnums.NotifyType.success);
            return RedirectToAction("Index");
        }

        public void DeleteCustomer(Customer model)
        {
            try
            {
                Customer? mdl = model;
                _context.Customers.Remove(mdl);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private CustomerViewModel GetCustomerData(int itemId)
        {
            try
            {
                var result = _context.Customers.ToList();
                Customer? mView = result.Find(x => x.Id == itemId);
                CustomerViewModel? model = new CustomerViewModel()
                {
                    Id = itemId,
                    Name = mView.Name,
                    SurName = mView.SurName,
                    Document = mView.Document,
                    DocumentType = mView.DocumentType,
                    Email = mView.Email,
                    Imagen = mView.Imagen,
                    CreateDate = mView.CreateDate
                };
                return model ?? new CustomerViewModel();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ProcessUploadedFile(CustomerViewModel model)
        {
            string uniqueFileName = string.Empty;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "storage/customers/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        private void OnLoadHeaderComponent()
        {
            var pFolder = Path.Combine(_environment.WebRootPath, "settings/");
            string pfilePath = Path.Combine(pFolder, "");
            GetHeaderDataInfo(pfilePath);
        }
    }
}
