using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public ProductsController(IDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                var products =
                    _dataService.GetProducts().Select(x => CreateProductListModel(x));
                return Ok(products);
            }

            var data = _dataService.GetProductByName(search);
            return Ok(data);
        }

        [HttpGet("category/{id}")]
        public IActionResult GetProductsByCategory(int id)
        {
            var products = _dataService.GetProductsByCategory(id);

            if (products.Count == 0)
            {
                return NotFound(products);
            }

            return Ok(products);

        }
        [HttpGet("name/{name}")]
        public IActionResult GetProductsByName(string name)
        {
            var products = _dataService.GetProductsByName(name);

            if (products.Count == 0)
            {
                return NotFound(products);
            }

            return Ok(products);

        }

        [HttpGet("{id}", Name = nameof(GetProduct))]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = CreateProductModel(product);

            return Ok(model);

        }

        private ProductListModel CreateProductListModel(Product product)
        {
            var model = _mapper.Map<ProductListModel>(product);
            model.Url = _generator.GetUriByName(HttpContext, nameof(GetProduct), new { product.Id });
            return model;
        }

        private ProductModel CreateProductModel(Product product)
        {
            var model = _mapper.Map<ProductModel>(product);
            model.Url = _generator.GetUriByName(HttpContext, nameof(GetProduct), new { product.Id });
            model.Category.Url = _generator.GetUriByName(HttpContext, nameof(CategoriesController.GetCategory), new { product.Category.Id });
            return model;
        }
    }
}