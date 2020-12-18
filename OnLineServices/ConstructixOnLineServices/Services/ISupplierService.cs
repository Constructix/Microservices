using System;
using System.Collections.Generic;
using ConstructixOnLineServices.Models;

namespace ConstructixOnLineServices.Services
{
    public interface ISupplierService
    {
        public List<Supplier> GetAll();
        public Supplier GetSupplierByName(string id);
        public Supplier GetSupplierById(Guid id);


    }
}