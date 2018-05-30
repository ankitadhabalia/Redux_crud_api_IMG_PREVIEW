using App.Data;
using App.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication_Product.Models;
using System.Linq.Dynamic;
using WebApplication_Product.Helpers;
using System.Web.Http.Cors;

namespace WebApplication_Product.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        ProductContext1  _context;
        private IRepository repository;
        public ProductsController()
        {
            _context = new ProductContext1();

            this.repository = new Repository(new ProductContext1());
        }
        public ProductsController(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ProductFile> GetAllProducts([FromUri]PagingParameterModel pagingparametermodel)
        {

            // Return List of Customer  
            var source = (from prod in _context.ProductInformation.
                            OrderBy(a => a.id )
                          select prod).AsQueryable();

            // ------------------------------------ Search Parameter-------------------   

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.firstName.Contains(pagingparametermodel.QuerySearch));
            }

            // ------------------------------------ Search Parameter-------------------  

            // Get's No of Rows Count   
            int count = source.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage,
                QuerySearch = string.IsNullOrEmpty(pagingparametermodel.QuerySearch) ?
                      "No Parameter Passed" : pagingparametermodel.QuerySearch
            };

            // Setting Header  
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }

        //public IHttpActionResult Get(string sort)
        //{
        //    var data = (from prod in _context.ProductInformation.
        //                       OrderBy(a => a.id)
        //                select prod).AsQueryable();

        //    // Apply sorting
        //    data = data.ApplySort(sort);

        //    // Return response
        //    return Ok(data);
        //}

        //public IHttpActionResult GetProducts()
        //{
        //    var data = repository.GetAll();

        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(data);
        //}

        public IHttpActionResult GetProducts(int id)
        {
            ProductFile data = repository.Get(id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST: api/Customer

        public IHttpActionResult PostProduct(ProductFile item)
        {
            try
            {
                item = repository.Add(item);
                repository.Save();
                var response = Request.CreateResponse<ProductFile>(HttpStatusCode.Created, item);

                string uri = Url.Link("DefaultApi", new { id = item.id });
                response.Headers.Location = new Uri(uri);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        // PUT: api/Customer/5
        [System.Web.Http.HttpPut]
        public IHttpActionResult PutProduct(ProductFile product)
        {

            //product.id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok("Successful");
        }

        [System.Web.Http.HttpPatch]
        public IHttpActionResult PatchProducts(int id, ProductFile product)
        {
            product.id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok("Successful");
        }

        [System.Web.Http.HttpDelete]
        public void DeleteProduct(int id)
        {
            ProductFile item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(id);
            repository.Save();
        }
    }
}