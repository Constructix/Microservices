using System;
using System.Collections.Generic;
using System.Linq;
using ConstructixOnLineServices.Models;
using ConstructixOnLineServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructixOnLineServices.Controllers
{
    [ApiController]
    public class SuppliersController : ControllerBase
    {

        private readonly ISupplierService _supplierService;
        
        public SuppliersController(ISupplierService supplierService)  => _supplierService = supplierService;

        [HttpGet]
        [Route("suppliers")]
        public List<Supplier> GetAll() => _supplierService.GetAll();

        [HttpGet]
        [Route("suppliers/name")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(string supplierName)
        {
            var result = _supplierService.GetSupplierByName(supplierName);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }


        [HttpGet]
        [Route("[controller]/id")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetWithId(Guid id)
        {
            var result = _supplierService.GetSupplierById(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}