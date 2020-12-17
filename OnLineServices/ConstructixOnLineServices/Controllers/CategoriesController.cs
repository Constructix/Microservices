using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructixOnLineServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructixOnLineServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private IRepository<Category, string> _repository;

        public CategoriesController(IRepository<Category, string> _categoryRepository) => _repository = _categoryRepository;

        [HttpGet]
        public List<Category> GetAll() => _repository.GetAll();

        [HttpGet("{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(string category)
        {
            var result = _repository.Get(category);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}