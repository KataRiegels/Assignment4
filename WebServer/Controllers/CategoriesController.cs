using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using WebServer.Models;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

namespace WebServer.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public CategoriesController(IDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories =
                _dataService.GetCategories().Select(x => CreateCategoryModel(x));
            return Ok(categories);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = CreateCategoryModel(category);

            return Ok(model);

        }

        //[HttpPost]
        //public IActionResult CreateCategory(CategoryCreateModel model)
        //{
        //    var category = _mapper.Map<Category>(model);

        //    _dataService.CreateCategory(category);

        //    return CreatedAtRoute(null, CreateCategoryModel(category));
        //}

        //[HttpPost]
        //public IActionResult CreateCategory(CategoryCreateModel model)
        //{
        //    var category = _mapper.Map<Category>(model);

        //    category = _dataService.CreateCategory(category.Name, category.Description);

        //    return CreatedAtRoute(null, category);
        //}

        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateModel model)
        {

            var category = _dataService.CreateCategory(model.Name, model.Description);
            return CreatedAtRoute(null, CreateCategoryModel(category));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var deleted = _dataService.DeleteCategory(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }


        private CategoryModel CreateCategoryModel(Category category)
        {
            var model = _mapper.Map<CategoryModel>(category);
            model.Url = _generator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
            return model;
        }

        [HttpPut("{id}")]
        public IActionResult PutCategory(CategoryUpdateModel model, int id)
        {
            var updated = _dataService.UpdateCategory(model.Id, model.Name, model.Description);
            var category = _dataService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok();
            //return CreatedAtRoute(null,CreateCategoryModel();

        }
        private CategoryModel UpdateCategoryModel(Category category)
        {
            var model = _mapper.Map<CategoryModel>(category);
            model.Url = _generator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
            return model;
        }

  
    }


}