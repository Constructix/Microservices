﻿using System;
using System.Collections.Generic;
using System.Linq;
using ConstructixOnLineServices.Models;
using ConstructixOnLineServices.Repository;
using ConstructixOnLineServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructixOnLineServices.Controllers
{
    public class SupplierRepository : IRepository<Supplier, string>
    {
        private List<Supplier> _items;

        public Supplier Get(string id) => _items.Find(x => x.Name.Equals(id, StringComparison.CurrentCultureIgnoreCase));

        public List<Supplier> GetAll() =>  _items;

        public SupplierRepository(List<Supplier> items) => _items = items;
    }

    public class CategoryRepository : IRepository<Category, string>
    {
        private List<Category> _items;
        public Category Get(string id) => _items.Find(x => x.Name.Equals(id, StringComparison.CurrentCultureIgnoreCase));

        public List<Category> GetAll() => _items;

        public CategoryRepository(List<Category> items) => _items = items;
    }


    [ApiController]
    [Route("[controller]")]
    public class SuppliersController : ControllerBase
    {

        private readonly ISupplierService _supplierService;
        
        public SuppliersController(ISupplierService supplierService)  => _supplierService = supplierService;

        [HttpGet]
        public List<Supplier> GetAll() => _supplierService.GetAll();

        [HttpGet("{supplierName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(string supplierName)
        {
            var result = _supplierService.Get(supplierName);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}