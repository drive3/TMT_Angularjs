using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.Http.OData.Routing;
using Store.ViewModel;
using Microsoft.Data.OData;

namespace Store.Api
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Store.ViewModel;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ProductViewModel>("ProductViewModels");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductViewModelsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/ProductViewModels
        public IHttpActionResult GetProductViewModels(ODataQueryOptions<ProductViewModel> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<IEnumerable<ProductViewModel>>(productViewModels);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/ProductViewModels(5)
        public IHttpActionResult GetProductViewModel([FromODataUri] int key, ODataQueryOptions<ProductViewModel> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<ProductViewModel>(productViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/ProductViewModels(5)
        public IHttpActionResult Put([FromODataUri] int key)
        {
         

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(productViewModel);

            // TODO: Save the patched entity.

            // return Updated(productViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/ProductViewModels
        public IHttpActionResult Post(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(productViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/ProductViewModels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(productViewModel);

            // TODO: Save the patched entity.

            // return Updated(productViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/ProductViewModels(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
