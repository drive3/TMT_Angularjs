using Store.Models;
using Store.ViewModel;
using Store_My;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Store.Api
{
    public class CatelogyController : ApiController
    { 

            // Constructor   
            Data_Store context = new Data_Store();

            //Get All Employees  
            [HttpGet]
            public IEnumerable<CatelogyViewModel> GetAllCatelogy()
            {

                var data = context.catelogies.ToList().OrderBy(x => x.Name);
                var result = data.Select(x => new CatelogyViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Description = x.Description
                });
                return result.ToList();
            }


            //Get the single employee data  
            [HttpGet]
            public CatelogyViewModel GetCatelogy(int Id)
            {
                var data = context.catelogies.Where(x => x.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    CatelogyViewModel catelogy = new CatelogyViewModel();
                    catelogy.Id = data.Id;
                    catelogy.Name = data.Name;
                    catelogy.Description = data.Description;
                    catelogy.Date = data.Date;
                    return catelogy;
                }
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
            }

            //Add new employee  

            [HttpPost]
            public HttpResponseMessage AddCatelogy(CatelogyViewModel model)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Catelogy catelogy = new Catelogy();
                        catelogy.Id = model.Id;
                        catelogy.Name = model.Name;
                        catelogy.Description = model.Description;
                        catelogy.Date = Convert.ToDateTime(model.Date.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    context.catelogies.Add(catelogy);
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
            public HttpResponseMessage UpdateCatelogy(CatelogyViewModel model)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Catelogy catelogy = new Catelogy();
                        catelogy.Id = model.Id;
                        catelogy.Name = model.Name;
                        catelogy.Description = model.Description;
                        catelogy.Date = Convert.ToDateTime(model.Date.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    context.Entry(catelogy).State = System.Data.Entity.EntityState.Modified;
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
            public IHttpActionResult DeleteCatelogy(int Id)
            {
                Catelogy catelogy = new Catelogy();
                catelogy = context.catelogies.Find(Id);
                if (catelogy != null)
                {
                    context.catelogies.Remove(catelogy);
                    context.SaveChanges();
                    return Ok(catelogy);
                }
                else
                {
                    //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Không Được Xoá Sản Phẩm Đã Tồn Tại Trong Hoá Đơn");
                    return BadRequest("Không Tìm Thấy");
                }
            }
        }
    }
