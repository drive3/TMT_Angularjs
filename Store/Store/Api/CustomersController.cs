using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Store.Models;
using Store_My;
using Store.ViewModel;

namespace Store.Api
{
    public class CustomersController : ApiController
    {
        Data_Store context = null;
        // Constructor 
        public CustomersController()
        {
            // create instance of an object
            context = new Data_Store();
        }
        //Get All Employees  
        [HttpGet]
        public IEnumerable<CustomerViewModel> GetAllCustomer()
        {

            var data = context.customers.ToList().OrderBy(x => x.Name);
            var result = data.Select(x => new CustomerViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
                DOB = x.DOB,
            });
            return result.ToList();
        }


        //Get the single employee data  
        [HttpGet]
        public CustomerViewModel GetCustomer(int Id)
        {
            var data = context.customers.Where(x => x.Id == Id).FirstOrDefault();
            if (data != null)
            {
                CustomerViewModel customer = new CustomerViewModel();
                customer.Id = data.Id;
                customer.Name = data.Name;
                customer.Address = data.Address;
                customer.DOB = data.DOB;
                customer.PhoneNumber = data.PhoneNumber;

                return customer;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
        }

        //Add new employee  

        [HttpPost]
        public HttpResponseMessage AddCustomer(CustomerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = new Customer();
                    customer.Id = model.Id;
                    customer.Name = model.Name;
                    customer.Address = model.Address;
                    customer.PhoneNumber = model.PhoneNumber;
                    customer.DOB = Convert.ToDateTime(model.DOB.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    context.customers.Add(customer);
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "Submitted Successfully");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !", ex);
            }
        }

        //Update the employee  

        [HttpPut]
        public HttpResponseMessage UpdateCustomer(CustomerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = new Customer();
                    customer.Id = model.Id;
                    customer.Name = model.Name;       
                    customer.Address = model.Address;
                    customer.PhoneNumber = model.PhoneNumber;
                    customer.DOB = Convert.ToDateTime(model.DOB.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Updated Successfully");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong !");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !", ex);
            }
        }

        //Delete the employee  

        [HttpDelete]
        public HttpResponseMessage DeleteCustomer(int Id)
        {
            Customer customer = new Customer();
            customer = context.customers.Find(Id);
            if (customer != null)
            {
                context.customers.Remove(customer);
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong !");
            }
        }
    }
}