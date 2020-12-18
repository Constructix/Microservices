using System;
using System.Collections.Generic;
using ConstructixOnLineServices.Controllers;
using ConstructixOnLineServices.Models;
using ConstructixOnLineServices.Repository;

namespace ConstructixOnLineServices.Services
{
    public class SupplierService : ISupplierService
    {
        private IRepository<Supplier, string> _supplierRepository;
        private IRepository<Category, string> _categoryRepository;


        public SupplierService(IRepository<Supplier, string> supplierRepository, IRepository<Category, string> categoryRepository)
        {
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
        }


        public List<Supplier> GetAll() => _supplierRepository.GetAll();

        public Supplier GetSupplierByName(string id) => _supplierRepository.Get(id);
        public Supplier GetSupplierById(Guid id) => _supplierRepository.GetById(id);

    }
}