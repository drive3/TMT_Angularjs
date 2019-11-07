using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Store_My.Models;
using Store_My;
using Store.ViewModel;

namespace Project_Angualar_Demo.Api
{
    [RoutePrefix("/api/Product")]
    public class ProductController : ApiController
    {
        // StudentDBEntities object point  
        
        // Constructor   
        Data_Store context = new Data_Store();

        //Get All Employees  
        [HttpGet]
        public IEnumerable<ProductViewModel> GetAllProduct()
        {

            var data = context.Products.ToList().OrderBy(x => x.Name);
            var result = data.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Note = x.Note,
                DateCreated = x.DateCreated,
                CatelogyName = x.Catelogy.Name,
                CatelogyID = x.CatelogyId,
                CodeProduct = x.CodeProduct,
            }) ;
            return result.ToList();
        }
        //Get the single employee data  
        [HttpGet]
        public ProductViewModel GetProduct(int Id)
        {
            var data = context.Products.Where(x => x.Id == Id).FirstOrDefault();
            if (data != null)
            {
                ProductViewModel product = new ProductViewModel();
                product.Id = data.Id;
                product.Name = data.Name;
                product.Price = data.Price;
                product.Description = data.Description;
                product.Note = data.Note;
                product.DateCreated = data.DateCreated;
                product.CatelogyName = data.Catelogy.Name;
                product.CatelogyID = data.CatelogyId;
                return product;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
        }

        //Add new employee  

        [HttpPost]
        public HttpResponseMessage AddProduct(ProductViewModel model)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                  
                    Product product = new Product();
                    product.Id = model.Id;
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    product.Note = model.Note;
                    product.CatelogyId = model.CatelogyID;
                    product.CodeProduct = "SP" + model.DateCreated.ToString("ddMMmmss") ;

                    product.DateCreated = Convert.ToDateTime(model.DateCreated.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    context.Products.Add(product);
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
        public HttpResponseMessage UpdateProduct(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    product.Id = model.Id;
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    product.Note = model.Note;
                    product.CatelogyId = model.CatelogyID; 
                    product.DateCreated = Convert.ToDateTime(model.DateCreated.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    context.Entry(product).State = System.Data.Entity.EntityState.Modified;
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
        public IHttpActionResult DeleteProduct(int Id)
        {
            Product products = new Product();
            products = context.Products.Find(Id);
            if (products != null)
            {
                context.Products.Remove(products);
                context.SaveChanges();
                return Ok(products);
            }
            else
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Không Được Xoá Sản Phẩm Đã Tồn Tại Trong Hoá Đơn");
                return BadRequest("Không Tìm Thấy");
            }
        }
    }
}