using App.Data;
using App.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WebApplication_Product.Controllers
{
    public class Repository : IRepository, IDisposable
    {
        ProductContext1 context;
        public Repository(ProductContext1 context)
        {
            this.context = context;
        }
        public IEnumerable<ProductFile> GetAll()
        {
            return context.ProductInformation.ToList();
        }

        public ProductFile Get(int id)
        {
            return context.ProductInformation.Find(id);
        }

        public ProductFile Add(ProductFile item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            context.ProductInformation.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            ProductFile product = context.ProductInformation.Find(id);
            context.ProductInformation.Remove(product);
        }

        public bool Update(ProductFile item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }

        public ProductFile PatchUpdate(ProductFile item)
        {

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return new ProductFile()
            {
                // employeeCode = item.employeeCode,
                //userId = item.userId,
                id = item.id,
                jobTitleName = item.jobTitleName,
               firstName = item.firstName,
               lastName = item.lastName,
               //preferredFullName = item.preferredFullName,
               //region = item.region,
               phoneNumber = item.phoneNumber,
               emailAddress = item.emailAddress

            };
        }
        #region IDisposable Support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                disposedValue = true;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}