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
using Store.Models;

namespace Store.Api
{
    public class OrderController : ApiController
    {
        Data_Store context = new Data_Store();

        //Get All Employees
        [HttpGet]
        public IEnumerable<OrderViewModel> GetAllOrder()
        {

            var data = context.Orders.ToList().OrderBy(x => x.Id);
            var result = data.Select(x => new OrderViewModel()
            {
               Id = x.Id,
               orderDate = x.orderDate,
               OrderNumber = x.OrderNumber,
               TotalAmount = x.TotalAmount,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer.Name,
                CustomerAddress = x.Customer.Address,      
                CustomerPhoneNumber = x.Customer.PhoneNumber,
            });
            return result.ToList();
        }


        //Get the single employee data
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Order orderDeatil = context.Orders.Where(x => x.Id == id).FirstOrDefault();
            var order = context.Orders.FirstOrDefault(x => x.Id == id);
            if (order != null)
            {
                var model = new OrderViewModel
                {
                    Id = order.Id,
                    orderDate = order.orderDate,
                    OrderNumber = order.OrderNumber,
                    TotalAmount = order.TotalAmount,
                    CustomerId = order.CustomerId,
                    CustomerName = order.Customer.Name,
                    CustomerAddress = order.Customer.Address,
                    CustomerPhoneNumber = order.Customer.PhoneNumber,
                    Items = new List<OrderDetailViewModel>(),
                };
                foreach (var item in orderDeatil.Items)
                {
                    var detail = new OrderDetailViewModel
                    {
                        Id = item.Id,
                        Quantiy = item.Quantiy,
                        ProductName = item.Product.Name,
                        Price = item.Price,
                        OrderId = item.OrderId,
                    };
                    model.Items.Add(detail);
                }
                return Ok(model);
            }
            else
            {
                return BadRequest("Không tìm thấy");
            }
        }
        // Add new order
        //[HttpPost]
        //public HttpResponseMessage AddOrder(OrderViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Order order = new Order();
        //            order.Id = model.Id;
        //            order.orderDate = model.orderDate;
        //            order.OrderNumber = model.OrderNumber;
        //            order.TotalAmount = model.TotalAmount;
        //            order.CustomerId = model.CustomerId;
        //            context.Orders.Add(order);
        //            var result = context.SaveChanges();
        //            if (result > 0)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.Created, "Submitted Successfully");
        //            }
        //            else
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !", ex);
        //    }
        //}
        [ResponseType(typeof(Order))]
        [HttpPost]
        public HttpResponseMessage SaveOrder(Order Orders)
        {

            int result = 0;
            try
            {
                context.Orders.Add(Orders);
                context.SaveChanges();
                result = 1;
            }
            catch (Exception e)
            {
                result = 0;
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //Update the employee

        [HttpPut]
        public HttpResponseMessage UpdateOrder(int key, OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //Order order = new Order();
                    Order order = context.Orders.Where(i => i.Id == key).SingleOrDefault();//Lay ra order
                    order.orderDate = model.orderDate;
                    order.OrderNumber = model.OrderNumber;
                    order.TotalAmount = model.TotalAmount;
                    order.CustomerId = model.CustomerId;
                    var demo = model.Items.Select(x => x.Id ).ToList();
                    List<OrderDetail>
                     itemRemoves = order.Items.Where(x => ! demo.Contains(x.Id)).ToList();
                    foreach (var i in itemRemoves)
                    {
                        context.OrderDetails.Remove(i);
                    }
                    foreach (var item in model.Items)//duyệt list items mới truyền lên chưa có thì thêm vào db có rồi thì update propety
                    {
                        var orderDetail = context.OrderDetails.Where(i => i.Id == item.Id).FirstOrDefault();// lấy 1 chi tiết có id sản phẩm = id sản phẩm truyền lên

                        if (orderDetail != null)
                        {
                            orderDetail.Quantiy = item.Quantiy;
                            orderDetail.Price = item.Price;
                        }
                        else
                        {
                            OrderDetail ord = new OrderDetail
                            {
                                ProductId = item.ProductId,
                                Price = item.Price,
                                Quantiy = item.Quantiy
                            };
                            order.Items.Add(ord);
                        }
                    }
                    context.Entry(order).State = System.Data.Entity.EntityState.Modified;
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
        public HttpResponseMessage DeleteEmployee(int Id)
        {
            Order order = new Order();
            order = context.Orders.Find(Id);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, order);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
            }
        }
    }
}
